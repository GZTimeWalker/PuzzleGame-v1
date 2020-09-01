using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public class LiveLikeRepository : ILiveLikeRepository
    {
        private readonly AppDbContext _context;

        public LiveLikeRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckTotalCount() => GetTotal().Count > 2147483647;

        public long GetTotalCount() => GetTotal().Count;

        public bool GetIsEnabled() => GetTotal().IsEnabled;

        public DateTime GetLastUploadTime(string token)
        {
            var like = _context.LiveLikes.FirstOrDefault(l => string.Equals(l.Token, token));
            return like == null ? new DateTime(0) : like.UpdateTimeUTC;
        }

        private LiveLike GetTotal()
        {
            var total = _context.LiveLikes.FirstOrDefault(l => string.Equals(l.Token, "[Total]"));
            if (total == null)
            {
                LiveLike totalLike = new LiveLike
                {
                    Token = "[Total]",
                    IsEnabled = true,
                    Count = 0,
                    UpdateTimeUTC = DateTime.UtcNow,
                };
                _context.LiveLikes.Add(totalLike);
                _context.SaveChanges();
                return totalLike;
            }
            else
                return total;
        }


        public void UpdateUserCount(string token,int append)
        {
            //Update User
            var like = _context.LiveLikes.FirstOrDefault(l => string.Equals(l.Token, token));
            if (like == null)
            {
                LiveLike Like = new LiveLike
                {
                    IsEnabled = true,
                    Token = token,
                    Count = append > 0 ? append : 0,
                    DCount = append < 0 ? append : 0,
                    UpdateTimeUTC = DateTime.UtcNow,
                };
                _context.LiveLikes.Add(Like);
            }
            else
            {
                if (append >= 0)
                    like.Count += append;
                else
                    like.DCount += append;
                like.UpdateTimeUTC = DateTime.UtcNow;
            }
            // Update Total
            var total = GetTotal();
            total.Count += append;
            _context.SaveChanges();
        }

        public void ResetTotal()
        {
            var total = GetTotal();
            total.Count = 0;
            total.UpdateTimeUTC = DateTime.UtcNow;
            total.IsEnabled = true;
            _context.SaveChanges();
        }

        public void LockTotal()
        {
            var total = GetTotal();
            total.IsEnabled = false;
            _context.SaveChanges();
        }

        public IEnumerable<LiveLike> GetLiveLikes() => _context.LiveLikes;
    }
}
