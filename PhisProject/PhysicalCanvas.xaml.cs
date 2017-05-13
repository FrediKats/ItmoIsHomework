using System;
using System.Windows;
using PhysProject.Source;
using PhysProject.TestingTool;

namespace PhysProject
{
    public partial class MainWindow : Window
    {
        private PhysicalField _field;

        public MainWindow()
        {

            InitializeComponent();
            UpdateUserInterface();
        }
    }
}
