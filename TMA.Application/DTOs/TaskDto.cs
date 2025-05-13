using TMA.Domain.Models.Enums;

namespace TMA.Application.DTOs
{
    public class TaskDto : BaseDto
    {
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public TaskStatusEnum? Status { get; set; }
    }
}
