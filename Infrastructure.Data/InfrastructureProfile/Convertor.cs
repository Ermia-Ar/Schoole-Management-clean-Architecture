using Core.Domain.Entities;
using System.Net.Http.Headers;

namespace Infrastructure.Data.InfrastructureProfile
{
    public static class Convertor
    {
        public static TeacherCourse? ConvertToTeacher(Entities.Teacher teacher)
        {
            if (teacher == null)
                return null;

            return new TeacherCourse()
            {
                FullName = teacher.FullName,
                PhoneNumber = teacher.PhoneNumber,
            };
        }
    }
}
