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
    /// Interaction logic for Difficulty.xaml
    /// </summary>
    public partial class Difficulty : Window
    {
        private GameLogic gameLogic;
        /// <summary>
        /// Nastavení obtížnosti hry tzn. hádaných znaků
        /// </summary>
        /// <param name="gameLogic"></param>
        public Difficulty(GameLogic gameLogic)
        {
            InitializeComponent();
            this.gameLogic = gameLogic;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gameLogic.NewGame((int)SliderLabel.Content);
            gameLogic.IsEnableStart = true;
            this.Close();
        }

        private void DifficultySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int value = 2 * Convert.ToInt32(e.NewValue);
            SliderLabel.Content = value;
        }
    }
}
