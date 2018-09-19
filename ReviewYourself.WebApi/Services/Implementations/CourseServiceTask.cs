using System;
using System.Collections.Generic;
using System.Linq;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Tools;

namespace ReviewYourself.WebApi.Services.Implementations
{
    public class CourseServiceTask : ICourseTaskService
    {
        private readonly PeerReviewContext _context;
        private readonly IMemberService _memberService;

        public CourseServiceTask(PeerReviewContext context, IMemberService memberService)
        {
            _context = context;
            _memberService = memberService;
        }

        public CourseTask Create(CourseTask courseTask, Guid executorId)
        {
            if (courseTask.AuthorId != executorId)
            {
                throw new ArgumentException("Other id marked as author");
            }

            _context.Add(courseTask);
            _context.SaveChanges();
            return courseTask;
        }

        public CourseTask Get(Guid taskId, Guid executorId)
        {
            //TODO: check if executor is course member
            return _context.CourseTasks.Find(taskId);
        }

        public ICollection<CourseTask> GetTaskInCourse(Guid courseId, Guid executorId)
        {
            //TODO: check if executor is course member
            return _context.CourseTasks.Where(ct => ct.CourseId == courseId).ToList();
        }

        public void Delete(Guid taskId, Guid executorId)
        {
            //TODO: check if executor is course creator or task's author
            var task = _context.CourseTasks.Find(taskId);
            _context.CourseTasks.Remove(task);
        }
    }
}