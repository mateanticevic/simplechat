using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat.DataLayer.Entities
{
    public class ConversationEntity
    {
        public DateTime? LastActivity { get; set; }
        public int NewMessages { get; set; }
        public string Identifier { get; set; }
    }
}
