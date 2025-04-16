using AutoMapper;
using Core.Application.DTOs.Student.StudentDtos;
using Core.Application.Featurs.Students.StudentCommands;
using Core.Application.Featurs.Students.StudentQuery;
using Core.Application.Interfaces;
using Core.Application.Wrapper;
using Core.Domain.Bases;
using MediatR;
using System.Diagnostics;

namespace Core.Application.Featurs.Students.StudentHandlers
{
    public class StudentHandlers : ResponseHandler
        , IRequestHandler<AddStudentCommand, Response<string>>
        , IRequestHandler<GetStudentListQuery, Response<PaginatedResult<StudentResponse>>>
        , IRequestHandler<GetStudentByIdQuery, Response<StudentResponse>>
        , IRequestHandler<DeleteStudentCommand, Response<StudentResponse>>
    {
        private IStudentServices _studentServices;
        private IBackgroundJobServices _backgroundJob;
        private IMapper _mapper;

        public StudentHandlers(IAuthService authService, IStudentServices studentServices, IMapper mapper, IBackgroundJobServices backgroundJob)
        {
            _studentServices = studentServices;
            _mapper = mapper;
            _backgroundJob = backgroundJob;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
           
            //add to student and user table
            var result = await _studentServices.AddStudentAsync(request.student);

            if (result)
                return Created("");
            else
                return UnprocessableEntity<string>();
        }

        public async Task<Response<PaginatedResult<StudentResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            //get from students table
            var students = await _studentServices.GetStudentListAsync();

            //map to list of student response 
            var studentsResponse = _mapper.Map<List<StudentResponse>>(students);
            var paginated = await studentsResponse.AsQueryable()
                .ToPaginatedListAsync(request.pageNumber, request.pageSize);

            _backgroundJob.AddEnqueue(() => Task.Delay(TimeSpan.FromSeconds(20)).Wait());

            return Success(paginated);
        }

        public async Task<Response<StudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            //get from students table by id
            var result = await _studentServices.GetStudentByIdAsync(request.Id);
            if (result.IsFailure)
            {
                return NotFound<StudentResponse>();
            }
            // map to student response
            var studentResponse = _mapper.Map<StudentResponse>(result.Value);

            return Success(studentResponse);

        }

        public async Task<Response<StudentResponse>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            // check student have course
            var result = await _studentServices.StudentIsInAnyCourse(request.Id);
            if (result)
            {
                return BadRequest<StudentResponse>("student have course");
            }
            //Delete student from tables student and user
            var student = await _studentServices.DeleteStudentAsync(request.Id);
            if (student == null)
            {
                return NotFound<StudentResponse>();
            }
            //map to student response
            var studentResponse = _mapper.Map<StudentResponse>(student);
            return Deleted(studentResponse);
        }
    }
}
