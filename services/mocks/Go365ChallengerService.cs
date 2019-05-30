using DSEHackatthon.models;
using DSEHackatthon.services.interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace DSEHackatthon.services.mocks{
    public class Go365ChallengerService : IGo365ChallengerService
    {

        private static Dictionary<string,string> _memberDetails = new Dictionary<string,string>();

        public Go365ChallengerService(){
            _memberDetails= new Dictionary<string, string>{
               {"Naveen","111444"},
               {"Jatin","111445"},
               {"Sreeni","111446"},
               {"Sravan","111447"}
            };
        }

        public void AddChallenge(string memberId, string challengeName)
        {
            throw new System.NotImplementedException();
        }

        public string GetChallengeDetails(string challengeName)
        {
            throw new System.NotImplementedException();
        }

        public string GetNewChallengeForUser(string memberId)
        {
            throw new System.NotImplementedException();
        }

        public UserChallengeModel GetUserChallenges(string memberId)
        {         
            UserChallengeModel userChallenges = JsonConvert.DeserializeObject<UserChallengeModel>(File.ReadAllText($@"{memberId}.json"));
            return userChallenges;
          
        }

        public string GetMemberId(string memberName){
            string memberId;
           if(!_memberDetails.TryGetValue(memberName,out memberId))
           {
              return null;
           }
           return memberId;
        }

        public string GetUserFriendStats(string memberId, string friendName)
        {
            throw new System.NotImplementedException();
        }

        public string GetUserStats(string memberId, string challengeName)
        {
            throw new System.NotImplementedException();
        }

        public string GetUserTeamStats(string memberId, string challengeName)
        {
            throw new System.NotImplementedException();
        }
    }
}