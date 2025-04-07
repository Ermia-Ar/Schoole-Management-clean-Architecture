using AutoMapper;
using Core.Application.DTOs.Course;
using Core.Application.Interfaces;
using Infrastructure.Data.Data;
using Infrastructure.Data.Entities;
using SchoolProject.Infrustructure.UnitOfWork;
using CoreCourse = Core.Domain.Entities.Course;


namespace Infrastructure.Data.Services
{
    public class CourseServices : ICourseServices
    {
        private IUnitOfWork _unitOfWork { get; set; }
        private ApplicationDbContext _context { get; set; }
        private IMapper _mapper { get; set; }

        public CourseServices(ApplicationDbContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddCourseAsync(AddCourseRequest request)
        {
            //map and fill the course id 
            var course = _mapper.Map<Course>(request);
            course.Id = Guid.NewGuid();

            try
            {
                //add to Courses table
                await _unitOfWork.Courses.AddAsync(course);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<CoreCourse>> GetCourseListAsync()
        {
            // get all from courses table
            var courses = await _unitOfWork.Courses.GetCourseListAsync();

            //map to core courses
            var coreCourses = _mapper.Map<List<CoreCourse>>(courses);

            return coreCourses;
        }

        public async Task<bool> DeleteCourseAsync(string id)
        {
            //get course from data base
            var course = await _context.Courses.FindAsync(Guid.Parse(id));
            if (course == null)
                return false;

            //delete course from data base
            try
            {
                await _unitOfWork.Courses.DeleteAsync(course);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CoreCourse?> GetCourseById(string id)
        {
            //Find in Courses Table
            var course = await _unitOfWork.Courses.GetByIdAsync(Guid.Parse(id));
            if (course == null)
                return null;

            var coreCourse = _mapper.Map<CoreCourse>(course);

            return coreCourse;
        }

        public async Task<List<CoreCourse>> GetTeacherCoursesById(string id)
        {
            var courses = await _unitOfWork.Courses.GetTeacherCoursesById(id);

            var coreCourses = _mapper.Map<List<CoreCourse>>(courses);
            return coreCourses;
        }
    }
}