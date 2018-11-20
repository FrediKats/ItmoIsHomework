using System.Collections.Generic;
using System.Windows.Input;
using SubjectSolutionManager.Models;
using SubjectSolutionManager.Views;
using DelegateCommand = SubjectSolutionManager.Tools.DelegateCommand;

namespace SubjectSolutionManager.ViewModels
{
    public class SubjectSolutionExplorerViewModel : BaseViewModel
    {
        private readonly ISubjectSolutionRepository _repository;

        public SubjectSolutionExplorerViewModel()
        {
            _repository = RepositoryProvider.GetRepository();
            FakeDataGenerator.AddFakeSolution(_repository);
        }

        public List<SubjectSolutionModel> SubjectSolutionList => _repository.Read();
        public ICommand OpenNewView => new DelegateCommand(OnOpenView);

        private void OnOpenView(object o)
        {
            var window = new SolutionCreationWindow()
            {
                Width = 500,
                Height = 250
            };
            window.ShowDialog();
            if (window.IsAccepted)
            {
                _repository.Create(new SubjectSolutionModel(window.Subject, window.PathToFile, window.Description));
                OnPropertyChanged(nameof(SubjectSolutionList));
            }
        }
    }
}