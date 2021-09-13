using Microsoft.Extensions.Logging;
using MVCTesterCPI2.Infrastructure.Interfaces;
using MVCTesterCPI2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MVCTesterCPI2.Infrastructure
{
    public partial class EntityUpdater : IUpdater
    {
        private readonly IAthenaeum _athen;
        private readonly ILogger _logger;
        
        public enum TypeOfEntity { Project, Program, User, Assessment, Model }

        public EntityUpdater(IAthenaeum athenaeum)
        {
            _athen = athenaeum;
            // _logger = logger;
        }

        public async Task UpdateEntity()
        {
            // updating project
            await UpdateMultipleProjects(TypeOfEntity.Project);
        }
    }
}