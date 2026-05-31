using System;
using System.Windows;
using System.Windows.Controls;
using Model.Core;

namespace ComputerGames
{
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
        MessageBox.Show("Введите данные игры", "Ошибка",
                      MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }

      // Получаем выбранный тип игры
      string gameType = ((ComboBoxItem)gameTypeCombo.SelectedItem).Content.ToString();

      Game newGame = null;
      string[] listText = newGameTitle.Text.Split(',');

      try
      {
        // Очищаем строки от пробелов
        for (int i = 0; i < listText.Length; i++)
          listText[i] = listText[i].Trim();

        string title = listText[0];
        string genre = listText.Length > 1 ? listText[1] : "Unknown";
        int age = listText.Length > 2 ? int.Parse(listText[2]) : 0;
        double rating = listText.Length > 3 ? double.Parse(listText[3].Replace('.', ',')) : 5.0;

        // Создаём игру в зависимости от выбранного типа
        if (gameType.Contains("SingleGame"))
        {
          newGame = new SingleGame(title, genre, age, DateTime.Now, rating);
        }
        else if (gameType.Contains("MultiplayerGame"))
        {
          newGame = new MultiplayerGame(title, genre, age, DateTime.Now, rating);
        }
        else if (gameType.Contains("OnlineGame"))
        {
          newGame = new OnlineGame(title, genre, age, DateTime.Now, rating);
        }
        else
        {
          newGame = new SingleGame(title, genre, age, DateTime.Now, rating);
        }
      }
      catch (Exception ex)
      {
        // Если парсинг не удался - создаём с значениями по умолчанию
        string defaultTitle = newGameTitle.Text;

        if (gameType.Contains("SingleGame"))
        {
          newGame = new SingleGame(defaultTitle, "Unknown", 0, DateTime.Now, 5.0);
        }
        else if (gameType.Contains("MultiplayerGame"))
        {
          newGame = new MultiplayerGame(defaultTitle, "Unknown", 0, DateTime.Now, 5.0);
        }
        else
        {
          newGame = new OnlineGame(defaultTitle, "Unknown", 0, DateTime.Now, 5.0);
        }
      }

      if (newGame != null)
      {
        catalog.AddGame(newGame);
        newGameTitle.Clear();
        gamesGrid.Items.Refresh();

        MessageBox.Show("Игра добавлена", "Успех",
                      MessageBoxButton.OK, MessageBoxImage.Information);
      }
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

    private void Close_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }
  }
}
