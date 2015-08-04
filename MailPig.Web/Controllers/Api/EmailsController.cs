namespace MailPig.Web.Controllers.Api
{
    using BL.Mailing;
    using BL.Models;
    using BL.Services;
    using Core;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/emails")]
    public class EmailsController : MailPigApiControllerBase
    {
        public EmailsService EmailsService { get; set; }

        public EmailsController()
        {
            EmailsService = new EmailsService(UnitOfWork);
        }

        // GET: api/emails
        [Route("")]
        public IEnumerable<EmailModel> Get()
        {
            return EmailsService.GetAllEmails();
        }

        // GET: api/emails/5
        [Route("{id}")]
        public EmailModel Get(int id)
        {
            return EmailsService.GetEmailById(id);
        }

        // POST: api/emails
        public IHttpActionResult Post([FromBody]EmailModel email)
        {
            EmailModel createdMail = EmailsService.CreateEmail(email);
            if (createdMail != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/emails
        public IHttpActionResult Put([FromBody]EmailModel email)
        {
            EmailModel editedMail = EmailsService.EditEmail(email);
            if (editedMail != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // POST api/emails/5/sendto/6
        [Route("{emailId}/sendto/{groupId}")]
        public IHttpActionResult SendEmailToGroup(int emailId, int groupId)
        {
            IEnumerable<MailerResult> results = EmailsService.SendMailToGroup(emailId, groupId);

            if (results.Any(result => result.Status != MailerResultStatusEnum.Succeeded))
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}