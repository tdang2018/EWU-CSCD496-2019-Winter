using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    //Holds the User who is the recipient and the User who is the Santa for each group
    public class Pairing:Entity
    {
        //public Group Group { get; set; }
        public User Santa { get; set; }
        public User Recipient { get; set; }
    }
}
