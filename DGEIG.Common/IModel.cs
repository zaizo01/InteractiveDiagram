using System;

namespace DGEIG.Common
{
    public interface IModel
    {
        public Guid Id { get; set; }
        public string LastModifyBy { get; set; }
        public string CreateBy { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public string Commentary { get; set; }
    }
}
