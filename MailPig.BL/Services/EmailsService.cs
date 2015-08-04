namespace MailPig.BL.Services
{
    using Core;
    using DAL.Core;
    using Mailing;
    using Model.Entities;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class EmailsService : ServiceBase
    {
        public const string SenderEmail = "noreply@mailpig.hr";

        public EmailsService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IEnumerable<EmailModel> GetAllEmails()
        {
            IRepository<Email> emailRepo = UnitOfWork.Repository<Email>();
            return emailRepo.Query
                .Select(e => new EmailModel
                {
                    Id = e.Id,
                    Body = e.Body,
                    Subject = e.Subject
                })
                .ToList();
        }

        public EmailModel GetEmailById(int emailId)
        {
            IRepository<Email> emailRepo = UnitOfWork.Repository<Email>();

            return emailRepo.Query
                .Where(e => e.Id == emailId)
                .Select(e => new EmailModel
                {
                    Id = e.Id,
                    Body = e.Body,
                    Subject = e.Subject
                })
                .SingleOrDefault();
        }

        public EmailModel CreateEmail(EmailModel email)
        {
            IRepository<Email> emailRepo = UnitOfWork.Repository<Email>();
            Email newMail = new Email
            {
                Subject = email.Subject,
                Body = email.Body
            };

            Email insertedEmail = emailRepo.Insert(newMail);

            if (insertedEmail != null)
            {
                email.Id = insertedEmail.Id;
                return email;
            }
            return null;
        }

        public EmailModel EditEmail(EmailModel email)
        {
            IRepository<Email> emailRepo = UnitOfWork.Repository<Email>();

            Email emailToEdit = emailRepo.Query.SingleOrDefault(e => e.Id == email.Id);

            if (emailToEdit != null)
            {
                emailToEdit.Subject = email.Subject;
                emailToEdit.Body = email.Body;
                return emailRepo.Update(emailToEdit) != null ? email : null;
            }
            return null;
        }

        public IEnumerable<MailerResult> SendMailToGroup(int emailId, int groupId)
        {
            IRepository<Email> emailRepo = UnitOfWork.Repository<Email>();
            IRepository<GroupSubscription> groupSubscriptionsRepo = UnitOfWork.Repository<GroupSubscription>();

            Email emailToSend = emailRepo.FindById(emailId);
            List<GroupSubscription> groupSubscribers = groupSubscriptionsRepo.Query
                .Where(gs => gs.GroupId == groupId &&
                             !gs.DateLeft.HasValue)
                .Include(gs => gs.Subscriber)
                .ToList();

            // if email not found or no subscribers to send to, return null
            if (emailToSend == null || groupSubscribers.Count == 0)
            {
                return new[] { new MailerResult(MailerResultStatusEnum.Exception, "Invalid Id's specified!", null) };
            }

            List<MailPigEmail> emailsToDeliver = groupSubscribers
                .Select(gs => new MailPigEmail(
                    emailId,
                    gs.Id,
                    SenderEmail,
                    gs.Subscriber.Email,
                    emailToSend.Subject,
                    emailToSend.Body))
                .ToList();

            Mailer mailer = new Mailer();
            IEnumerable<MailerResult> results = mailer.SendBulk(emailsToDeliver);

            IRepository<SentEmail> sentEmailsRepo = UnitOfWork.Repository<SentEmail>();
            foreach (MailPigEmail mailPigEmail in emailsToDeliver)
            {
                if (mailPigEmail.SentSuccessfully)
                {
                    SentEmail sentMail = new SentEmail
                    {
                        DateSent = DateTime.Now,
                        EmailId = mailPigEmail.EmailId,
                        RecipientId = mailPigEmail.RecipientId
                    };
                    sentEmailsRepo.Insert(sentMail);
                }
            }

            return results.ToList();
        }
    }
}