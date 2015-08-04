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
    public class GroupsController : MailPigControllerBase
    {
        public GroupService GroupService { get; set; }

        public GroupsController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            GroupService = new GroupService(UnitOfWork);
        }

        // GET: Groups
        public ActionResult Index()
        {
            IEnumerable<TableGroupModel> model = GroupService.GetAllGroups();

            return View(model);
        }

        // GET: Groups/Create
        [HttpGet]
        public ActionResult Create()
        {
            GroupModel model = new GroupModel();
            return View(model);
        }

        // POST: Groups/Create
        [HttpPost]
        public ActionResult Create(GroupModel model)
        {
            if (ModelState.IsValid)
            {
                GroupModel newGroup = GroupService.CreateNewGroup(model);
                if (newGroup != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        // GET: Groups/Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }

            GroupModel toEdit = GroupService.GetGroupById(id.Value);

            if (toEdit != null)
            {
                return View(toEdit);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Groups/Edit
        [HttpPost]
        public ActionResult Edit(GroupModel model)
        {
            if (ModelState.IsValid)
            {
                GroupModel newGroup = GroupService.EditGroup(model);
                if (newGroup != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }

        // POST: Groups/Remove
        [HttpPost]
        public ActionResult Remove(int? id)
        {
            if (!id.HasValue)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }

            bool removed = GroupService.RemoveGroup(id.Value);

            if (removed)
            {
                return StatusCode(HttpStatusCode.OK);
            }
            else
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
        }

        // GET: Groups/EditSubscribers
        [HttpGet]
        public ActionResult EditSubscribers(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Groups");
            }

            GroupWithSubscribersModel model = GroupService.GetGroupWithSubscribersById(id.Value);

            if (model == null)
            {
                RedirectToAction("Index", "Groups");
            }

            return View(model);
        }

        // POST: Groups/EditSubscribers
        [HttpPost]
        public ActionResult EditSubscribers(GroupWithSubscribersModel model)
        {
            if (ModelState.IsValid)
            {
                bool editSuccessful = GroupService.EditGroupSubscriptions(model);
                if (editSuccessful)
                {
                    return RedirectToAction("Index", "Groups");
                }
            }

            return View(model);
        }

        // GET: Groups/GroupsList
        [HttpGet]
        public ActionResult GroupsList()
        {
            List<KeyValuePair<int, string>> groupsList = GroupService.GetAllGroups()
                .Select(g => new KeyValuePair<int, string>(g.Id, g.Name))
                .ToList();

            return Json(groupsList, JsonRequestBehavior.AllowGet);
        }
    }
}