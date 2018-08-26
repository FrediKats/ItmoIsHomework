using System;
using System.Collections.Generic;
using System.Linq;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Exceptions;
using ReviewYourself.WebApi.Models;
using ReviewYourself.WebApi.Tools;

namespace ReviewYourself.WebApi.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly PeerReviewContext _context;

        public CourseService(PeerReviewContext context)
        {
            _context = context;
        }

        public Course Create(Course course, Guid executorId)
        {
            _context.Courses.Add(course);
            var partition = new Participation
                {CourseId = course.Id, MemberId = executorId, Permission = MemberPermission.Creator};
            _context.Participations.Add(partition);
            _context.SaveChanges();

            return course;
        }

        public Course Get(Guid courseId)
        {
            var course = _context.Courses.Find(courseId);
            return course;
        }

        public ICollection<Course> FindCourses(string courseName)
        {
            //TODO: add search
            var courses = _context.Courses.Where(c => c.Title == courseName).ToList();
            return courses;
        }

        public void Update(Course course, Guid executorId)
        {
            //TODO: check if executorId is admin
            var currentCourse = _context.Courses.Find(course.Id);
            var userPermission = currentCourse.Members.FirstOrDefault(p => p.MemberId == executorId);
            if (userPermission == null || userPermission.Permission != MemberPermission.Creator)
            {
                throw new PermissionDeniedException(executorId);
            }

            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public void Delete(Guid courseId, Guid executorId)
        {
            //TODO: check if executorId is admin
            var currentCourse = _context.Courses.Find(courseId);
            var userPermission = currentCourse.Members.FirstOrDefault(p => p.MemberId == executorId);
            if (userPermission == null || userPermission.Permission != MemberPermission.Creator)
            {
                throw new PermissionDeniedException(executorId);
            }

            _context.Courses.Remove(currentCourse);
            _context.SaveChanges();
        }
    }
}