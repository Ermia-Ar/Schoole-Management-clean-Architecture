using Core.Application.Commands;
using Core.Application.DTOs.Student.StudentDtos;
using Core.Application.Featurs.Students.StudentCommands;
using Core.Application.Featurs.Students.StudentQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using School_Management.Api.Base;

namespace School_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : AppControllerBase
    {
        private IMediator _mediator { get; set; }

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] AddStudentRequest request)
        {
            var response = new AddStudentCommand { student = request };
            var result = await _mediator.Send(response);

            return NewResult(result);
        }

        [HttpGet]
        [Route("GetListOfStudent")]
        public async Task<IActionResult> GetStudentList()
        {
            var response = new GetStudentListQuery();
            var result = await _mediator.Send(response);

            return NewResult(result);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetStudentById([FromRoute] Guid id)
        {
            var response = new GetStudentByIdQuery { Id = id.ToString() };
            var result = await _mediator.Send(response);

            return NewResult(result);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid id)
        {
            var response = new DeleteStudentCommand { Id = id.ToString() };
            var result = await _mediator.Send(response);

            return NewResult(result);
        }
    }
}
