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

        // Переопределение свойства (4)
        public override double QualityRating
        {
            get => Math.Round(base.QualityRating * 0.95, 1);
            set => base.QualityRating = value;
        }

        public void PlayOnPC() { }
        public void PlayOnMobile() { }

        // Переопределение 3
        public override string GetGameInfo()
        {
            return $"[Online] {base.GetGameInfo()}";
        }
    }
}
