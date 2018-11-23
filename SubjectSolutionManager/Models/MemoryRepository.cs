using System;
using System.Collections.Generic;

namespace SubjectSolutionManager.Models
{
    public class MemoryRepository : ISubjectSolutionRepository
    {
        private readonly List<SubjectSolutionModel> _solutionList;

        public MemoryRepository()
        {
            _solutionList = new List<SubjectSolutionModel>();
        }

        public void Create(SubjectSolutionModel solution)
        {
            _solutionList.Add(solution);
        }

        public List<SubjectSolutionModel> Read()
        {
            return _solutionList;
        }

        public SubjectSolutionModel Update(SubjectSolutionModel solution)
        {
            int i = _solutionList.FindIndex(s => s.Id == solution.Id);
            _solutionList[i] = solution;
            return _solutionList[i];
        }

        public void Delete(Guid id)
        {
            SubjectSolutionModel item = _solutionList.Find(s => s.Id == id);
            _solutionList.Remove(item);
        }

        public SubjectSolutionModel Read(Guid id)
        {
            return _solutionList.Find(s => s.Id == id);
        }
    }
}