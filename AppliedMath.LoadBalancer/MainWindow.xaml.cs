using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AppliedMath.LoadBalancer.Models;

namespace AppliedMath.LoadBalancer
{
    public partial class MainWindow : Window
    {
        private readonly QueueGenerator _queueGenerator;

        public MainWindow()
        {
            InitializeComponent();
            var logger = new Logger((s) =>
            {
                lock (LogList)
                {
                    LogList.Items.Add(s);
                    if (VisualTreeHelper.GetChildrenCount(LogList) > 0)
                    {
                        var border = (Border)VisualTreeHelper.GetChild(LogList, 0);
                        var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
                        scrollViewer.ScrollToBottom();
                    }
                }
            });
            _queueGenerator = new QueueGenerator(logger);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _queueGenerator.Start();   
        }
    }
}
