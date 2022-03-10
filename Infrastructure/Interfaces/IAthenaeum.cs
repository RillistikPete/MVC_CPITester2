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
        /// Gets an CPI Project specified by userID.
        /// </summary>
        /// <param name="userID">Unique id of user</param>
        /// <returns>List of CPI Projects</returns>
        Task<HttpResponse<List<CpiProject>>> GetCPIProjectsList(string resourceUri, string userID, IDictionary<string, string> properties, int offset);

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
