using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace SubjectSolutionManager.Views
{
    public partial class SolutionCreationWindow : Window
    {
        public bool IsAccepted;

        public SolutionCreationWindow()
        {
            InitializeComponent();
        }

        private void PathSelecting(object sender, EventArgs args)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Solution (*.sln)|*.sln"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                PathToFileBlock.Text = openFileDialog.FileName;
            }
        }

        private void ValidateSolution(object sender, EventArgs args)
        {
            if (File.Exists(PathToFileBlock.Text) == false)
            {
                MessageBox.Show("Invalid path");
                return;
            }

            IsAccepted = true;
            Close();
        }
    }
}