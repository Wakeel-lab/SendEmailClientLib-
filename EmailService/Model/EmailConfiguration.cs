using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Model
{
    public class EmailConfiguration
    {
        public string HOST { get; set; }
        public int PORT { get; set; }
        public string USERNAME { get; set; }
        public string PASSWORD { get; set; }
        public string ADDRESS { get; set; }
        public string DISPLAYNAME { get; set; }
    }
}
