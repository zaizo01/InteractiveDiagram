using DIGEIG.Domain.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DIGEIG.Domain.Entities
{
    public class Sys_Tb_InstitutionsStructure : Audit
    {
        public int InstitutionStructureId { get; set; } 
        public int InstitutionId { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; } 
        public int? MainInstitutionStructureId { get; set; }

        public List<Sys_Tb_InstitutionsStructure> ListInstitutionsStructure = new();
    }
}
