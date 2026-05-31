using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
  public class SingleGame : Game, IComputerable, IConsoleable
  {
    public SingleGame(string title, string genre, int ageRestriction,
                     DateTime releaseDate, double qualityRating)
        : base(title, genre, ageRestriction, releaseDate, qualityRating)
    {
    }

    public void PlayOnPC() { }
    public void PlayOnConsole() { }

    public override string GetGameInfo()
    {
      return $"[Single] {base.GetGameInfo()}";
    }
  }
}