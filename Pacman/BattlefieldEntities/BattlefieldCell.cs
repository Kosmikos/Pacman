namespace Pacman.BattlefieldEntities
{
    public class BattlefieldCell
    {
        public BattlefieldCellType BattlefieldCellType { get; }
        public Position Position { get; }

        public BattlefieldCell(BattlefieldCellType celltType, Position position)
        {
            BattlefieldCellType = celltType;
            Position = position;
        }
    }
}
