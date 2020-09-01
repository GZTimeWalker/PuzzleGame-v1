using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public class Rank
    {
        [Key]
        public int ID { get; set; }
        public int Ranking { get; set; }
        public string Token { get; set; }
        public string IP { get; set; }
        public DateTime UpdateTimeUTC { get; set; }
    }
}
