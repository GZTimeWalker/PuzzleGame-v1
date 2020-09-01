using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public interface ILevelProcessRepository
    {
        LevelProcess GetLevelProcessByToken(string token);
        Assets.Status AddOrUpdateLevelProcess(LevelProcess process, string code);
        IEnumerable<LevelProcess> GetAllLevelProcesses();
        bool CheckAuthority(string token, int levelID);
        int GetQuantityOfUser();
        int GetNumberOfUserInCurrentLevel(int levelID);
    }
}
