using MVCTesterCPI2.Infrastructure.DbSetup;
using MVCTesterCPI2.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace MVCTesterCPI2.Infrastructure
{
    public partial class EntityUpdater
    {
        int offset = 0;
        private readonly CpiClientBase cpiClientBase;
        private readonly string cpiBaseUri = ConfigurationManager.AppSettings["CpiBaseUri"];
        private readonly string cpiProjectsUri = ConfigurationManager.AppSettings["CpiProjectsUri"];
        private readonly string cpiProjectUri = ConfigurationManager.AppSettings["CpiProjectUri"];

        public async Task<List<CpiProject>> GetCPIProjects()
        {
            List<Task> taskList = new List<Task>();
            List<CpiProject> cpiProjList = new List<CpiProject>();
            HttpResponse<List<CpiProject>> projResponse = new HttpResponse<List<CpiProject>>();
            projResponse = await _athen.GetCPIProjectsList(cpiProjectsUri, Authorization.userID.ToString(), null, offset);
            if (!projResponse.IsSuccess)
            {
                Debug.WriteLine($"Failed getting CPI Projects on offset {offset}");
                LoggerLQ.LogQueue($"Failed getting CPI Projects on offset {offset}");
            }
            if (projResponse.ResponseContent != null)
            {
                cpiProjList = projResponse.ResponseContent;
            }
            else
            {
                Debug.WriteLine($"Null projResponse.ResponseContent at GetCPIProjects({offset}");
                LoggerLQ.LogQueue($"Null projResponse.ResponseContent at GetCPIProjects({offset}");
            }
            if (cpiProjList.Count > 0)
            {
                return cpiProjList;
            }
            else
            {
                return new List<CpiProject>();
            }
        }

        public async Task<CpiProject> GetSingleProject(int projId)
        {
            CpiProject project = new CpiProject();
            HttpResponse<CpiProject> projResponse = new HttpResponse<CpiProject>();
            projResponse = await _athen.GetCPIProject(cpiProjectUri, projId, Authorization.userID.ToString(), null, 0);
            if (!projResponse.IsSuccess)
            {
                Debug.WriteLine($"Failed getting CPI Projects on offset {offset}");
                LoggerLQ.LogQueue($"Failed getting CPI Projects on offset {offset}");
            }
            if (projResponse.ResponseContent != null)
            {
                project = projResponse.ResponseContent;
            }
            else
            {
                Debug.WriteLine($"Null projResponse.ResponseContent at GetCPIProjects({offset}");
                LoggerLQ.LogQueue($"Null projResponse.ResponseContent at GetCPIProjects({offset}");
            }
            if (project != null)
            {
                return project;
            }
            else
            {
                return new CpiProject();
            }
        }

        public async Task EditSingleProject(CpiProject project)
        {
            var updateResponse = await _athen.EditProject(cpiProjectUri, project);
            if (updateResponse.HttpRespMsg.IsSuccessStatusCode)
            {
                LoggerLQ.LogQueue($"Successfully updated CPI Project {project.Id}.");
            }
            else
            {
                LoggerLQ.LogQueue($"Failed updating CPI Project {project.Id} - {updateResponse.Message}");
            }
        }

        public async Task UpdateMultipleProjects(TypeOfEntity typeOfEntity)
        {
            List<Task> taskList = new List<Task>();
            Athenaeum _athen = new Athenaeum(cpiClientBase);
            List<CpiProject> cpiProjList = new List<CpiProject>();
            HttpResponse<List<CpiProject>> projResponse = new HttpResponse<List<CpiProject>>();
            projResponse = await _athen.GetCPIProjectsList(ConfigurationManager.AppSettings["CpiProjectsUri"], Models.Authorization.userID.ToString(), null, offset);
            if (!projResponse.IsSuccess)
            {
                Debug.WriteLine($"Failed getting CPI Projects list on offset {offset}");
                LoggerLQ.LogQueue($"Failed getting  CPI Projects list on offset {offset}");
            }
            if (projResponse.ResponseContent != null)
            {
                cpiProjList = projResponse.ResponseContent;
            }
            else
            {
                Debug.WriteLine($"Null projResponse.ResponseContent at GetCPIProjects({offset}");
                LoggerLQ.LogQueue($"Null projResponse.ResponseContent at GetCPIProjects({offset}");
            }
            if (cpiProjList.Count > 0)
            {
                foreach (var CpiProject in cpiProjList)
                {
                    try
                    {
                        var result = UpdateProject(CpiProject);
                        if (result != null)
                        {
                            taskList.Add(result);
                        }
                        else
                        {
                            Debug.WriteLine($"Null result at RunEdfiOdsSync()");
                            LoggerLQ.LogQueue($"Null result at RunEdfiOdsSync()");
                        }
                    }
                    catch (Exception exc)
                    {
                        Debug.WriteLine($"Exception in RunEdfiOdsSync() for enrollment-changes; {exc.Message}");
                        throw;
                    }
                }
            }
            //Await all tasks
            try
            {
                Task batchTask = Task.WhenAll(taskList);
                Console.WriteLine("Main thread done");
                batchTask.Wait();
            }
            catch (Exception e)
            {
                LoggerLQ.LogQueue($"Error in synchronizing batch {batchNumber} - {e.Message}");
            }

        }
    }
}
