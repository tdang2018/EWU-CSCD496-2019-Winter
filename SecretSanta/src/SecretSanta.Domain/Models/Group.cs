using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    //Will have a title and a list of User's who are part of that group. 
    //Users can belong to more than one group
    public class Group : Entity
    {
        public string Title { get; set; }
        public List<User> Users { get; set; }
    }

}
