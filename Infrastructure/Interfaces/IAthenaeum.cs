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

        //=================================CRUD=================================================

        /// <summary>
        /// Updates Student Badge Record in the Badge System
        /// </summary>
        /// <param name="id">Record Id</param>
        /// <param name="dto">Student Badge DTO</param>
        /// <returns></returns>
        /// Task<ServerResponse> UpdateStudentBadge(string id, BadgeStudent dto);

        /// <summary>
        /// Creates a new Student Badge Record in the Badge System
        /// </summary>
        /// <param name="badgeStudent"></param>
        /// <returns></returns>
        /// Task<ServerResponse> CreateStudentBadge(BadgeStudent badgeStudent);

        /// <summary>
        /// Creates a new StudentHistorical Object Record in the Badge System
        /// </summary>
        /// <param name="shObj"></param>
        /// <returns>Server Response</returns>
        /// Task<ServerResponse> PostToStudentHistoricals(StudentHistorical shObj);

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