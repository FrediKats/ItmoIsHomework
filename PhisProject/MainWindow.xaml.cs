using System;
using System.Windows;
using PhisProject.PhisAbstract;
using System.Windows.Threading;

namespace PhisProject
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer moveTimer;

        private Ball ball;
        double positionX = 150, positionY = 100;
        private int currentTime = 0;

        private double phi, Phi0 = Settings.PI / 2 * 0.4;

        public MainWindow()
        {
            InitializeComponent();
            InitInterface();
            //UpdateUI();
        }

        private void InitInterface()
        {
            ball = new Ball(Width, Height, positionX, positionY);

            PaintCanvas.Children.Add(ball.Line);
            PaintCanvas.Children.Add(ball.Ellipse);

            moveTimer = new DispatcherTimer();
            moveTimer.Interval = TimeSpan.FromMilliseconds(Settings.timePerTick);
            moveTimer.Tick += Tick;
        }

        private void Tick(object sender, EventArgs e)
        {
            currentTime += Settings.timePerTick;
            UpdatePosition();
            UpdateUI();
        }

        private void UpdatePosition()
        {
            positionX = Calculator.getObjectPositionX(Settings.W, currentTime, Phi0, Settings.L);
            positionY = Calculator.getObjectPositionY(Settings.W, currentTime, Phi0, Settings.L);
        }

        private void UpdateUI()
        {
            ball.UpdateLinePosition(ball.CenterX() + positionX, ball.CenterY() + positionY);
            ball.UpdateBallPosition(ball.CenterX() + positionX, ball.CenterY() + positionY);
            EllipseData.Text = $"X: {150 - positionX}\nY: {200 - positionY}\nPhi: {phi}";
        }

        private void StartMoving(object sender, RoutedEventArgs e)
        {
            moveTimer.Start();
        }
    }
}
