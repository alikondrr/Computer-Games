using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
  public class MultiplayerGame : Game, IConsoleable, IComputerable, IMobile
  {
    public MultiplayerGame(string title, string genre, int ageRestriction,
                          DateTime releaseDate, double qualityRating)
        : base(title, genre, ageRestriction, releaseDate, qualityRating)
    {
    }

    public void PlayOnPC() { }
    public void PlayOnConsole() { }
    public void PlayOnMobile() { }

    public override string GetGameInfo()
    {
      return $"[Multi] {base.GetGameInfo()}";
    }
  }
}