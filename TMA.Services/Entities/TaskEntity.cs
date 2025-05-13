using TMA.Domain.Models.Enums;

namespace TMA.Domain.Entities
{
    public class TaskEntity : BaseEntity
    {
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public TaskStatusEnum? Status { get; set; }
    }
}
