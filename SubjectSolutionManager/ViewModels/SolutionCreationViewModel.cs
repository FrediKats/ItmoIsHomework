using System.IO;
using System.Windows.Input;
using Microsoft.Win32;
using SubjectSolutionManager.Tools;

namespace SubjectSolutionManager.ViewModels
{
    public class SolutionCreationViewModel : BaseViewModel
    {
        private string _subject;

        public string Subject
        {
            get => _subject;
            set
            {
                SetProperty(ref _subject, value);
            }
        }

        public string Description { get; set; }

        public string PathToFile { get; private set; }

        public ICommand OpenPathSelectDialog => new DelegateCommand(PathSelecting);

        private void PathSelecting(object o)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Solution (*.sln)|*.sln"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                PathToFile = openFileDialog.FileName;
                OnPropertyChanged(nameof(PathToFile));
            }
        }
    }
}