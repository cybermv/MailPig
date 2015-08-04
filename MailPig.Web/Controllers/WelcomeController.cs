namespace MailPig.Web.Controllers
{
    using BL.Models;
    using BL.Services;
    using Core;
    using DAL.Core;
    using System.Web.Mvc;

    public class WelcomeController : MailPigControllerBase
    {
        public WelcomeController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            StatisticsService = new StatisticsService(UnitOfWork);
        }

        public StatisticsService StatisticsService { get; set; }

        // GET: /
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            WelcomePageStatisticsModel model = StatisticsService.GetWelcomePageStatistics();
            return View(model);
        }

        // GET: Welcome/Description
        public ActionResult Description()
        {
            return View();
        }

        // GET: Welcome/References
        public ActionResult References()
        {
            return View();
        }

        // GET: Welcome/Try
        public ActionResult Try()
        {
            return View();
        }
    }
}