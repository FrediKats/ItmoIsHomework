using System;
using System.Windows.Controls;
using SubjectSolutionManager.Models;

namespace SubjectSolutionManager.Views
{
    public partial class SubjectSolutionExplorerControl : UserControl
    {
        private readonly ISubjectSolutionRepository _repository;

        public SubjectSolutionExplorerControl()
        {
            InitializeComponent();
            _repository = RepositoryProvider.GetRepository();
            FakeDataGenerator.AddFakeSolution(_repository);
            SolutionListBox.ItemsSource = _repository.Read();
        }

        private void OnOpenView(object sender, EventArgs args)
        {
            var window = new SolutionCreationWindow
            {
                Width = 500,
                Height = 250
            };
            window.ShowDialog();
            if (window.IsAccepted)
            {
                _repository.Create(new SubjectSolutionModel(window.Subject, window.PathToFile, window.Description));
                SolutionListBox.ItemsSource = _repository.Read();
                SolutionListBox.Items.Refresh();
            }
        }
    }
}