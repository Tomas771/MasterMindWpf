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
    /// Interaction logic for WinWindow.xaml
    /// </summary>
    public partial class WinWindow : Window
    {
        private PlayerAdministrator playerAdministrator;
        private int attempts;
        public WinWindow(PlayerAdministrator playerAdministrator, int Attempts)
        {
            InitializeComponent();
            this.playerAdministrator = playerAdministrator;
            attempts = Attempts;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            playerAdministrator.Players.Add(new Player(NameTextBox.Text, attempts));

            try
            {
                playerAdministrator.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var scoreboard = new Scoreboard(playerAdministrator);
            scoreboard.ShowDialog();
            this.Close();
        }
    }
}
