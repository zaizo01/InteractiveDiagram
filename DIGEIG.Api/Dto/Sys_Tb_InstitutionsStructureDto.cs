using DIGEIG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIGEIG.Api.Dto
{
    public class Sys_Tb_InstitutionsStructureDto
    {
        public int InstitutionStructureId { get; set; }
        public int InstitutionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? MainInstitutionStructureId { get; set; }

        public List<Sys_Tb_InstitutionsStructure> ListInstitutionsStructure = new();
    }
}
