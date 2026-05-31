using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Model.Core;

namespace Model.Data
{
  public static class GameDataManager
  {
    private static string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ComputerGamesData");

    static GameDataManager()
    {
      if (!Directory.Exists(dataPath))
        Directory.CreateDirectory(dataPath);
    }

    public static GameCatalog LoadCatalog(string format = "JSON")
    {
      string filePath = Path.Combine(dataPath, $"games.{format.ToLower()}");
      Serializer serializer = GetSerializer(format);

      var catalog = serializer.Deserialize<GameCatalog>(filePath);

      if (catalog == null)
      {
        catalog = new GameCatalog();
        GenerateTestData(catalog);
        SaveCatalog(catalog, format);
      }

      return catalog;
    }

    public static void SaveCatalog(GameCatalog catalog, string format)
    {
      string filePath = Path.Combine(dataPath, $"games.{format.ToLower()}");
      Serializer serializer = GetSerializer(format);
      serializer.Serialize(catalog, filePath);
    }

    private static Serializer GetSerializer(string format)
    {
      if (format == "XML")
        return new XmlSerializer();
      return new JsonSerializer(); // JSON по умолчанию
    }

    private static void GenerateTestData(GameCatalog catalog)
    {
      var games = new List<Game>
            {
                new SingleGame("The Witcher 3", "RPG", 18, new DateTime(2015, 5, 19), 9.5),
                new SingleGame("Cyberpunk 2077", "RPG", 18, new DateTime(2020, 12, 10), 7.8),
                new MultiplayerGame("Fortnite", "Battle Royale", 12, new DateTime(2017, 7, 25), 8.2),
                new MultiplayerGame("Among Us", "Party", 7, new DateTime(2018, 6, 15), 8.5),
                new OnlineGame("World of Warcraft", "MMORPG", 13, new DateTime(2004, 11, 23), 9.0),
                new OnlineGame("Genshin Impact", "Action RPG", 12, new DateTime(2020, 9, 28), 8.7),
                new SingleGame("Hades", "Roguelike", 16, new DateTime(2020, 9, 17), 9.3),
                new MultiplayerGame("Call of Duty", "Shooter", 18, new DateTime(2019, 10, 25), 8.0),
                new OnlineGame("League of Legends", "MOBA", 13, new DateTime(2009, 10, 27), 8.8),
                new SingleGame("Stardew Valley", "Simulation", 6, new DateTime(2016, 2, 26), 9.1)
            };

      foreach (var game in games)
      {
        catalog.AddGame(game);
      }
    }
  }
}