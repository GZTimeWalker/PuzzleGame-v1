using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public interface ILiveLikeRepository
    {
        public void ResetTotal();
        public bool GetIsEnabled();
        public void LockTotal();
        public void UpdateUserCount(string token,int append);
        public DateTime GetLastUploadTime(string token);
        public bool CheckTotalCount();
        public long GetTotalCount();
        public IEnumerable<LiveLike> GetLiveLikes();
    }
}
