using MVCTesterCPI2.Infrastructure;
using MVCTesterCPI2.Infrastructure.Interfaces;
using MVCTesterCPI2.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI;

namespace MVCTesterCPI2.Controllers
{
    public class ApiTesterController : Controller
    {
        int offset = 0;
        private readonly CpiClientBase cpiClientBase;
        private static Uri baseUri = Models.Authorization._cpiClient.BaseAddress;
        private static AuthenticationHeaderValue headerValue = Models.Authorization._cpiClient.DefaultRequestHeaders.Authorization;
        public static HttpClient _cpiClient = new HttpClient()
        {
            // BaseAddress = baseUri
        };
        public ApiTesterController(HttpClient client)
        {
            _cpiClient = client;
        }

        //public async Task<List<CpiProject>> GetCPIProjects()
        //{
        //    List<Task> taskList = new List<Task>();
        //    Athenaeum _athen = new Athenaeum(cpiClientBase);
        //    List<CpiProject> cpiProjList = new List<CpiProject>();
        //    HttpResponse<List<CpiProject>> projResponse = new HttpResponse<List<CpiProject>>();
        //    projResponse = await _athen.GetCPIProjectsList(Models.Authorization.userID.ToString(), null, offset);
        //    if (!projResponse.IsSuccess)
        //    {
        //        Debug.WriteLine($"Failed getting IC enrollment changes on offset {offset}");
        //        // LoggerLQ.LogQueue($"Failed getting IC enrollment changes on offset {offset}");
        //    }
        //    if (projResponse.ResponseContent != null)
        //    {
        //        cpiProjList = projResponse.ResponseContent;
        //    }
        //    else
        //    {
        //        Debug.WriteLine($"Null projResponse.ResponseContent at GetCPIProjects({offset}");
        //        // LoggerLQ.LogQueue($"Null projResponse.ResponseContent at GetCPIProjects({offset}");
        //    }
        //    if (cpiProjList.Count > 0)
        //    {
        //        return cpiProjList;
        //    }
        //    else
        //    {
        //        return new List<CpiProject>();
        //    }
        //}

        //public async Task UpdateMultipleProjects()
        //{
        //    List<Task> taskList = new List<Task>();
        //    Athenaeum _athen = new Athenaeum(cpiClientBase);
        //    List<CpiProject> cpiProjList = new List<CpiProject>();
        //    HttpResponse<List<CpiProject>> projResponse = new HttpResponse<List<CpiProject>>();
        //    projResponse = await _athen.GetCPIProjectsList(Models.Authorization.userID.ToString(), null, offset);
        //    if (!projResponse.IsSuccess)
        //    {
        //        Debug.WriteLine($"Failed getting IC enrollment changes on offset {offset}");
        //        // LoggerLQ.LogQueue($"Failed getting IC enrollment changes on offset {offset}");
        //    }
        //    if (projResponse.ResponseContent != null)
        //    {
        //        cpiProjList = projResponse.ResponseContent;
        //    }
        //    else
        //    {
        //        Debug.WriteLine($"Null projResponse.ResponseContent at GetCPIProjects({offset}");
        //        // LoggerLQ.LogQueue($"Null projResponse.ResponseContent at GetCPIProjects({offset}");
        //    }
        //    if (cpiProjList.Count > 0)
        //    {
        //        foreach (var cpiProject in cpiProjList)
        //        {
        //            try
        //            {
        //                var result = EntityUpdater.UpdateEntity(cpiProject);
        //                var result = "";
        //                if (result != null)
        //                {
        //                    taskList.Add(result);
        //                }
        //                else
        //                {
        //                    Debug.WriteLine($"Null result at RunEdfiOdsSync()");
        //                    // LoggerLQ.LogQueue($"Null result at RunEdfiOdsSync()");
        //                }
        //            }
        //            catch (Exception exc)
        //            {
        //                Debug.WriteLine($"Exception in RunEdfiOdsSync() for enrollment-changes; {exc.Message}");
        //                throw;
        //            }
        //        }
        //    }
        //    //Await all tasks
        //    try
        //    {
        //        Task batchTask = Task.WhenAll(taskList);
        //        Console.WriteLine("Main thread done");
        //        batchTask.Wait();
        //    }
        //    catch (Exception e)
        //    {
        //        //_logger.LogInformation($"Error in synchronizing batch {batchNumber} of enrollments. {e.Message}");
        //        //LoggerLQ.LogQueue($"Error in synchronizing batch {batchNumber} - {e.Message}");
        //    }
        //    //try
        //    //{
        //    //    var baseUri = 
        //    //    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUri + ConfigurationManager.AppSettings["CpiProjectsUri"] + Models.Authorization.userID.ToString());
        //    //    request.Headers.Add("Authorization", "Bearer " + Models.Authorization._cpiToken);
        //    //    HttpResponseMessage result = await _cpiClient.SendAsync(request);
        //    //    //if (result.IsSuccessStatusCode)
        //    //    //{
        //    //    //    var content = result.Content.ReadAsStringAsync().Result;
        //    //    //    reJ = JsonConvert.DeserializeObject<CPIProject>(content);
        //    //    //    CPIProject cpiProj = new CPIProject();
        //    //    //    JsonConvert.PopulateObject(cpiProj, reJ);
        //    //    //    return cpiProj;
        //    //    //}
        //    //}
        //    //catch (Exception exc)
        //    //{
        //    //    if (exc.GetType().IsSubclassOf(typeof(Exception)))
        //    //        throw;
        //    //    Debug.WriteLine(exc.Message);
        //    //}
        //}

        //public async Task UpdateProject()
        //{

        //}

    }
}
