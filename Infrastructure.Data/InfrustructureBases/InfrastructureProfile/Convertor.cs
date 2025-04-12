using Core.Domain.Entities;

namespace Infrastructure.Data.InfrustructureBases.InfrastructureProfile
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
