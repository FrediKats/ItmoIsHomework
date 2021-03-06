using System;
using System.Collections.Generic;
using System.Linq;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Tools;

namespace ReviewYourself.WebApi.Services.Implementations
{
    public class SolutionService : ISolutionService
    {
        private readonly PeerReviewContext _context;
        private readonly IMemberService _memberService;

        public SolutionService(PeerReviewContext context, IMemberService memberService)
        {
            _context = context;
            _memberService = memberService;
        }

        public CourseSolution Create(CourseSolution courseSolution, Guid executorId)
        {
            //TODO: check if executor is course's member
            _context.Solutions.Add(courseSolution);
            _context.SaveChanges();
            return courseSolution;
        }

        public CourseSolution Get(Guid solutionId, Guid executorId)
        {
            return _context.Solutions.Find(solutionId);
        }

        public CourseSolution GetUserSolution(Guid taskId, Guid userId, Guid executorId)
        {
            return _context.Solutions.FirstOrDefault(s => s.CourseTaskId == taskId
                                                          && s.AuthorId == userId);
        }

        public ICollection<CourseSolution> GetSolutionsByTask(Guid taskId, Guid executor)
        {
            return _context.Solutions.Where(s => s.CourseTaskId == taskId).ToList();
        }

        public void Delete(Guid solutionId, Guid executorId)
        {
            //TODO: check permission
            var solution = _context.Solutions.Find(solutionId);
            _context.Solutions.Remove(solution);
            _context.SaveChanges();
        }
    }
}