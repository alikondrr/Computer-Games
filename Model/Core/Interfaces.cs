using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Core
{
  public interface IPlatform { }

  public interface IConsoleable : IPlatform
  {
    void PlayOnConsole();
  }

  public interface IComputerable : IPlatform
  {
    void PlayOnPC();
  }

  public interface IMobile : IPlatform
  {
    void PlayOnMobile();
  }
}
