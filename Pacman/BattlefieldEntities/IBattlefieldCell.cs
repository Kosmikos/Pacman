using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.BattlefieldEntities
{
    public interface IBattlefieldCell
    {
        BattlefieldCellType BattlefieldCellType { get; }
    }
}
