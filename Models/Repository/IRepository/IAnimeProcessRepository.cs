using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public interface IAnimeProcessRepository
    {
        bool IsSatisfyRequiredCount(string token);
        bool UpdateAnimeProcess(string token,int order, string info);
        IEnumerable<AnimeProcess> GetAllAnimeProcesses();
    }
}
