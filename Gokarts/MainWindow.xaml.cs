using Gokarts.Controllers;
using System.Windows;
using Gokarts.Models;

namespace Gokarts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataBaseController.Initialize();
            DataBaseController.InsertTournament(new Models.Tournament(1, "TestTournament", "", false, "test", "x"));
            InitializeComponent();
            DataBaseController.InsertTournamentOccurence(new Models.TournamentOccurence(0, 1, DateTime.Now.ToShortTimeString()));
            DataBaseController.InsertTeam(new Models.Team(1, "Test", "img"));
        }
    }
}