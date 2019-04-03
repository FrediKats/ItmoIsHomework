using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AppliedMath.Lab4.Models;

namespace AppliedMath.Lab4
{
    public partial class MainWindow : Window
    {
        private readonly SystemState _state;
        private readonly ITransition[] _transitions;
        private readonly Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            _state = new SystemState();
            _transitions = new ITransition[] {new ProductCreator(), new ProductSeller(), new ResourceGenerator()};
            StateBlock.Text = _state.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string info = Iteration();
            UpdateUi(info);
        }

        private void UpdateUi(string info)
        {
            StateBlock.Text = _state.ToString();
            Logger.Items.Add(info);
            if (VisualTreeHelper.GetChildrenCount(Logger) > 0)
            {
                var border = (Border)VisualTreeHelper.GetChild(Logger, 0);
                var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                scrollViewer.ScrollToBottom();
            }
        }

        private string Iteration()
        {
            int index = _random.Next(_transitions.Length);
            ITransition current = _transitions[index];
            string description = current.GetTransitionName();

            bool isActive = current.Invoke(_state);
            if (isActive)
            {
                return description;
            }

            return $"Failed to make {description}";
        }
    }
}