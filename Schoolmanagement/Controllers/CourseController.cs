using Core.Application.DTOs.Course;
using Core.Application.Featurs.Courses.CourseCommands;
using Core.Application.Featurs.Courses.CourseQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Management.Api.Base;

namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : AppControllerBase
    {
        private IMediator _mediator { get; set; }

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCourse([FromBody] AddCourseRequest course)
        {
            var request = new AddCourseCommand { Course = course };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpGet]
        [Route("GetListOfCourses")]
        //[Authorize(Roles = "Student")]
        public async Task<IActionResult> GetCourseList(int pageSize, int pageNumber)
        {
            var request = new GetCourseListQuery() { pageNumber = pageNumber, pageSize = pageSize };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpDelete]
        [Route("{id:guid}DeleteCourse")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var request = new DeleteCourseCommand { Id = id.ToString() };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpGet]
        [Route("{id:guid}FindCourseById")]
        //[Authorize(Roles = "Admin,Student,Teacher")]
        public async Task<IActionResult> GetCourseById([FromRoute] Guid id)
        {
            var request = new GetCourseByIdQuery { Id = id.ToString() };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }


        [HttpGet]
        [Route("{id:guid}FindTeacherCourses")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTeacherCoursesById([FromRoute] Guid id, int pageSize, int pageNumber)
        {
            var response = new GetTeacherCoursesByIdQuery
            {
                Id = id.ToString(),
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _mediator.Send(response);

            return NewResult(result);
        }

    }
}
