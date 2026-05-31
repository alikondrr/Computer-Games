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
using Model.Core;

namespace ComputerGames
{
  /// <summary>
  /// Логика взаимодействия для GameInfoWindow.xaml
  /// </summary>
  public partial class GameInfoWindow : Window
  {
    private Game game;

    public GameInfoWindow(Game selectedGame)
    {
      InitializeComponent();
      game = selectedGame;
      DisplayGameInfo();
    }

    private void DisplayGameInfo()
    {
      titleText.Text = game.Title;
      genreText.Text = game.Genre;
      ageText.Text = $"{game.AgeRestriction}+";
      dateText.Text = game.ReleaseDate.ToShortDateString();
      ratingText.Text = $"{game.QualityRating}/10";
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }
  }
}