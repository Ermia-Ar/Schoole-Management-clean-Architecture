namespace Core.Domain.Entities
{
    public class Student : BaseUser
    {
        public Grade Grade { get; set; }
    }
}
