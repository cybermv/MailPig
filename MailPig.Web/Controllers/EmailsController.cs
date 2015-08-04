namespace MailPig.Web.Controllers
{
    using BL.Mailing;
    using BL.Models;
    using BL.Services;
    using Core;
    using DAL.Core;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class EmailsController : MailPigControllerBase
    {
        public EmailsService EmailService { get; set; }

        public EmailsController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            EmailService = new EmailsService(UnitOfWork);
        }

        // GET: Emails
        [HttpGet]
        public ActionResult Index(int id = 0)
        {
            ViewBag.SelectedEmailId = id;

            IEnumerable<EmailModel> model = EmailService.GetAllEmails();
            return View(model);
        }

        // GET: Emails/Create
        [HttpGet]
        public ActionResult Create()
        {
            EmailEditCreateModel model = new EmailEditCreateModel();
            return View(model);
        }

        // POST: Emails/Create
        [HttpPost]
        public ActionResult Create(EmailEditCreateModel model)
        {
            if (ModelState.IsValid)
            {
                EmailModel blModel = new EmailModel
                {
                    Id = model.Id,
                    Subject = model.Subject,
                    Body = model.Body
                };

                EmailModel createdEmail = EmailService.CreateEmail(blModel);
                if (createdEmail != null)
                {
                    return RedirectToAction("Index", "Emails", new { id = createdEmail.Id });
                }
            }

            return View(model);
        }

        // GET: Emails/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmailModel blModel = EmailService.GetEmailById(id);
            if (blModel != null)
            {
                EmailEditCreateModel model = new EmailEditCreateModel
                {
                    Id = blModel.Id,
                    Subject = blModel.Subject,
                    Body = blModel.Body
                };
                return View(model);
            }

            return RedirectToAction("Index", "Emails");
        }

        // POST: Emails/Edit
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(EmailEditCreateModel model)
        {
            if (ModelState.IsValid)
            {
                EmailModel blModel = new EmailModel
                {
                    Id = model.Id,
                    Subject = model.Subject,
                    Body = model.Body
                };

                EmailModel updatedEmail = EmailService.EditEmail(blModel);

                if (updatedEmail != null)
                {
                    return RedirectToAction("Index", "Emails", new { id = updatedEmail.Id });
                }
            }
            return View(model);
        }

        // POST: Emails/SendMail/5/To/6
        [HttpPost]
        [Route("Emails/SendMail/{emailId}/To/{groupId}")]
        public ActionResult SendMail(int emailId, int groupId)
        {
            IEnumerable<MailerResult> results = EmailService.SendMailToGroup(emailId, groupId);

            if (results.All(r => r.Status == MailerResultStatusEnum.Succeeded))
            {
                return StatusCode(HttpStatusCode.OK);
            }

            return StatusCode(HttpStatusCode.BadRequest);
        }
    }
}