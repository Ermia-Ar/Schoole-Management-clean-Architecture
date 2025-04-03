namespace Core.Domain.Entities
{
    public class Teacher : BaseUser
    {
        public decimal Salary { get; set; }

        public Subjects Subject { get; set; }
    }
}
