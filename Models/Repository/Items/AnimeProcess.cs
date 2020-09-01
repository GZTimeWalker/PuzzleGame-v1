using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace GZTimeServer.Models
{
    public class AnimeProcess
    {
        [Key]
        public int ID { get; set; }
        public string Token { get; set; }
        public bool _0 { get; set; }
        public bool _1 { get; set; }
        public bool _2 { get; set; }
        public bool _3 { get; set; }
    }
}
