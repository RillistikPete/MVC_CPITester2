using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTesterCPI2.Models
{
    public partial class CpiProject
    {
        [JsonProperty("ID")]
        public long Id { get; set; }

        [JsonProperty("CurrentUserID")]
        public long CurrentUserId { get; set; }

        [JsonProperty("CreatedDate")]
        public DateTimeOffset CreatedDate { get; set; }

        [JsonProperty("LastEditDate")]
        public DateTimeOffset LastEditDate { get; set; }

        [JsonProperty("LastEditBy")]
        public string LastEditBy { get; set; }

        [JsonProperty("ProgramID")]
        public long ProgramId { get; set; }

        [JsonProperty("ProjectName")]
        public string ProjectName { get; set; }

        [JsonProperty("VersionIncrementSpiral")]
        public string VersionIncrementSpiral { get; set; }

        [JsonProperty("DODDeptSvcAgncy")]
        public string DodDeptSvcAgncy { get; set; }

        [JsonProperty("SyscomOrgLab")]
        public string SyscomOrgLab { get; set; }

        [JsonProperty("ProgramOffice")]
        public string ProgramOffice { get; set; }

        [JsonProperty("ProgramOfficePOC")]
        public string ProgramOfficePoc { get; set; }

        [JsonProperty("ACAT")]
        public string Acat { get; set; }

        [JsonProperty("EffortType")]
        public string EffortType { get; set; }

        [JsonProperty("ProjClass")]
        public int ProjClass { get; set; }

        [JsonProperty("ScientificResearchSurveyA")]
        public bool? ScientificResearchSurveyA { get; set; }

        [JsonProperty("ScientificResearchSurveyB")]
        public bool? ScientificResearchSurveyB { get; set; }

        [JsonProperty("ScientificResearchSurveyC")]
        public bool? ScientificResearchSurveyC { get; set; }

        [JsonProperty("ScientificResearchSurveyD")]
        public bool? ScientificResearchSurveyD { get; set; }

        [JsonProperty("ScientificResearchDefnA")]
        public string ScientificResearchDefnA { get; set; }

        [JsonProperty("ScientificResearchDefnB")]
        public string ScientificResearchDefnB { get; set; }

        [JsonProperty("ScientificResearchDefnC")]
        public string ScientificResearchDefnC { get; set; }

        [JsonProperty("ScientificResearchDefnD")]
        public string ScientificResearchDefnD { get; set; }

        [JsonProperty("ScientificResearchDefnE")]
        public string ScientificResearchDefnE { get; set; }

        [JsonProperty("CPISurveyA")]
        public bool? CpiSurveyA { get; set; }

        [JsonProperty("CPISurveyA1")]
        public bool? CpiSurveyA1 { get; set; }

        [JsonProperty("CPISurveyA2")]
        public bool? CpiSurveyA2 { get; set; }

        [JsonProperty("CPISurveyB")]
        public bool? CpiSurveyB { get; set; }

        [JsonProperty("CPISurveyC")]
        public bool? CpiSurveyC { get; set; }

        [JsonProperty("CPISurveyD")]
        public bool? CpiSurveyD { get; set; }

        [JsonProperty("CPISurveyE")]
        public bool? CpiSurveyE { get; set; }

        [JsonProperty("CPISurveyF")]
        public bool? CpiSurveyF { get; set; }

        [JsonProperty("CPISurveyG")]
        public bool? CpiSurveyG { get; set; }

        [JsonProperty("CPISurveyH")]
        public bool? CpiSurveyH { get; set; }
    }
}