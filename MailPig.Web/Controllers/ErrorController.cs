namespace MailPig.Web.Controllers
{
    using Core;
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        // GET: Error?code=401
        public ActionResult Index(int? code)
        {
            if (code.HasValue)
            {
                return View((ErrorType)code.Value);
            }
            else
            {
                return View(ErrorType.NotFound);
            }
        }
    }
}