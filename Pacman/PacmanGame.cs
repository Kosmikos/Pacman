using Pacman.BattlefieldEntities;
using Pacman.Init;
using Pacman.UI;
using Pacman.Units;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace Pacman
{
    public class PacmanGame
    {
        Battlefield _battlefield { get; set; }
        PacmanUnit _pacman { get; set; }
        IEnumerable<IEnimy> _enimies { get; set; }
        private int _score;

        public PacmanGame(IArrangementInitializer initializer, IViewer viewer)
        {
            var width = initializer.GetWidth();
            var height = initializer.GetHeight();
            _battlefield = new Battlefield(width, height);
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                   var cell= initializer.GetBattlefieldCell(i, j);
                    _battlefield.SetCell(i, j, cell);
                }
            }

            _pacman = initializer.GetPacman();
            _enimies = initializer.GetEnimies();
        }

        public void Start()
        {

        }

    }
}
