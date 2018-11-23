using System;
using System.Windows;
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
            UpdateUi();
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
                _repository.Create(new SubjectSolutionModel(
                    window.SubjectInput.Text, 
                    window.PathToFileBlock.Text,
                    window.DescriptionInput.Text));
                UpdateUi();
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
                ElementAction(dialog.State, solution);
            }

            UpdateUi();
            list.UnselectAll();
        }

        private void ElementAction(ActionSelectedState? state, SubjectSolutionModel solution)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            try
            {
                switch (state)
                {
                    case ActionSelectedState.Delete:
                        _repository.Delete(solution.Id);
                        return;

                    case ActionSelectedState.Edit:
                        OnEdit(solution);
                        return;

                    case ActionSelectedState.Open:
                        Configuration.SolutionManager.OpenSolutionFile(4, solution.Path);
                        return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception catch");
            }
        }

        private void OnEdit(SubjectSolutionModel solution)
        {
            var window = new SolutionCreationWindow
            {
                Width = 500,
                Height = 250,
            };
            window.SubjectInput.Text = solution.Title;
            window.DescriptionInput.Text = solution.Description;
            window.PathToFileBlock.Text = solution.Path;
            window.ShowDialog();
            if (window.IsAccepted)
            {
                solution.Title = window.SubjectInput.Text;
                solution.Description = window.DescriptionInput.Text;
                solution.Path = window.PathToFileBlock.Text;
                _repository.Update(solution);
            }
        }

        private void UpdateUi()
        {
            SolutionListBox.ItemsSource = _repository.Read();
            SolutionListBox.Items.Refresh();
        }
    }
}