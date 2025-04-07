using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Courses.CourseCommands
{
    public class DeleteCourseCommand : IRequest<Response<string>>
    {
        public string Id { get; set; }
    }
}
