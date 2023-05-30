using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.BattlefieldEntities
{
    public class Battlefield
    {
        private IBattlefieldCell[,] _map;

        public List<IBattlefieldCell> Cells { get; }

        public Battlefield(int width, int height)
        {
            _map = new IBattlefieldCell[width, height];
            Cells = new List<IBattlefieldCell>();
        }

        internal void SetCell(int i, int j, IBattlefieldCell cell)
        {
            _map[i,j] = cell;
        }
    }
}

