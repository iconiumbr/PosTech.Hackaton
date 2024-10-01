using System.ComponentModel.DataAnnotations;

namespace Domain.Shared
{
    public abstract class BaseAuditableEntity : BaseEntity
    {
        public DateTimeOffset Created { get; set; }

        [MaxLength(100)]
        public string CreatedBy { get; set; }

        public DateTimeOffset LastModified { get; set; }

        [MaxLength(100)]
        public string LastModifiedBy { get; set; }
    }
}
