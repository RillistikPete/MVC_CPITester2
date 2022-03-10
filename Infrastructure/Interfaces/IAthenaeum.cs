using MVCTesterCPI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCTesterCPI2.Infrastructure.Interfaces
{
    public interface IAthenaeum
    {
        /// <summary>
        /// Logs in for token
        /// </summary>
        /// <returns>Login Object</returns>
        Task<HttpResponse<Login>> Login(string resourceUri, Models.CPIUser userObj, IDictionary<string, string> properties);

        /// <summary>
        /// Gets a list of CPI Projects specified by userID.
        /// </summary>
        /// <param name="userID">Unique id of user</param>
        /// <returns>List of CPI Projects</returns>
        Task<HttpResponse<List<CpiProject>>> GetCPIProjectsList(string resourceUri, string userID, IDictionary<string, string> properties, int offset);

        /// <summary>
        /// Gets a CPI Project specified by projectID and userID.
        /// </summary>
        /// <param name="userID">Unique id of user</param>
        /// <param name="projID">Unique id of project</param>
        /// <returns>CPI Project</returns>
        Task<HttpResponse<CpiProject>> GetCPIProject(string resourceUri, int projId, string userID, IDictionary<string, string> properties, int offset);

        //=================================CRUD=================================================

        /// <summary>
        /// Updates CPI project via API
        /// </summary>
        /// <param name="id">Record Id</param>
        /// <param name="dto">CPI Project DTO</param>
        /// <returns></returns>
        Task<ServerResponse> EditProject(string resourceUri, CpiProject dto);

        //==================================Sprocs===============================================
        /// <summary>
        /// Runs main:DataRetentionPolicy
        /// </summary>
        /// <returns>Task</returns>
        /// Task<DataRetentionPolicy> RunDataRetentionPolicyOnComplete();

        /// <summary>
        /// Runs main:updGFIonPersons
        /// </summary>
        /// <returns>Task</returns>
        /// Task<UpdGFIonPersonsPolicy> RunUpdGFIonPersons();
    }
}
