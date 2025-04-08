using AutoMapper;
using Core.Application.DTOs.Course;
using Core.Application.DTOs.Student.StudentDtos;
using Core.Application.DTOs.StudentCourse;
using Core.Application.Featurs.StudentCourse.StudentCourseCommands;
using Core.Application.Featurs.StudentCourse.StudentCourseQueries;
using Core.Application.Interfaces;
using Core.Application.Wrapper;
using Core.Domain.Bases;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Core.Application.Featurs.StudentCourse.StudentCourseHandler
{
    public class StudentCourseHandler : ResponseHandler
        ,IRequestHandler<RegisterInCourseCommand, Response<string>>
        ,IRequestHandler<GetCourseStudentListQuery , Response<PaginatedResult<StudentCourseResponse>>>
        , IRequestHandler<GetCoursesByStudentIdQuery, Response<PaginatedResult<CourseResponse>>>

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
                return UnprocessableEntity<string>("Course id is wrong !");
        }

        public async Task<Response<PaginatedResult<StudentCourseResponse>>> Handle(GetCourseStudentListQuery request, CancellationToken cancellationToken)
        {
            var result = await _studentCourseServices.GetCourseStudentList();
            
            var studentCourseResponse = _mapper.Map<List<StudentCourseResponse>>(result);
            var paginated = await studentCourseResponse.AsQueryable()
                .ToPaginatedListAsync(request.pageNumber, request.pageSize);

            return Success(paginated);
        }

        public async Task<Response<PaginatedResult<CourseResponse>>> Handle(GetCoursesByStudentIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _studentCourseServices.GetCoursesByStudentId(request.Id);

            var coursesResponse = _mapper.Map<List<CourseResponse>>(result);
            var paginated = await coursesResponse.AsQueryable()
                .ToPaginatedListAsync(request.pageNumber, request.pageSize);

            return Success(paginated);

        }
    }
}
