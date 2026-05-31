using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
  public interface IStatistic
  {
    double MaxQuality(List<Game> games);
    double MinQuality(List<Game> games);
    double AvgQuality(List<Game> games);
    double MedianQuality(List<Game> games);
  }


  public class GameQualityStats : IStatistic
  {
    public double MaxQuality(List<Game> games)
    {
      if (games == null || games.Count == 0) return 0;
      return games.Max(g => g.QualityRating);
    }

    public double MinQuality(List<Game> games)
    {
      if (games == null || games.Count == 0) return 0;
      return games.Min(g => g.QualityRating);
    }

    public double AvgQuality(List<Game> games)
    {
      if (games == null || games.Count == 0) return 0;
      return games.Average(g => g.QualityRating);
    }

    public double MedianQuality(List<Game> games)
    {
      if (games == null || games.Count == 0) return 0;

      var sorted = games.Select(g => g.QualityRating).OrderBy(x => x).ToList();
      int count = sorted.Count;

      if (count % 2 == 0)
        return (sorted[count / 2 - 1] + sorted[count / 2]) / 2.0;
      return sorted[count / 2];
    }
  }
}