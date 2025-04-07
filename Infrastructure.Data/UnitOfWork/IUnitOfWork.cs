using Infrastructure.Data.Interfaces;

namespace SchoolProject.Infrustructure.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
	{
		IUserRepository Users { get; }
		IStudentRepository Students { get; }
		IStudentCourseRepository StudentsCourse { get; }
		ITeacherRepository Teachers { get; }
		IAdminRepository Admins { get; }
		ICourseRepository Courses { get; }
		int Complete();
	}
}
