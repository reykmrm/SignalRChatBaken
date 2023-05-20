using Microsoft.AspNetCore.Mvc;

namespace SignalRChatGPT.Hubs
{
    public class LoginHub : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
