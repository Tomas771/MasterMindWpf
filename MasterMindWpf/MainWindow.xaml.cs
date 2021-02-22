using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterMindWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameLogic gameLogic = new GameLogic();
        private PlayerAdministrator playerAdministrator = new PlayerAdministrator();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = gameLogic;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            var difficultyWindow = new Difficulty(gameLogic);
            difficultyWindow.ShowDialog();
            if(gameLogic.IsEnableStart)
            {
                gameLogic.GenerateEllipses(SearchedColoursCanvas, Brushes.Gray, 1);
                gameLogic.GenerateTipEllipses(TipColoursCanvas);
                gameLogic.GeneratePreviousPlayerTipEllipses(PreviousTipPlayerCanvas);
                gameLogic.GeneratePreviousTipEllipses(PreviousTipCanvas);
                TipButton.Visibility = Visibility.Visible;
            }
        }

        private void ScoreBoardButton_Click(object sender, RoutedEventArgs e)
        {
            var scoreboard = new Scoreboard(playerAdministrator);
            scoreboard.ShowDialog();
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gameLogic.SelectedColour = ((Ellipse)sender).Fill;
        }

        private void TipButton_Click(object sender, RoutedEventArgs e)
        {
            switch (gameLogic.ComparingColours(TipColoursCanvas))
            {
                case 0:
                    gameLogic.GeneratePreviousPlayerTipEllipses(PreviousTipPlayerCanvas);
                    gameLogic.GeneratePreviousTipEllipses(PreviousTipCanvas);
                    gameLogic.GenerateTipEllipses(TipColoursCanvas);
                    break;
                case 1:
                    var winWindow = new WinWindow(playerAdministrator, gameLogic.Attempt);
                    winWindow.ShowDialog();
                    break;
                case -1:
                    var loseWindow = new LoseWindow(gameLogic);
                    loseWindow.ShowDialog();
                    break;
            }

        }
    }
}
