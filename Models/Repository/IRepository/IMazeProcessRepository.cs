using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public interface IMazeProcessRepository
    {
        public int Step(string token,Maze.Direction direction);
        public Maze.State GetCurState(string token);
        public bool IsArriveDestination(string token);
        public int Reset(string token);
    }
}
