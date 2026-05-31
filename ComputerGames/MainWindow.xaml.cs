using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.Linq;
using Model.Core;
using Model.Data;

namespace ComputerGames
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameCatalog catalog;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                string format = ((ComboBoxItem)formatCombo.SelectedItem)?.Content.ToString() ?? "JSON";
                catalog = GameDataManager.LoadCatalog(format);
                UpdateGameList();
                ShowStatistics();
                statusText.Text = $"Загружено игр: {catalog.Games.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateGameList()
        {
            string platform = null;
            string mode = null;

            if (platformCombo.SelectedItem is ComboBoxItem pItem && pItem.Content.ToString() != "Все")
                platform = pItem.Content.ToString();

            if (modeCombo.SelectedItem is ComboBoxItem mItem && mItem.Content.ToString() != "Все")
                mode = mItem.Content.ToString();

            var filtered = catalog.SortAndFilter(platform, mode);
            gamesCombo.ItemsSource = filtered;
            showGameBtn.IsEnabled = filtered.Count > 0;
        }

        private void Filter_Changed(object sender, SelectionChangedEventArgs e)
        {
            UpdateGameList();
        }

        private void GamesCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            showGameBtn.IsEnabled = gamesCombo.SelectedItem != null;
        }

        private void Format_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (catalog != null)
            {
                string format = ((ComboBoxItem)formatCombo.SelectedItem).Content.ToString();
                GameDataManager.SaveCatalog(catalog, format);
                statusText.Text = $"Сохранено в формате {format}";
            }
        }

        private void ShowGame_Click(object sender, RoutedEventArgs e)
        {
            if (gamesCombo.SelectedItem is Game selectedGame)
            {
                var infoWindow = new GameInfoWindow(selectedGame);
                infoWindow.Owner = this;
                infoWindow.ShowDialog();
            }
        }

        private void OpenCatalog_Click(object sender, RoutedEventArgs e)
        {
            var catalogWindow = new CatalogWindow(catalog);
            catalogWindow.Owner = this;
            catalogWindow.ShowDialog();

            UpdateGameList();
            ShowStatistics();

            try
            {
                string format = "json";
                if (formatCombo.SelectedItem is ComboBoxItem item)
                    format = item.Content.ToString();

                GameDataManager.SaveCatalog(catalog, format);
                statusText.Text = $"Каталог сохранён. Игр: {catalog.Games.Count}";
            }
            catch (Exception ex)
            {
                statusText.Text = "Игры обновлены, но сохранение не удалось";
            }
        }

        private void ShowStatistics()
        {
            var allGames = catalog.Games.ToList();
            if (allGames.Count == 0) return;

            // Приведение к базовому классу №1
            Game firstGame = allGames[0];

            // Приведение к интерфейсу №1
            if (firstGame is IPlatform)
            {
                System.Diagnostics.Debug.WriteLine("Игра поддерживает IPlatform");
            }

            // Приведение к интерфейсу №2
            var pcGames = allGames.OfType<IComputerable>().ToList();

            // Приведение к интерфейсу №3
            foreach (var game in allGames)
            {
                if (game is IConsoleable consoleGame)
                    consoleGame.PlayOnConsole();
            }

            // Приведение к базовому классу №2
            Game lastGame = allGames[allGames.Count - 1];
        }
    }
}
