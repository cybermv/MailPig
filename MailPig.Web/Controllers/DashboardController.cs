namespace MailPig.Web.Controllers
{
    using BL.Models;
    using BL.Services;
    using Core;
    using DAL.Core;
    using System.Web.Mvc;

    [Authorize]
    public class DashboardController : MailPigControllerBase
    {
        public StatisticsService StatisticsService { get; set; }

        public DashboardController(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            StatisticsService = new StatisticsService(UnitOfWork);
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            DashboardPageStatisticsModel model = StatisticsService.GetDashboardStatistics();

            return View(model);
        }
    }
}