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

    public virtual string GetGameInfo()
    {
      return $"{Title} - {Genre} ({ReleaseDate.Year})";
    }
  }
}