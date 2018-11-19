using System.Collections.Generic;
using SubjectSolutionManager.Models;

namespace SubjectSolutionManager.ViewModels
{
    public class SubjectSolutionExplorerViewModel
    {
        private readonly ISubjectSolutionRepository _repository;

        public SubjectSolutionExplorerViewModel()
        {
            _repository = RepositoryProvider.GetRepository();
            FakeDataGenerator.AddFakeSolution(_repository);
        }

        public List<SubjectSolutionModel> SubjectSolutionList => _repository.Read();
    }
}