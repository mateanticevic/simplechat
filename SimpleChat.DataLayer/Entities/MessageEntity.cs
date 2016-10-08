using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat.DataLayer.Entities
{
    public class MessageEntity
    {
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string Identifier { get; set; }
        public string Nickname { get; set; }
    }
}
