using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public class LiveLike
    {
        [Key]
        public int ID { get; set; }
        public string Token { get; set; }
        public long Count { get; set; }
        public long DCount { get; set; }
        public DateTime UpdateTimeUTC { get; set; }
        public bool IsEnabled { get; set; }
    }
}
