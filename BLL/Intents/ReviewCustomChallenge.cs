using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using DSEHackatthon.CustomAttributes;
using DSEHackatthon.services.interfaces;
using DSEHackatthon.services.mocks;
using System;
using System.Text;
using System.Linq;

namespace DSEHackatthon.BLL{
    [IntentHandler("review.challenges - custom")]
    [IntentHandler("get-challenge-stats")]
    public class ReviewCustomChallenge : BaseHandler
    {
        private IGo365ChallengerService _service;
        public ReviewCustomChallenge(Conversation conversation) : base(conversation)
        {
            _service = new Go365ChallengerService();
        }

        public override WebhookResponse Handle(WebhookRequest request){  
            var challengeName =  request.QueryResult.Parameters.Fields["Challengename"].StringValue;  
            _conversation.conversationState.ChallengeName=challengeName;      
            var userChallenges =_conversation.conversationState.UserChallengeModel;    
            
            var challenge = userChallenges.Challenges.FirstOrDefault(x=>x.ChallengeName==challengeName);
             
            //try{
            if(challenge!=null){
                return new WebhookResponse{
                    FulfillmentText= $@"You have {challenge.userStats.DailyAverageSteps} steps
                    and you are in {challenge.userStats.RankWithInTeam} rank with in your team and 
                    you are in {challenge.userStats.OverallRankInThisChallenge} rank overall. Would 
                    you like to review team progress"
  
                };
            }
            //}
            /*catch{
                return new WebhookResponse{
                    FulfillmentText= $@"Sorry I could not perform the action you requested"
  
                };
            }*/
                       
            return new WebhookResponse{
                FulfillmentText=$@"Sorry I could not find {challengeName} to review"
            };
            

        }



    }
}