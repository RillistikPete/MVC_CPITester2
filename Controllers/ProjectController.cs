using MVCTesterCPI2.Infrastructure;
using MVCTesterCPI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCTesterCPI2.Controllers
{
    public class ProjectController : Controller
    {
        // GET: CPIProjects
        public async Task<ActionResult> DisplayUserProjects()
        {
            EntityUpdater updater = new EntityUpdater(new Athenaeum(new CpiClientBase(Authorization._cpiClient)));
            List<Models.CpiProject> projectList = new List<Models.CpiProject>();
            // projectList = await _entityUpdater.GetCPIProjects();
            projectList = await updater.GetCPIProjects();
            return View(projectList);
        }

        // POST: CPIProject
        public ActionResult CreateProject()
        {
            return View();
        }
    }
}