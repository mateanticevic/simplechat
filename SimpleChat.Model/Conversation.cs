using System;
using System.Collections.Generic;

namespace SimpleChat.Model
{
    public class Conversation
    {
        public int NewMessages { get; set; }
        public DateTime? LastActivity { get; set; }
        public IEnumerable<Profile> Profiles { get; set; }
        public string Identifier { get; set; }
    }
}