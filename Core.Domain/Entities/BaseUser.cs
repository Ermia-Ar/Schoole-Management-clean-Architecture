namespace Core.Domain.Entities
{
    public class BaseUser
    {
        public string Id { get; set; }

        public string ApplicationUserId { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public DateTime Birthday { get; set; }

        public string CodeMelly { get; set; }

        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }
    }
}
