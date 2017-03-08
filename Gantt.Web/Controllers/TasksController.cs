using Gantt.Web.DAL;
using Gantt.Web.Mappers;
using Gantt.Web.Models.Entities;
using Gantt.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gantt.Web.Controllers
{
    public class TasksController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Tasks
        public ActionResult Index()
        {
            var items = unitOfWork.TasksRepository.Get();
            var vmItems = TaskMapper.MapItemToViewModel(items);
            return View(vmItems);
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int id)
        {
            var item = unitOfWork.TasksRepository.GetByID(id);
            var vm = TaskMapper.MapItemToViewModel(item);
            return View(item);
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            var model = new TaskViewModel();
            return View(model);
        }

        public ActionResult CreatePartial()
        {
            var model = new TaskViewModel();
            model.StartDate = DateTime.Today;
            model.EndDate = DateTime.Today.AddMonths(3);
            return PartialView("_Create", model);
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.StartDate > model.EndDate)
                    {
                        ModelState.AddModelError("StartDate", "Start date cannot be later than end date of the task");
                        return View(model);
                    }

                    var task = TaskMapper.MapViewModelToItem(model);
                    task.created = DateTime.Today;
                    unitOfWork.TasksRepository.Insert(task);
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

        // GET: Tasks/Edit/5
        public ActionResult Edit(int id)
        {
            var model = TaskMapper.MapItemToViewModel(unitOfWork.TasksRepository.GetByID(id));
            return View(model);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        public ActionResult Edit(TaskViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.StartDate > model.EndDate)
                    {
                        ModelState.AddModelError("StartDate", "Start date cannot be later than end date of the task");
                        return View(model);
                    }
                    var task = TaskMapper.MapViewModelToItem(model);
                    task.updated = DateTime.Today;
                    unitOfWork.TasksRepository.Update(task);
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

        // GET: Tasks/Delete/5
        public ActionResult Delete(int id)
        {
            var task = unitOfWork.TasksRepository.GetByID(id);
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            unitOfWork.TasksRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }


        public ActionResult EditPartial(int id)
        {
            TaskViewModel model = TaskMapper.MapItemToViewModel(unitOfWork.TasksRepository.GetByID(id));
            return PartialView("_Edit", model);
        }

        public ActionResult Appoint(int id)
        {
            AppointViewModel model = new AppointViewModel();
            model.Task = TaskMapper.MapItemToViewModel(unitOfWork.TasksRepository.GetByID(id));

            var rits = unitOfWork.ResInTasksRepository.Get(x => x.task_id == id, includeProperties: "Resource,Task");
            if (rits != null)
                model.Resources = AppointMapper.MapItemsToViewModel(rits);
            else
            {
                model.Resources = new List<ResourceInTaskVM>();
            }

            model.AllResources = ResourceMapper.MapItemToViewModel(unitOfWork.ResourcesRepository.Get()
                    .Where(r => !model.Resources.Any(vm => vm.Resource.Id == r.resource_id)));

            return PartialView("_Appoint", model);
        }
        [HttpPost]
        public ActionResult Generate(List<AppointDTO> model)
        {
            int task_id = 0;
            if (model.Count != 0)
            {
                task_id = int.Parse(model.First().task_id);

                foreach (var appointment in model)
                {
                    /*
                     * 1. поищем в бд такую запись, который имеет предоставленный таск айди и ресурс айди
                     * 2. если поиск дал результат, и флажок отмечен (status==true): то обновляем даты аппойнтментов найденной записи
                     * 3. если поиск дал результат, но флажок не отмечен: то удаляем найденную запись в бд
                     * 4. если поиск не дал результатов, но флажок отмечен : добавляем новую запись в бд
                     * 5. если поиск не дал результатов, и флажок не отмечен : ничего не делаем
                     * 6. коммитим все изменения в базу
                     */

                    var resource_id = int.Parse(appointment.resource_id);

                    //1
                    var found = unitOfWork.ResInTasksRepository.Get(x => x.resource_id == resource_id && x.task_id == task_id).SingleOrDefault();

                    if (appointment.status.ToLower().Equals("false"))
                    {
                        if (found != null)  //3
                            unitOfWork.ResInTasksRepository.Delete(found);
                    }
                    else
                    {
                        if (found != null) //2
                        {
                            found.date_from = DateTime.ParseExact(appointment.start_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            found.date_to = DateTime.ParseExact(appointment.end_date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            unitOfWork.ResInTasksRepository.Update(found);
                        }
                        else //4
                        {
                            unitOfWork.ResInTasksRepository.Insert(AppointMapper.MapViewModelToItem(appointment));
                        }
                    }
                }

                unitOfWork.Save(); //6
            }

            return Json(Url.Action("Generated", "Tasks", new { id = task_id }));
            //@"TODO: return url.action('Generated', 'Tasks', task_id) | and use this url in 'window.href.location' part of the ajax.success in the view");
        }

        public ActionResult Generated(int id, bool json = false)
        {
            var rits = unitOfWork.ResInTasksRepository.Get(x => x.task_id == id, includeProperties: "Resource,Task");
            if (json)
            {
                var counter = 0;
                Random r = new Random();
                List<object> result = new List<object>();
                foreach (var item in rits)
                {
                    counter++;
                    result.Add(new
                    {
                        pID = item.id.ToString(),
                        pName = item.Task.name,
                        pStart = item.date_from.ToString("yyyy-MM-dd"),
                        pEnd = item.date_to.ToString("yyyy-MM-dd"),
                        pColor = r.Next(counter).ToString("D2") + r.Next(counter).ToString("D2") + r.Next(counter).ToString("D2"),
                        pRes = item.Resource.first_name + " " + item.Resource.last_name,
                        pComp = "0",
                        pParent = "0"
                    });
                }
                return Json(result);
            }
            ViewBag.TaskName = rits.FirstOrDefault().Task.name;
            return View(id);
        }

        public ActionResult Appointments()
        {
            //var model = AppointMapper.MapAllItemsToViewModel(
            //    unitOfWork.ResInTasksRepository.Get(includeProperties: "Resource,Task"), 
            //    unitOfWork.ResourcesRepository.Get());

            var model = new List<AppointViewModel>();
            var rits = unitOfWork.ResInTasksRepository.Get(includeProperties: "Resource,Task");
            foreach (var item in rits.GroupBy(rit => rit.task_id).Select(g => g.First()))
            {
                IEnumerable<ResourceInTaskVM> resources = rits.Where(rit => rit.task_id == item.task_id).Select(x => AppointMapper.MapItemToViewModel(x)).ToList();
                model.Add(new AppointViewModel
                {
                    Task = TaskMapper.MapItemToViewModel(item.Task),
                    Resources = resources.ToList(),
                    AllResources = ResourceMapper.MapItemToViewModel(unitOfWork.ResourcesRepository.Get().Where(r => !resources.Any(vm => vm.Resource.Id == r.resource_id)))
                });
            }

            return View(model);
        }

    }
}
