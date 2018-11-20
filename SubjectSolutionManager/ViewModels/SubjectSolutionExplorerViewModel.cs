using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using SubjectSolutionManager.Models;
using SubjectSolutionManager.Views;
using DelegateCommand = SubjectSolutionManager.Tools.DelegateCommand;

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
        public ICommand OpenNewView => new DelegateCommand(OnOpenView);

        private void OnOpenView(object o)
        {
            var window = new DialogWindow() {Content = new SolutionCreationControl()};
            var res = window.ShowModal();
        }
    }
}