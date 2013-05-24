using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AzurePhotoManager.Models;

namespace AzurePhotoManager.Controllers
{   
    public class PhotosController : Controller
    {
		private readonly IPhotoRepository photoRepository;

        //// If you are using Dependency Injection, you can delete the following constructor
        //public PhotosController() : this(new PhotoRepository())
        //{
        //}

        public PhotosController(IPhotoRepository photoRepository)
        {
			this.photoRepository = photoRepository;
        }

        //
        // GET: /Photos/

        public ViewResult Index()
        {
            return View(photoRepository.All);
        }

        public JsonResult Home()
        {
            var userId = Request.RequestContext.HttpContext.User.Identity.Name;
            return Json(photoRepository.All.Where<Photo>(l=>l.UserId==userId).Take<Photo>(20), JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Photos/Details/5

        public ViewResult Details(int id)
        {
            return View(photoRepository.Find(id));
        }

        //
        // GET: /Photos/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Photos/Create

        [HttpPost]
        public ActionResult Create(Photo photo)
        {
            if (ModelState.IsValid) {
                photoRepository.InsertOrUpdate(photo);
                photoRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Photos/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(photoRepository.Find(id));
        }

        //
        // POST: /Photos/Edit/5

        [HttpPost]
        public ActionResult Edit(Photo photo)
        {
            if (ModelState.IsValid) {
                photoRepository.InsertOrUpdate(photo);
                photoRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Photos/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(photoRepository.Find(id));
        }

        //
        // POST: /Photos/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            photoRepository.Delete(id);
            photoRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                photoRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

