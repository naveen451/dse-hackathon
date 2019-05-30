using System.Collections.Generic;
namespace DSEHackatthon.models{
    public class UserChallengeModel{

        public string UserName{get;set;}
        public List<Challenge> Challenges{get;set;}
    }
}