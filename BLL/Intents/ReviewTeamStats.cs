using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using DSEHackatthon.CustomAttributes;
using DSEHackatthon.services.interfaces;
using DSEHackatthon.services.mocks;
using System;
using System.Text;
using System.Linq;

namespace DSEHackatthon.BLL{
    [IntentHandler("review.challenges - teamstats - yes")]
    [IntentHandler("review.challenges - custom - teamstats-yes")]
    [IntentHandler("team-stats")]
    public class ReviewTeamStats : BaseHandler
    {
        private IGo365ChallengerService _service;
        public ReviewTeamStats(Conversation conversation) : base(conversation)
        {
            _service = new Go365ChallengerService();
        }

        public override WebhookResponse Handle(WebhookRequest request){  
                  
            var userChallenges =_conversation.conversationState.UserChallengeModel;    
            
            var challenge = userChallenges.Challenges.FirstOrDefault(x=>x.ChallengeName==_conversation.conversationState.ChallengeName);
             
            //try{
            if(challenge!=null){
                return new WebhookResponse{
                    FulfillmentText= $@"Your team have {challenge.teamStats.DailyAverageSteps} steps
                    and in {challenge.teamStats.OverallRankInThisChallenge} with {challenge.teamStats.topper.contentderGivenName}
                    in leading position with {challenge.teamStats.topper.contentderStats.DailyAverageSteps} daily average steps"
  
                };
            }
            /*           catch{
                return new WebhookResponse{
                    FulfillmentText= $@"Sorry I could not perform the action you requested"
  
                };
            }*/
                       
            return new WebhookResponse{
                FulfillmentText=$@"Sorry I could not find challenge to review"
            };
            

        }



    }
}