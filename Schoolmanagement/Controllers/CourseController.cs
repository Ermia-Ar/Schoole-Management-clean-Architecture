using Core.Application.DTOs.Course.CourseDtos;
using Core.Application.Featurs.Courses.CourseCommands;
using Core.Application.Featurs.Courses.CourseQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Management.Api.Base;

namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : AppControllerBase
    {
        private IMediator _mediator {  get; set; }

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateCourse([FromBody] AddCourseRequest course)
        {
            var request = new AddCourseCommand { Course = course };
            var result  = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpGet]
        [Route("GetListOfCourses")]
        public async Task<IActionResult> GetCourseList()
        {
            var request = new GetCourseListQuery();
            var result = await _mediator.Send(request); 

            return NewResult(result);
        }

        [HttpDelete]
        [Route("{id:guid}DeleteCourse")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var request = new DeleteCourseCommand { Id = id.ToString() };
            var result  = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpGet]
        [Route("{id:guid}FindCourseById")]
        public async Task<IActionResult> GetCourseById([FromRoute]Guid id)
        {
            var request = new GetCourseByIdQuery { Id = id.ToString() };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }

    }
}
