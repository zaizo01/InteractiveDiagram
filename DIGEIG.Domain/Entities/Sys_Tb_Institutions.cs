using DIGEIG.Domain.Common;

namespace DIGEIG.Domain.Entities
{
    public class Sys_Tb_Institutions: Audit
    {
        public int InstitutionId { get; set; } 
        public string Name { get; set; }
        public int ReferenceID { get; set; } 
    }
}
