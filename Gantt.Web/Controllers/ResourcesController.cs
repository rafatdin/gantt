using Gantt.Web.DAL;
using Gantt.Web.Mappers;
using Gantt.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gantt.Web.Controllers
{
    public class ResourcesController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Resources
        public ActionResult Index()
        {
            var items = unitOfWork.ResourcesRepository.Get();
            var vmItems = ResourceMapper.MapItemToViewModel(items);
            return View(vmItems);
        }

        // GET: Resources/Create
        public ActionResult Create()
        {
            var model = new ResourceViewModel();
            return View(model);
        }
        
        public ActionResult CreatePartial()
        {
            var model = new ResourceViewModel();
            return PartialView("_Create", model);
        }

        // POST: Resources/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResourceViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var resource = ResourceMapper.MapViewModelToItem(model);
                    resource.created = DateTime.Today;
                    unitOfWork.ResourcesRepository.Insert(resource);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException dex)
            {
                ModelState.AddModelError("", dex.Message);
            }
            return View(model);
        }

        // GET: Resources/Edit/5
        public ActionResult Edit(int id)
        {
            var model = ResourceMapper.MapItemToViewModel(unitOfWork.ResourcesRepository.GetByID(id));
            return View(model);
        }

        public ActionResult EditPartial(int id)
        {
            ResourceViewModel model = ResourceMapper.MapItemToViewModel(unitOfWork.ResourcesRepository.GetByID(id));
            return PartialView("_Edit", model);
        }

        // POST: Resources/Edit/5
        [HttpPost]
        public ActionResult Edit(ResourceViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resource = ResourceMapper.MapViewModelToItem(model);
                    resource.updated = DateTime.Today;
                    unitOfWork.ResourcesRepository.Update(resource);
                    unitOfWork.Save();
                    return RedirectToAction("Index");
                }

            }
            catch (DataException dex)
            {
                ModelState.AddModelError("", dex.Message);
            }
            return View(model);
        }

        // GET: Resources/Delete/5
        public ActionResult Delete(int id)
        {
            var resource = unitOfWork.ResourcesRepository.GetByID(id);
            return View(resource);
        }

        // POST: Resources/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            unitOfWork.ResourcesRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }


    }
}
