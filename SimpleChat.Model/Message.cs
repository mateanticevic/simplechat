using System;

namespace SimpleChat.Model
{
    public class Message
    {
        public bool CanDelete { get; set; }
        public DateTime Timestamp { get; set; }
        public Profile Profile { get; set; }
        public string Content { get; set; }
        public string Identifier { get; set; }
    }
}
