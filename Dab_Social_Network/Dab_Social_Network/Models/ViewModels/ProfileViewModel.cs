using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dab_Social_Network.Models.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public IEnumerable<Post> UserPosts { get; set; } = new List<Post>();
        public IEnumerable<Circle> Circles { get; set; } = new List<Circle>();
        public IEnumerable<Post> CirclePosts { get; set; } = new List<Post>();
    }
}
