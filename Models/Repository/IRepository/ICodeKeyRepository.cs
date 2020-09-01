using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public interface ICodeKeyRepository
    {
        void SaveCodeKey(string token, string key);
        bool CheckCodeKey(string token, string key);
    }
}
