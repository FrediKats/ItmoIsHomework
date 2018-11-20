using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using SubjectSolutionManager.Models;
using SubjectSolutionManager.Tools;

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
        public ICommand TestCommand => new DelegateCommand((o) => MessageBox.Show("On debug command"));
    }
}