using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat.Model
{
    public class Conversation
    {
        public bool HasNewMessages { get; set; }
        public string Identifier { get; set; }
        public DateTime LastActivity { get; set; }
    }
}
