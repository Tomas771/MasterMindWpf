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
using System.Windows.Shapes;

namespace MasterMindWpf
{
    /// <summary>
    /// Interaction logic for LoseWindow.xaml
    /// </summary>
    public partial class LoseWindow : Window
    {
        private GameLogic gameLogic;
        public LoseWindow(GameLogic gameLogic)
        {
            InitializeComponent();
            this.gameLogic = gameLogic;
            GenerateSearchedColours();
        }

        public void GenerateSearchedColours()
        {
            gameLogic.GenerateEllipses(SearchedColoursCanvas, Brushes.White, 1);

            int y = 0;
            foreach (var ellipse in SearchedColoursCanvas.Children)
            {
                ((Ellipse)ellipse).Fill = gameLogic.SearchedColours[y++];
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
