namespace TMA.Application.DTOs
{
    public class BaseDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedByEmail { get; set; }

        public bool? IsActive { get; set; } = true;

        public bool? IsDeleted { get; set; } = false;
    }
}
