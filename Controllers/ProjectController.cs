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
 
        public async Task<ActionResult> EditProject(int id)
        {
            EntityUpdater updater = new EntityUpdater(new Athenaeum(new CpiClientBase(Authorization._cpiClient)));
            Models.CpiProject project = new Models.CpiProject();
            project = await updater.GetSingleProject(id);
            return View(project);
        }

        [HttpPost]
        public async Task<ActionResult> EditProject(CpiProject projectModel)
        {
            CpiProject dto = new CpiProject
            {
                Id = projectModel.Id,
                ProjectName = projectModel.ProjectName,
                VersionIncrementSpiral = projectModel.VersionIncrementSpiral,
                LastEditBy = projectModel.LastEditBy
            };
            EntityUpdater updater = new EntityUpdater(new Athenaeum(new CpiClientBase(Authorization._cpiClient)));
            await updater.EditSingleProject(dto);
            return RedirectToAction("DisplayUserProjects");
        }
    }
}
