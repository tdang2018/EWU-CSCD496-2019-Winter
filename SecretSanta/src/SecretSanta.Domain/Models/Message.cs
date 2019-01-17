using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    //Chat used so that Recipient and Santa can hold an anonymous discussion
    public class Message:Entity
    {
        public User ToUser { get; set; }
        public User FromUser { get; set; }
        public string MessageContent { get; set; }
    }
}
