using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public class LevelProcessRepository : ILevelProcessRepository
    {
        private readonly AppDbContext _context;

        public LevelProcessRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 添加或更新关卡进度
        /// </summary>
        /// <param name="process">进度</param>
        /// <param name="code">识别码</param>
        public Assets.Status AddOrUpdateLevelProcess(LevelProcess process, string code)
        {
            if (Assets.CheckLevelProcess(process, code))
            {
                var pro = _context.LevelProcesses.FirstOrDefault(p => string.Equals(p.Token, process.Token));
                if (pro != null)
                {
                    if(pro.LevelID + 1 == process.LevelID)
                    {
                        pro.LevelID = process.LevelID;
                        pro.LevelName = process.LevelName;
                        pro.IP = process.IP;
                        pro.UpdateTimeUTC = process.UpdateTimeUTC;
                        _context.SaveChanges();
                    }
                    else if(pro.LevelID >= process.LevelID)
                    {
                        pro.IP = process.IP;
                        pro.UpdateTimeUTC = process.UpdateTimeUTC;
                        _context.SaveChanges();
                    }
                    else
                    {
                        return Assets.Status.NotFound;
                    }
                    return Assets.Status.Success;
                }
                else if (process.LevelID == 0)
                {
                    _context.Add(process);
                    _context.SaveChanges();
                    return Assets.Status.Success;
                }
                else
                {
                    return Assets.Status.NotFound;
                }
            }
            return Assets.Status.Fail;
        }

        public bool CheckAuthority(string token, int levelID)
        {
            var pro = _context.LevelProcesses.FirstOrDefault(p => string.Equals(p.Token, token));
            return pro != null && levelID <= pro.LevelID + 1;
        }

        public IEnumerable<LevelProcess> GetAllLevelProcesses()
            => _context.LevelProcesses;

        public LevelProcess GetLevelProcessByToken(string token) 
            => _context.LevelProcesses.FirstOrDefault(l => string.Equals(token, l.Token));

        public int GetNumberOfUserInCurrentLevel(int levelID) 
            => _context.LevelProcesses.Count(p => p.LevelID == levelID);

        public int GetQuantityOfUser() 
            => _context.LevelProcesses.Count();
    }
}
