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
using Model.Core;

namespace ComputerGames
{
  /// <summary>
  /// Логика взаимодействия для CatalogWindow.xaml
  /// </summary>
  public partial class CatalogWindow : Window
  {
    private GameCatalog catalog;

    public CatalogWindow(GameCatalog gameCatalog)
    {
      InitializeComponent();
      catalog = gameCatalog;
      gamesGrid.ItemsSource = catalog.Games;
    }

    private void AddGame_Click(object sender, RoutedEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(newGameTitle.Text))
      {
        MessageBox.Show("Введите название игры", "Ошибка",
                      MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }

      Game newGame;
      string[] listText = newGameTitle.Text.Split(", ");

      try
      {
        string gameType = listText[0].ToLower();
        string title = listText[0];
        string genre = listText[1];
        int age = int.Parse(listText[2]);
        double rating = double.Parse(listText[3].Replace('.', ','));

        // определяет тип игры
        if (gameType.Contains("multi") || gameType.Contains("multiplayer"))
        {
          newGame = new MultiplayerGame(title, genre, age, DateTime.Now, rating);
        }
        else if (gameType.Contains("online") || gameType.Contains("onlinegame"))
        {
          newGame = new OnlineGame(title, genre, age, DateTime.Now, rating);
        }
        else
        {
          newGame = new SingleGame(title, genre, age, DateTime.Now, rating);
        }
      }
      catch
      {
        newGame = new SingleGame(
            newGameTitle.Text,
            "Unknown",
            0,
            DateTime.Now,
            5.0
        );
      }

      catalog.AddGame(newGame);
      newGameTitle.Clear();
      gamesGrid.Items.Refresh();

      MessageBox.Show("Игра добавлена", "Успех",
                    MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void RemoveGame_Click(object sender, RoutedEventArgs e)
    {
      if (gamesGrid.SelectedItem is Game selectedGame)
      {
        if (MessageBox.Show($"Удалить игру '{selectedGame.Title}'?", "Подтверждение",
                          MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        {
          catalog.RemoveGame(selectedGame);
          gamesGrid.Items.Refresh();
        }
      }
      else
      {
        MessageBox.Show("Выберите игру для удаления", "Ошибка",
                      MessageBoxButton.OK, MessageBoxImage.Warning);
      }
    }
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
      this.Close();  // Закрывает ТОЛЬКО это окно
    }
  }
}