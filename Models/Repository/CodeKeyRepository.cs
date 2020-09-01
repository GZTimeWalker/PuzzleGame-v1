using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public class CodeKeyRepository : ICodeKeyRepository
    {
        private readonly AppDbContext _context;

        public CodeKeyRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckCodeKey(string token, string key)
        {
            var _ck = _context.CodeKeys.FirstOrDefault(k => string.Equals(k.Token, token));
            return _ck != null && string.Equals(_ck.Key, key);
        }

        public void SaveCodeKey(string token, string key)
        {
            if (string.IsNullOrWhiteSpace(token)) return;
            CodeKey ck = new CodeKey
            {
                Token = token,
                Key = key,
            };
            var _ck = _context.CodeKeys.FirstOrDefault(k => string.Equals(k.Token,token));
            if (_ck == null) _context.CodeKeys.Add(ck);
            else _ck.Key = key;
            _context.SaveChanges();
        }
    }
}
