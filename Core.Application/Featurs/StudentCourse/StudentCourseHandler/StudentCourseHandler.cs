using AutoMapper;
using Core.Application.DTOs.Course;
using Core.Application.DTOs.StudentCourse;
using Core.Application.Featurs.StudentCourse.StudentCourseCommands;
using Core.Application.Featurs.StudentCourse.StudentCourseQueries;
using Core.Application.Interfaces;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.StudentCourse.StudentCourseHandler
{
    public class StudentCourseHandler : ResponseHandler
        ,IRequestHandler<RegisterInCourseCommand, Response<string>>
        ,IRequestHandler<GetCourseStudentListQuery , Response<List<StudentCourseResponse>>>
        , IRequestHandler<GetCoursesByStudentIdQuery, Response<List<CourseResponse>>>

    {
        private IStudentCourseServices _studentCourseServices { get; set; }
        private IMapper _mapper { get; set; }

        public StudentCourseHandler(IStudentCourseServices studentCourseServices, IMapper mapper)
        {
            _studentCourseServices = studentCourseServices;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(RegisterInCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _studentCourseServices.RegisterInCourse(request.StudentCourse);
            if (result)
                return Created("");
            else
                return UnprocessableEntity<string>("code melly or password is wrong");
        }

        public async Task<Response<List<StudentCourseResponse>>> Handle(GetCourseStudentListQuery request, CancellationToken cancellationToken)
        {
            var result = await _studentCourseServices.GetCourseStudentList();
            
            var studentCourseResponse = _mapper.Map<List<StudentCourseResponse>>(result);

            return Success(studentCourseResponse);
        }

        public async Task<Response<List<CourseResponse>>> Handle(GetCoursesByStudentIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _studentCourseServices.GetCoursesByStudentId(request.Id);

            var coursesResponse = _mapper.Map<List<CourseResponse>>(result);

            return Success(coursesResponse);

        }
    }
}
