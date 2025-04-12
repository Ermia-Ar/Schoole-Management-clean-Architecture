using AutoMapper;
using Core.Application.DTOs.Course;
using Core.Application.Featurs.Courses.CourseCommands;
using Core.Application.Featurs.Courses.CourseQueries;
using Core.Application.Interfaces;
using Core.Application.Wrapper;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Courses.CourseHandlers
{
    public class CourseHandler : ResponseHandler
        , IRequestHandler<AddCourseCommand, Response<string>>
        , IRequestHandler<GetCourseListQuery, Response<PaginatedResult<CourseResponse>>>
        , IRequestHandler<DeleteCourseCommand, Response<string>>
        , IRequestHandler<GetCourseByIdQuery, Response<CourseResponse>>
        , IRequestHandler<GetTeacherCoursesByIdQuery, Response<PaginatedResult<CourseResponse>>>

    {
        private ICourseServices _courseServices { get; set; }
        private IMapper _mapper { get; set; }

        public CourseHandler(ICourseServices courseServices, IMapper mapper)
        {
            _courseServices = courseServices;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var result = await _courseServices.AddCourseAsync(request.Course);

            if (result)
                return Created("");
            else
                return UnprocessableEntity<string>("");
        }

        public async Task<Response<PaginatedResult<CourseResponse>>> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseServices.GetCourseListAsync();

            var courseResponse = _mapper.Map<List<CourseResponse>>(courses);
            var paginated = await courseResponse.AsQueryable()
                .ToPaginatedListAsync(request.pageNumber, request.pageSize);

            return Success(paginated);
        }

        public async Task<Response<string>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            //delete courses from course table
            var result = await _courseServices.DeleteCourseAsync(request.Id);

            if (result)
                return Deleted("");
            else
                return NotFound<string>();
        }

        public async Task<Response<CourseResponse>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _courseServices.GetCourseById(request.Id);

            if (result != null)
            {
                var course = _mapper.Map<CourseResponse>(result);
                return Success(course);
            }
            else
                return NotFound<CourseResponse>();
        }

        public async Task<Response<PaginatedResult<CourseResponse>>> Handle(GetTeacherCoursesByIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseServices.GetTeacherCoursesById(request.Id);

            var coursesResponse = _mapper.Map<List<CourseResponse>>(courses);

            var coursesPaginated = await coursesResponse.AsQueryable()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return Success(coursesPaginated);

        }
    }
}
