namespace HR_LeaveManagement.Domain
{
    public abstract class BaseEntity
    {
        public DateTime? EndDate { get; set; }
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
    }
}