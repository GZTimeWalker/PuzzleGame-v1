using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public class MazeProcessRepository : IMazeProcessRepository
    {
        private readonly AppDbContext _context;

        public MazeProcessRepository(AppDbContext context)
        {
            _context = context;
        }

        public Maze.State GetCurState(string token)
        {
            var pro = _context.MazeProcesses.FirstOrDefault(p => string.Equals(p.Token, token));
            if (pro != null)
                return new Maze.State { walls = Maze.Map[pro.x][pro.y], x = pro.x, y = pro.y };
            else
            {
                MazeProcess process = new MazeProcess
                {
                    Token = token,
                    x = 0,
                    y = 0
                };
                _context.MazeProcesses.Add(process);
                _context.SaveChanges();
                return new Maze.State { walls = Maze.Map[0][0], x = 0, y = 0 };
            }
        }

        public bool IsArriveDestination(string token)
        {
            var pro = _context.MazeProcesses.FirstOrDefault(p => string.Equals(p.Token, token));
            return pro != null && pro.x == 63 && pro.y == 63;
        }

        public int Reset(string token)
        {
            var pro = _context.MazeProcesses.FirstOrDefault(p => string.Equals(p.Token, token));
            if (pro != null)
            {
                pro.x = 0;
                pro.y = 0;
                _context.SaveChanges();
                return Maze.Map[0][0];
            }
            else
            {
                MazeProcess process = new MazeProcess
                {
                    Token = token,
                    x = 0,
                    y = 0
                };
                _context.MazeProcesses.Add(process);
                _context.SaveChanges();
                return Maze.Map[0][0];
            }
        }

        public int Step(string token, Maze.Direction direction)
        {
            var pro = _context.MazeProcesses.FirstOrDefault(p => string.Equals(p.Token, token));
            if (pro == null)
                return Maze.Map[0][0];
            else
            {
                Maze.Edges edges = (Maze.Edges)Maze.Map[pro.x][pro.y];
                switch (direction)
                {
                    case Maze.Direction.UP:
                        if (!edges.HasFlag(Maze.Edges.UP))
                            pro.y = pro.y + 1;
                        break;
                    case Maze.Direction.DOWN:
                        if (!edges.HasFlag(Maze.Edges.DOWN))
                            pro.y = pro.y - 1;
                        break;
                    case Maze.Direction.LEFT:
                        if (!edges.HasFlag(Maze.Edges.LEFT))
                            pro.x = pro.x - 1;
                        break;
                    case Maze.Direction.RIGHT:
                        if (!edges.HasFlag(Maze.Edges.RIGHT))
                            pro.x = pro.x + 1;
                        break;
                    default:
                        break;
                }
                _context.SaveChanges();
                return Maze.Map[pro.x][pro.y];
            }
        }
    }
}
