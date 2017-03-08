using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gantt.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gantt.Web.Models.ViewModels;
using Gantt.Web.Mappers;
using Gantt.Web.DAL;

namespace Gantt.Web.Controllers.Tests
{
    [TestClass()]
    public class TasksControllerTests
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        [TestMethod()]
        public void AppointTest()
        {
            var rits = unitOfWork.ResInTasksRepository.Get(/*includeProperties: "Resource,Task"*/);
            //AppointViewModel model = AppointMapper.MapItemsToViewModel(rits);
            Assert.Fail();
        }
    }
}