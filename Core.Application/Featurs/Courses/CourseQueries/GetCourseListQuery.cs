using Core.Application.DTOs.Course;
using Core.Domain.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Featurs.Courses.CourseQueries
{
    public class GetCourseListQuery : IRequest<Response<List<CourseResponse>>>
    {
    }
}
