using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
  public interface IGameCatalog
  {
    void Add(Game game);
    void Remove(Game game);
  }


  // Основное хранилище
  public partial class GameCatalog
  {
    private List<Game> games = new List<Game>();
    public IReadOnlyList<Game> Games => games;

    public void AddGame(Game game)
    {
      if (game == null)
        throw new ArgumentNullException(nameof(game));
      games.Add(game);
    }
    public void RemoveGame(Game game)
    {
      if (game == null)
        throw new ArgumentNullException(nameof(game));
      games.Remove(game);
    }
  }


  // Реализация IGameCatalog
  public partial class GameCatalog : IGameCatalog
  {
    public void Add(Game game) => AddGame(game);
    public void Remove(Game game) => RemoveGame(game);
  }


  // Сортировка и фильтрация
  public partial class GameCatalog
  {
    public List<Game> SortByTitle()
    {
      return games.OrderBy(g => g.Title).ToList();
    }
    public List<Game> FilterByPlatform<T>() where T : IPlatform
    {
      return games.Where(g => g is T).ToList();
    }
    public List<Game> FilterByGameMode<T>() where T : Game
    {
      return games.OfType<Game>().ToList();
    }

    public List<Game> SortAndFilter(string platform, string gameMode)
    {
      var filtered = games.AsEnumerable();

      // Фильтр по платформе
      if (platform == "PC")
        filtered = filtered.Where(g => g is IComputerable);
      else if (platform == "console")
        filtered = filtered.Where(g => g is IConsoleable);
      else if (platform == "mobile")
        filtered = filtered.Where(g => g is IMobile);

      // Фильтр по режиму
      if (gameMode == "single")
        filtered = filtered.Where(g => g is SingleGame);
      else if (gameMode == "multi")
        filtered = filtered.Where(g => g is MultiplayerGame);
      else if (gameMode == "online")
        filtered = filtered.Where(g => g is OnlineGame);

      return filtered.OrderBy(g => g.Title).ToList();
    }
  }
}