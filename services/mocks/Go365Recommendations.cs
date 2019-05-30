using DSEHackatthon.services.interfaces;

namespace DSEHackatthon.services.mocks{
    public class Go365Recommendations : IGo365Recommendations
    {
        public string GetRecommendationsForUser(string memberId)
        {
            throw new System.NotImplementedException();
        }

        public string GetRecommendationToCompeteWithFriend(string memberId, string friendName)
        {
            throw new System.NotImplementedException();
        }
    }
}