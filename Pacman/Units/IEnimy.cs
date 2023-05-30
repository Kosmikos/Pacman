using Pacman.BattlefieldEntities;

namespace Pacman.Units
{
    public interface IEnimy
    {
        EnimyType Type { get; }
        BattlefieldCell GoTo(Battlefield  battlefield);
    }
}
