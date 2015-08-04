namespace MailPig.Web.Controllers
{
    using BL.Models;
    using BL.Services;
    using Core;
    using DAL.Core;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    [Authorize]
    public class SubscribersController : MailPigControllerBase
    {
        public SubscriberService SubscriberService { get; set; }

        public GroupService GroupService { get; set; }

        public SubscribersController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            SubscriberService = new SubscriberService(UnitOfWork);
            GroupService = new GroupService(UnitOfWork);
        }

        // GET: Subscribers?groupId=5
        [HttpGet]
        public ActionResult Index(int? groupId)
        {
            IEnumerable<SubscriberGroupsModel> model;
            if (groupId.HasValue)
            {
                GroupModel groupToShow = GroupService.GetGroupById(groupId.Value);

                if (groupToShow != null)
                {
                    model = SubscriberService.GetSubscribersFor(groupId.Value);
                    ViewBag.GroupName = groupToShow.Name;
                    return View(model);
                }
            }
            model = SubscriberService.GetAllSubscribers();
            return View(model);
        }

        // GET: Subscribers/History/5
        [HttpGet]
        public ActionResult History(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            SubscriberModel subscriber = SubscriberService.GetById(id.Value);
            IEnumerable<SubscriberHistoryModel> history = SubscriberService.GetHistoryFor(id.Value);

            if (history != null)
            {
                ViewBag.NameSurname = subscriber.Name + " " + subscriber.Surname;
                ViewBag.Email = subscriber.Email;
                return View(history);
            }
            return RedirectToAction("Index");
        }

        // GET: Subscribers/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Subscribers");
            }

            SubscriberModel model = SubscriberService.GetById(id.Value);

            if (model == null)
            {
                return RedirectToAction("Index", "Subscribers");
            }

            return View(model);
        }

        // POST: Subscribers/Edit/5
        [HttpPost]
        public ActionResult Edit(SubscriberModel model)
        {
            if (ModelState.IsValid)
            {
                SubscriberModel editedSubscriber = SubscriberService.EditSubscriber(model);
                if (editedSubscriber != null)
                {
                    return RedirectToAction("Index", "Subscribers");
                }
            }

            return View(model);
        }

        // GET: Subscribers/Create
        [HttpGet]
        public ActionResult Create()
        {
            SubscriberWithFirstGroupModel model = new SubscriberWithFirstGroupModel();
            return View(model);
        }

        // POST: Subscribers/Create
        [HttpPost]
        public ActionResult Create(SubscriberWithFirstGroupModel model)
        {
            if (ModelState.IsValid)
            {
                SubscriberModel addedSubscriber = SubscriberService.AddSubscriber(model);

                if (addedSubscriber != null)
                {
                    return RedirectToAction("Index", "Subscribers");
                }
            }

            return View(model);
        }

        // POST: Subscribers/Delete
        [HttpPost]
        public ActionResult Remove(int? id)
        {
            if (!id.HasValue)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            bool removed = SubscriberService.RemoveSubscriber(id.Value);

            if (removed)
            {
                return StatusCode(HttpStatusCode.OK);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        [HttpGet]
        public ActionResult SubscriberRow(int id)
        {
            if (Request.IsAjaxRequest())
            {
                SubscriberModel model = SubscriberService.GetById(id);
                if (model != null)
                {
                    return PartialView("_SubsriberRowEntry", model);
                }
            }
            return RedirectToAction("Index", "Subscribers");
        }

        [HttpGet]
        public ActionResult SubscribersListAjax()
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("Index", "Subscribers");
            }

            IEnumerable<KeyValuePair<int, string>> subscribers = SubscriberService
                .GetAllSubscribers()
                .Select(s =>
                    new KeyValuePair<int, string>(
                        s.Id,
                        string.Format("{0} {1} - {2}", s.Name, s.Surname, s.Email)));

            return Json(subscribers, JsonRequestBehavior.AllowGet);
        }
    }
}