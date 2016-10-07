using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat.Model
{
    public class Message
    {
        public string Content { get; set; }
        public string Identifier { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
