using AutoMapper;
using Core.Application.DTOs.Student.StudentDtos;
using Core.Application.Featurs.Students.StudentCommands;
using Core.Application.Featurs.Students.StudentQuery;
using Core.Application.Interfaces;
using Core.Domain.Bases;
using FluentValidation;
using MediatR;

namespace Core.Application.Featurs.Students.StudentHandlers
{
    public class StudentHandlers : ResponseHandler
        , IRequestHandler<AddStudentCommand, Response<string>>
        , IRequestHandler<GetStudentListQuery, Response<List<StudentResponse>>>
        , IRequestHandler<GetStudentByIdQuery, Response<StudentResponse>>
        , IRequestHandler<DeleteStudentCommand, Response<StudentResponse>>
    {
        private IStudentServices _studentServices { get; set; }
        private IMapper _mapper { get; set; }
        private IValidator<AddStudentRequest> _validator { get; set; }

        public StudentHandlers(IAuthService authService, IStudentServices studentServices, IMapper mapper
            , IValidator<AddStudentRequest> validator)
        {
            _studentServices = studentServices;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            //validate the model
            var validationResult = await _validator.ValidateAsync(request.student);
            if (!validationResult.IsValid)
            {
                return BadRequest<string>(validationResult.Errors[0].ErrorMessage);
            }
            //add to student and user table
            var result = await _studentServices.AddStudentAsync(request.student);

            if (result)
                return Created("");
            else
                return UnprocessableEntity<string>();
        }

        public async Task<Response<List<StudentResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            //get from students table
            var students = await _studentServices.GetStudentListAsync();

            //map to list of student response 
            var studentsResponse = _mapper.Map<List<StudentResponse>>(students);
            var result = Success(studentsResponse);

            result.Meta = new { Count = studentsResponse.Count() };
            return result;
        }

        public async Task<Response<StudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            //get from students table by id
            var student = await _studentServices.GetStudentByIdAsync(request.Id);
            if(student == null)
            {
                return NotFound<StudentResponse>();
            }
            // map to student response
            var studentResponse = _mapper.Map<StudentResponse>(student);

            var result = Success(studentResponse);
            return result;

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
