using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace SubjectSolutionManager.Views
{
    /// <summary>
    /// Interaction logic for SolutionCreationWindow.xaml
    /// </summary>
    public partial class SolutionCreationWindow : Window
    {
        public SolutionCreationWindow()
        {
            InitializeComponent();
        }

        public string Subject { get; set; }
        public string Description { get; set; }
        public string PathToFile { get; private set; } = "Select path to .sln";
        public bool IsAccepted = false;

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

            //TODO: add checker
            IsAccepted = true;
            Subject = SubjectInput.Text;
            Description = DescriptionInput.Text;
            PathToFile = PathToFileBlock.Text;
            this.Close();
        }
    }
}
