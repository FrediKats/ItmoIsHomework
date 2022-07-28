using System;
using System.Windows;
using SubjectSolutionManager.Models;

namespace SubjectSolutionManager.Views
{
    public partial class ActionSelectWindow : Window
    {
        public ActionSelectedState? State;
        public ActionSelectWindow()
        {
            InitializeComponent();
        }

        private void OpenButtonOnClick(object sender, RoutedEventArgs e)
        {
            State = ActionSelectedState.Open;
            Close();
        }

        private void EditButtonOnClick(object sender, RoutedEventArgs e)
        {
            State = ActionSelectedState.Edit;
            Close();
        }

        private void DeleteButtonOnClick(object sender, RoutedEventArgs e)
        {
            State = ActionSelectedState.Delete;
            Close();
        }
    }
}
