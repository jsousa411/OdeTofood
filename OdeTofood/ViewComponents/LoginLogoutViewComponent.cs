using Microsoft.AspNetCore.Mvc;

namespace OdeTofood.ViewComponents
{
    public class LoginLogoutViewComponent:  ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();

        }
    }
}
