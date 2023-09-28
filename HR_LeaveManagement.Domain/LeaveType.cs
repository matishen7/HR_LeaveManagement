namespace HR_LeaveManagement.Domain
{
    public class LeaveType
    {
        public int Id { get; set; }
        public int DefaultDays { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}