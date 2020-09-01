using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GZTimeServer.Models
{
    public class MazeProcess
    {
        [Key]
        public int ID { get; set; }
        public string Token { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
}
