using Microsoft.AspNetCore.Mvc;

namespace BigHauling.Controllers
{
    public class LoginController : Controller
    {

        private readonly ISession _session;
        public LoginController()
        {
            var bl = new BusinessLogic();
            _session = bl.GetSessionBL();
        }

        // GET: Login
        public IActionResult Index()
        {
            return View();
        }
    }
}
