namespace MailPig.BL.Mailing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Text.RegularExpressions;

    public class Mailer
    {
        private readonly SmtpClient _mailer;
        private readonly Regex _mailRegex;

        public Mailer()
        {
            this._mailer = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("mailpig.noreply@gmail.com", "mvcprojekt2015"),
                EnableSsl = true
            };
            this._mailRegex = new Regex(@"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        public MailerResult Send(MailPigEmail mail)
        {
            MailerResult result = ValidateMail(mail);
            if (!result.IsValid)
            {
                return result;
            }

            MailMessage message = Compose(mail);

            return SendInternal(message, mail);
        }

        public IEnumerable<MailerResult> SendBulk(IEnumerable<MailPigEmail> mails)
        {
            return mails
                .Select(mailToSend => this.Send(mailToSend))
                .ToList();
        }

        private MailerResult ValidateMail(MailPigEmail mail)
        {
            MailerResult result = new MailerResult();

            if (!_mailRegex.IsMatch(mail.Sender))
            {
                result.Status = (result.Status & ~MailerResultStatusEnum.Succeeded) |
                                MailerResultStatusEnum.InvalidSender;
                result.Message += string.Format("Invalid sender e-mail {0}", mail.Sender) + Environment.NewLine;
            }

            if (!_mailRegex.IsMatch(mail.Recipient))
            {
                result.Status = (result.Status & ~MailerResultStatusEnum.Succeeded) |
                                MailerResultStatusEnum.InvalidRecipient;
                result.Message += string.Format("Invalid recipient e-mail {0}", mail.Recipient) + Environment.NewLine;
            }

            return result;
        }

        private MailMessage Compose(MailPigEmail mail)
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(mail.Sender);
            //message.Sender = new MailAddress(mail.Sender);

            message.To.Add(new MailAddress(mail.Recipient));

            message.Subject = mail.Subject;
            message.Body = mail.Body;
            message.IsBodyHtml = true;

            return message;
        }

        private MailerResult SendInternal(MailMessage message, MailPigEmail mail)
        {
            try
            {
                this._mailer.Send(message);
                mail.SendSuceeded();
                return new MailerResult();
            }
            catch (Exception ex)
            {
                return new MailerResult(MailerResultStatusEnum.Exception, ex.Message, ex);
            }
        }
    }
}