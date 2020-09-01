using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public class CodeKey
    {
        [Key]
        public int ID { get; set; }
        public string Token { get; set; }
        public string Key { get; set; }
    }
}
