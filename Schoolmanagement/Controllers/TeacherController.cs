using Core.Application.Commands;
using Core.Application.DTOs;
using Core.Application.DTOs.Student.StudentDtos;
using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Application.Featurs.Students.StudentCommands;
using Core.Application.Featurs.Students.StudentQuery;
using Core.Application.Featurs.Teachers.TeacherCommands;
using Core.Application.Featurs.Teachers.TeacherQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School_Management.Api.Base;

namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : AppControllerBase
    {
        private IMediator _mediator { get; set; }

        public TeacherController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] AddTeacherRequest request)
        {
            var response = new AddTeacherCommand { Teacher = request };
            var result = await _mediator.Send(response);

            return NewResult(result);
        }

        [HttpGet]
        [Route("GetListOfTeachers")]
        public async Task<IActionResult> GetStudentList()
        {
            var response = new GetTeacherListQuery();
            var result = await _mediator.Send(response);

            return NewResult(result);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
        {
            var response = new GetTeacherByIdQuery { Id = id.ToString() };
            var result = await _mediator.Send(response);

            return NewResult(result);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid id)
        {
            var response = new DeleteTeacherCommand { Id = id.ToString() };
            var result = await _mediator.Send(response);

            return NewResult(result);
        }
    }
}
