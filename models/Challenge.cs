using System.Collections.Generic;

namespace DSEHackatthon.models{
    public class Challenge{
        public string ChallengeName{get;set;}

        public UserStats userStats{get;set;}

        public TeamStats teamStats{get;set;}

        public List<Contentder> contentders{get;set;}


    }
}