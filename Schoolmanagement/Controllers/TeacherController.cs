using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Application.Featurs.Teachers.TeacherCommands;
using Core.Application.Featurs.Teachers.TeacherQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] AddTeacherRequest request)
        {
            var response = new AddTeacherCommand { Teacher = request };
            var result = await _mediator.Send(response);

            return NewResult(result);
        }

        [HttpGet]
        [Route("GetListOfTeachers")]
        [Authorize(Roles = "Student")]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> GetTeacherList(int pageSize, int pageNumber)
        {
            var response = new GetTeacherListQuery() { pageNumber = pageNumber, pageSize = pageSize };
            var result = await _mediator.Send(response);

            return NewResult(result);
        }

        [HttpGet]
        [Route("{id:guid}FindTeacherById")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTeacherById([FromRoute] Guid id)
        {
            var response = new GetTeacherByIdQuery { Id = id.ToString() };
            var result = await _mediator.Send(response);

            return NewResult(result);
        }


        [HttpDelete]
        [Route("{id:guid}DeleteTeacher")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTeacher([FromRoute] Guid id)
        {
            var response = new DeleteTeacherCommand { Id = id.ToString() };
            var result = await _mediator.Send(response);

            return NewResult(result);
        }
    }
}
