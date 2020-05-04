using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dab_Social_Network.Models.ViewModels
{
    public class PostViewModel
    {
        public Post post { get; set; }

        public User user { get; set; }
        public List<User> users { get; set; }

        public Circle circle { get; set; }
        public List<Circle> circles { get; set; }

        public Comment comment { get; set; }
        public List<Comment> comments { get; set; }
    }
}
