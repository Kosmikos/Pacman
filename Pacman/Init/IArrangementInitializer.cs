using Pacman.BattlefieldEntities;
using Pacman.Units;
using System.Collections.Generic;

namespace Pacman.Init
{
    public interface IArrangementInitializer
    {
        IBattlefieldCell GetBattlefieldCell(int x, int y);
        int GetWidth();
        int GetHeight();
        PacmanUnit GetPacman();
        IEnumerable<IEnimy> GetEnimies();
    }
}
