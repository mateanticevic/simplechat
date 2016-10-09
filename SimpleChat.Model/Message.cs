using System;

namespace SimpleChat.Model
{
    public class Message
    {
        public string Content { get; set; }
        public string Identifier { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
