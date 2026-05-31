using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
  public class OnlineGame : Game, IComputerable, IMobile
  {
    public OnlineGame(string title, string genre, int ageRestriction,
                     DateTime releaseDate, double qualityRating)
        : base(title, genre, ageRestriction, releaseDate, qualityRating)
    {
    }

    public void PlayOnPC() { }
    public void PlayOnMobile() { }

    public override string GetGameInfo()
    {
      return $"[Online] {base.GetGameInfo()}";
    }
  }
}