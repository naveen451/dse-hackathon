namespace DSEHackatthon.services.interfaces{
    public interface IGo365Recommendations{
        string GetRecommendationsForUser(string memberId);
        string GetRecommendationToCompeteWithFriend(string memberId,string friendName);

    }
}