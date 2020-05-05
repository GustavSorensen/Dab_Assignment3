using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dab_Social_Network.Models
{
    public class SocialNetworkDatabaseSettings : ISocialNetworkDatabaseSettings
    {
        public string UserCollectionName { get; set; }
        public string CircleCollectionName { get; set; }
        public string PostCollectionName { get; set; }
        public string CommentCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string CollectionName { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ISocialNetworkDatabaseSettings
    {
        string UserCollectionName { get; set; }
        string CircleCollectionName { get; set; }
        string PostCollectionName { get; set; }
        string CommentCollectionName { get; set; }
        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
