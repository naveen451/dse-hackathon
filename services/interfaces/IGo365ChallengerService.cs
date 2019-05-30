using DSEHackatthon.models;

namespace DSEHackatthon.services.interfaces{
    public interface IGo365ChallengerService{

        string GetMemberId(string memberName);
        UserChallengeModel GetUserChallenges(string memberId);
        string GetUserStats(string memberId,string challengeName);
        string GetUserTeamStats(string memberId,string challengeName);
        string GetUserFriendStats(string memberId,string friendName);
        string GetNewChallengeForUser(string memberId);
        string GetChallengeDetails(string challengeName);
        void AddChallenge(string memberId,string challengeName);      
    }
}