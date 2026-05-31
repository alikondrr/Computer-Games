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

            gamesGrid.SelectionChanged += (s, e) =>
            {
                removeBtn.IsEnabled = gamesGrid.SelectedItem != null;
            };

            catalog = gameCatalog;
            gamesGrid.ItemsSource = catalog.Games;
        }

        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(newGameTitle.Text))
            {
                MessageBox.Show("Введите данные игры в формате: тип, название, жанр, возраст, рейтинг\nПример: single, Stardew Valley, Simulation, 6, 9.1",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Game newGame;
            string[] listText = newGameTitle.Text.Split(',');

            try
            {
                if (listText.Length < 5)
                {
                    MessageBox.Show("Нужно 5 полей: тип, название, жанр, возраст, рейтинг",
                                  "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                string gameType = listText[0].Trim().ToLower();
                string title = listText[1].Trim();
                string genre = listText[2].Trim();
                int age = int.Parse(listText[3].Trim());
                double rating = double.Parse(listText[4].Trim().Replace('.', ','));

                if (gameType.Contains("multi"))
                {
                    newGame = new MultiplayerGame(title, genre, age, DateTime.Now, rating);
                }
                else if (gameType.Contains("online"))
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
                MessageBox.Show($"Ошибка ввода: {ex.Message}\nФормат: тип, название, жанр, возраст, рейтинг",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            catalog.AddGame(newGame);
            newGameTitle.Clear();
            gamesGrid.Items.Refresh();

            MessageBox.Show($"Игра '{newGame.Title}' добавлена", "Успех",
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
    }
}
