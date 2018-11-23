using System;
using System.Windows.Controls;
using SubjectSolutionManager.Models;
using SubjectSolutionManager.Tools;

namespace SubjectSolutionManager.Views
{
    public partial class SubjectSolutionExplorerControl : UserControl
    {
        private readonly ISubjectSolutionRepository _repository;

        public SubjectSolutionExplorerControl()
        {
            InitializeComponent();
            _repository = RepositoryProvider.GetRepository();
            SolutionListBox.ItemsSource = _repository.Read();
            SolutionListBox.Items.Refresh();
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

        private void OnSelectingElement(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;
            if (e.AddedItems.Count == 1)
            {
                var solution = e.AddedItems[0] as SubjectSolutionModel;
                var dialog = new ActionSelectWindow();
                dialog.ShowDialog();
                switch (dialog.State)
                {
                    case ActionSelectedState.Delete:
                        _repository.Delete(solution.Id);
                        SolutionListBox.Items.Refresh();
                        break;
                    case ActionSelectedState.Edit:
                        break;
                    case ActionSelectedState.Open:
                        Configuration.SolutionManager.OpenSolutionFile(4, solution.Path);
                        break;
                }
            }

            list.UnselectAll();
        }
    }
}