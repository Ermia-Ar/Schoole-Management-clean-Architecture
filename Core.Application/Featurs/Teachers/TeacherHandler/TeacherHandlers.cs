using AutoMapper;
using Core.Application.DTOs.Teacher.TeacherDtos;
using Core.Application.Featurs.Teachers.TeacherCommands;
using Core.Application.Featurs.Teachers.TeacherQuery;
using Core.Application.Interfaces;
using Core.Application.Wrapper;
using Core.Domain.Bases;
using MediatR;

namespace Core.Application.Featurs.Teachers.TeacherHandler
{
    public class TeacherHandlers : ResponseHandler
        , IRequestHandler<AddTeacherCommand, Response<string>>
        , IRequestHandler<DeleteTeacherCommand, Response<TeacherResponse>>
        , IRequestHandler<GetTeacherListQuery, Response<PaginatedResult<TeacherResponse>>>
        , IRequestHandler<GetTeacherByIdQuery, Response<TeacherResponse>>
    {
        private ITeacherServices _teacherServices { get; set; }
        private IMapper _mapper { get; set; }

        public TeacherHandlers(ITeacherServices teacherServices, IMapper mapper)
        {
            _teacherServices = teacherServices;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddTeacherCommand request, CancellationToken cancellationToken)
        {
            //add to teachers and user tables
            var result = await _teacherServices.AddTeacherAsync(request.Teacher);

            if (result)
                return Created("");
            else
                return UnprocessableEntity<string>();
        }

        public async Task<Response<TeacherResponse>> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            // check teacher have course
            var result = await _teacherServices.TeacherIsInAnyCourse(request.Id);
            if (result)
            {
                return BadRequest<TeacherResponse>("Teacher have course !");
            }
            //delete from teachers and user tables
            var teacher = await _teacherServices.DeleteTeacherAsync(request.Id);
            if (teacher == null)
            {
                return NotFound<TeacherResponse>();
            }
            //map to teacher response
            var teacherResponse = _mapper.Map<TeacherResponse>(teacher);
            return Deleted(teacherResponse);

        }

        public async Task<Response<PaginatedResult<TeacherResponse>>> Handle(GetTeacherListQuery request, CancellationToken cancellationToken)
        {
            // get teacher list from teachers table
            var teachers = await _teacherServices.GetTeacherListAsync();
            //map to teacher response
            var teacherResponse = _mapper.Map<List<TeacherResponse>>(teachers);
            //paginate
            var teacherResponsePaginated = await teacherResponse.AsQueryable()
                .ToPaginatedListAsync(request.pageNumber, request.pageSize);

            return Success(teacherResponsePaginated);

        }

        public async Task<Response<TeacherResponse>> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            // get teacher from teachers table
            var teacher = await _teacherServices.GetTeacherByIdAsync(request.Id);
            if (teacher == null)
            {
                return NotFound<TeacherResponse>();
            }
            //map to teacher response
            var teacherResponse = _mapper.Map<TeacherResponse>(teacher);

            return Success(teacherResponse);
        }

    }
}
