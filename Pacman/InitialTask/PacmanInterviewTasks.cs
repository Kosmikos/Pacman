using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.InitialTask
{
    public sealed class Pacman
    {
        private UnitType[,] _map;
        private (int x, int y) _pacman;
        private readonly List<(int x, int y)> _enemies = new();
        private int _score;

        private enum UnitType
        {
            Pacman, Enemy, Wall, Coin, Free
        }

        public enum Direction
        {
            Top, Right, Bottom, Left
        }

        public void LoadMap(string path, int width, int height)
        {
            var lines = File.ReadAllLines(path);
            _map = new UnitType[width, height];

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var parts = line.Split(" ");

                for (var j = 0; j < parts.Length; j++)
                {
                    var part = parts[j];
                    var elementType = (UnitType)int.Parse(part);

                    switch (elementType)
                    {
                        case UnitType.Pacman:
                            _pacman = (i, j);
                            break;
                        case UnitType.Enemy:
                            _enemies.Add((i, j));
                            break;
                        case UnitType.Wall:
                        case UnitType.Coin:
                        case UnitType.Free:
                            _map[i, j] = elementType;
                            break;
                    }
                }
            }
        }

        public void SaveMap(string path)
        {
            _map[_pacman.x, _pacman.y] = UnitType.Pacman;
            foreach (var enemy in _enemies)
            {
                _map[enemy.x, enemy.y] = UnitType.Enemy;
            }

            var result = string.Empty;
            for (var i = 0; i < _map.GetLength(0); i++)
            {
                for (var j = 0; j < _map.GetLength(1); j++)
                {
                    result += _map[i, j] + " ";
                }

                result += "\n";
            }

            File.WriteAllText(path, result);
        }

        public int Score => _score;

        public void Step(Direction direction)
        {
            (int x, int y) newPosition;
            switch (direction)
            {
                case Direction.Top:
                    newPosition = (_pacman.x, _pacman.y + 1);
                    break;
                case Direction.Right:
                    newPosition = (_pacman.x + 1, _pacman.y);
                    break;
                case Direction.Bottom:
                    newPosition = (_pacman.x, _pacman.y - 1);
                    break;
                case Direction.Left:
                    newPosition = (_pacman.x - 1, _pacman.y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (newPosition.x < 0 || newPosition.x >= _map.GetLength(0) ||
                newPosition.y < 0 || newPosition.y >= _map.GetLength(1) ||
                _map[newPosition.x, newPosition.y] == UnitType.Wall)
            {
                return;
            }

            if (_map[newPosition.x, newPosition.y] == UnitType.Coin)
            {
                _score++;
            }

            _pacman = newPosition;
        }

        public void EnemiesStep()
        {
            for (var index = 0; index < _enemies.Count; index++)
            {
                var enemy = _enemies[index];

                var queue = new Queue<List<(int x, int y)>>();
                queue.Enqueue(new List<(int x, int y)> { enemy });
                var processed = new List<(int x, int y)>();

                while (queue.Any())
                {
                    var path = queue.Dequeue();
                    var pathEnd = path.Last();

                    if (pathEnd == _pacman)
                    {
                        _enemies[index] = path[1];
                        break;
                    }

                    var possibleSteps = new List<(int x, int y)>
                    {
                        (pathEnd.x + 1, pathEnd.y), (pathEnd.x - 1, pathEnd.y),
                        (pathEnd.x, pathEnd.y + 1), (pathEnd.x, pathEnd.y - 1)
                    };

                    foreach (var step in possibleSteps.Where(s => !processed.Contains(s)))
                    {
                        processed.Add(step);

                        if (step.x >= 0 && step.x < _map.GetLength(0) &&
                            step.y >= 0 && step.y < _map.GetLength(1))
                        {
                            queue.Enqueue(path.ToList());
                            queue.Peek().Add(step);
                        }
                    }
                }
            }
        }

        public bool GameOver()
        {
            return _enemies.Any(e => e == _pacman);
        }
    }
}
