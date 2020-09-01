using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public class AnimeProcessRepository : IAnimeProcessRepository
    {
        private readonly AppDbContext _context;

        public AnimeProcessRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AnimeProcess> GetAllAnimeProcesses()
            => _context.AnimeProcesses;

        public bool IsSatisfyRequiredCount(string token)
        {
            var pro = _context.AnimeProcesses.FirstOrDefault(p => p.Token == token);
            return pro != null && 
                    Convert.ToInt32(pro._1) +
                    Convert.ToInt32(pro._2) +
                    Convert.ToInt32(pro._3) +
                    Convert.ToInt32(pro._0) >= 2;
        }

        public bool UpdateAnimeProcess(string token, int order, string info)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;

            if (string.Equals(info, Assets.AnimeAns[order]))
            {
                var pro = _context.AnimeProcesses.FirstOrDefault(p => p.Token == token);
                if (pro != null)
                {
                    switch (order)
                    {
                        case 0: pro._0 = true; break;
                        case 1: pro._1 = true; break;
                        case 2: pro._2 = true; break;
                        case 3: pro._3 = true; break;
                        default: break;
                    }
                    _context.SaveChanges();
                }
                else
                {
                    AnimeProcess process = new AnimeProcess { 
                        Token = token ,_0 = false,_1 = false,_2 = false,_3 = false };
                    switch (order)
                    {
                        case 0: process._0 = true; break;
                        case 1: process._1 = true; break;
                        case 2: process._2 = true; break;
                        case 3: process._3 = true; break;
                        default: break;
                    }
                    _context.AnimeProcesses.Add(process);
                    _context.SaveChanges();
                }
                return true;
            }
            return false;
        }
    }
}