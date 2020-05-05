using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dab_Social_Network.Models.ViewModels
{
    public class PostViewModel
    {
        public Post Post { get; set; }

        public User User { get; set; }
        public List<User> Users { get; set; }

        public Circle Circle { get; set; }
        public List<Circle> Circles { get; set; }

        public Comment Comment { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
