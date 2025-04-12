using Core.Application.DTOs.StudentCourse;
using Core.Application.Featurs.StudentCourse.StudentCourseCommands;
using Core.Application.Featurs.StudentCourse.StudentCourseQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Management.Api.Base;

namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : AppControllerBase
    {
        private IMediator _mediator {  get; set; }

        public StudentCourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("RegisterInCourse")]
        //[Authorize(Roles = "Student")]
        public async Task<IActionResult> RegisterInCourse([FromBody]RegisterInCourseRequest studentCourse)
        {
            
            var request = new RegisterInCourseCommand { StudentCourse = studentCourse };
            var result = await _mediator.Send(request);

            return NewResult(result); 
        }

        [HttpGet]
        [Route("GetStudentCourses")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStudentCourses(int pageSize, int pageNumber)
        {
            var request = new GetCourseStudentListQuery { pageNumber = pageNumber , pageSize = pageSize };
            var result = await _mediator.Send(request);

            return NewResult(result);
        }

        [HttpGet]
        [Route("{id:guid}FindCoursesByStudentId")]
        //[Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetCoursesByStudentId([FromRoute] Guid id , int pageSize, int pageNumber)
        {
            var response = new GetCoursesByStudentIdQuery { Id = id.ToString() , pageSize = pageSize , pageNumber = pageNumber};
            var result = await _mediator.Send(response);

            return NewResult(result);
        }

    }
}
