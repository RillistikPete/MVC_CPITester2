using MVCTesterCPI2.Infrastructure.Interfaces;
using MVCTesterCPI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCTesterCPI2.Infrastructure
{
    public class Athenaeum : IAthenaeum
    {
        private readonly CpiClientBase _cpiClient;
        public Athenaeum(CpiClientBase cpiClient)
        {
            _cpiClient = cpiClient;
        }

        #region AuthenticateResponse
        public async Task<HttpResponse<Login>> Login(string resourceUri, Models.CPIUser userObj, IDictionary<string, string> properties)
        {
            return await _cpiClient.Login<Login>(resourceUri, userObj, properties);
        }
        #endregion

        #region Implementation
        public async Task<HttpResponse<List<CpiProject>>> GetCPIProjectsList(string resourceUri, string userID, IDictionary<string, string> properties, int offset)
        {
            return await _cpiClient.GetListById<CpiProject>(resourceUri, userID, properties);
        }

        //public async Task<HttpResponse<CpiProject>> GetEdfiStudentByStudentUniqueId(string resourceUri, string stdUniqueId, IDictionary<string, string> properties)
        //{
        //    return await _cpiClient.GetSingle<CpiProject>(resourceUri, stdUniqueId, properties);
        //}

        #endregion

        #region CRUD
        //public async Task<ServerResponse> UpdateStudentBadge(string id, BadgeStudent dto)
        //{
        //    return await _cpiClient.Put("studentBadges/", id, dto);
        //}

        //public async Task<ServerResponse> CreateStudentBadge(BadgeStudent badgeStudent)
        //{
        //    return await _cpiClient.Post("studentBadges/", badgeStudent);
        //}

        //public async Task<ServerResponse> PostToStudentHistoricals(StudentHistorical shObj)
        //{
        //    return await _cpiClient.Post("student-historicals/", shObj);
        //}
        #endregion

        #region Data Retention Policy and GFI
        //public async Task<DataRetentionPolicy> RunDataRetentionPolicyOnComplete()
        //{
        //    return await _cpiClient.GetData("main:DataRetentionPolicy");
        //}

        //public async Task<UpdGFIonPersonsPolicy> RunUpdGFIonPersons()
        //{
        //    return await _cpiClient.GetGFI("main:updGFIonPersons");
        //}
        #endregion
    }
}