using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public interface IRankRepository
    {
        void AddPlayer(string token,string ip);
        IEnumerable<Rank> GetAllRanks();
        int GetRankByToken(string token);
    }
}
