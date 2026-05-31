using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
  public abstract class Game
  {
    public string Title { get; set; }
    public string Genre { get; set; }
    public int AgeRestriction { get; set; }
    public DateTime ReleaseDate { get; set; }
    public double QualityRating { get; set; }

    protected Game(string title, string genre, int ageRestriction,
                  DateTime releaseDate, double qualityRating)
    {
      Title = title;
      Genre = genre;
      AgeRestriction = ageRestriction;
      ReleaseDate = releaseDate;
      QualityRating = qualityRating;
    }

    protected Game() { }

    public virtual string GetGameInfo()
    {
      return $"{Title} - {Genre} ({ReleaseDate.Year})";
    }

    // Свойства для отображения в таблице
    // Режим игры (возвращает имя класса) 
    public string GameMode
    {
      get
      {
        if (this is SingleGame) return "Single";
        if (this is MultiplayerGame) return "Multiplayer";
        if (this is OnlineGame) return "Online";
        return "Unknown";
      }
    }

    // Платформы (возвращает строку с поддерживаемыми платформами)
    public string Platforms
    {
      get
      {
        var platforms = new System.Collections.Generic.List<string>();

        if (this is IComputerable) platforms.Add("PC");
        if (this is IConsoleable) platforms.Add("Console");
        if (this is IMobile) platforms.Add("Mobile");

        return string.Join(", ", platforms);
      }
    }
  }
}
