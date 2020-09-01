using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public class RankRepository : IRankRepository
    {
        private readonly AppDbContext _context;

        public RankRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddPlayer(string token,string ip)
        {
            int max = 0;
            try
            {
                max = _context.Ranks.Select(s => s.Ranking).Max();
            }
            finally
            {
                Rank rank = new Rank
                {
                    Token = token,
                    Ranking = max + 1,
                    UpdateTimeUTC = DateTime.UtcNow,
                    IP = ip,
                };
                _context.Ranks.Add(rank);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Rank> GetAllRanks() => _context.Ranks;

        public int GetRankByToken(string token)
        {
            var r = _context.Ranks.FirstOrDefault(p => string.Equals(p.Token, token));
            if (r != null)
                return r.Ranking;
            else return -1;
        }
    }
}
