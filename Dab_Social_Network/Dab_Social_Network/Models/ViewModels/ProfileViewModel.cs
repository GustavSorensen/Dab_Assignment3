using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dab_Social_Network.Models.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; } = new User();
        public List<Post> UserPosts { get; set; } = new List<Post>();
        public List<Circle> Circles { get; set; } = new List<Circle>();
        public List<Post> CirclePosts { get; set; } = new List<Post>();

    }
}
