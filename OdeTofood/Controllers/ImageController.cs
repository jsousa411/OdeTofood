using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OdeTofood.ViewModels;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OdeTofood.Controllers
{
    public class ImageController : Controller
    {
        // GET: /<controller>/
        public ActionResult View(string id)
        {
            PictureDisplay _images = new PictureDisplay();
            

            var image = _images.ImagePath; // _images.LoadImage(id);//pull image from directory

            return View(image);
        }
    }
}
