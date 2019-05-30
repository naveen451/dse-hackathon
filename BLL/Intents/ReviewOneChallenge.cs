using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using DSEHackatthon.CustomAttributes;
using DSEHackatthon.services.interfaces;
using DSEHackatthon.services.mocks;
using System;
using System.Text;
using System.Linq;

namespace DSEHackatthon.BLL{
    [IntentHandler("review.challenges - one challenge")]
    public class ReviewOneChallenge : BaseHandler
    {
        private IGo365ChallengerService _service;
        public ReviewOneChallenge(Conversation conversation) : base(conversation)
        {
            _service = new Go365ChallengerService();
        }

        public override WebhookResponse Handle(WebhookRequest request){           
            var userChallenges =_conversation.conversationState.UserChallengeModel;    
            
            var challenge = userChallenges.Challenges.FirstOrDefault(x=>x.ChallengeName==_conversation.conversationState.ChallengeName);
             
            //try{
            if(challenge!=null){
                return new WebhookResponse{
                    FulfillmentText= $@"You have {challenge.userStats.DailyAverageSteps} steps
                    and you are in {challenge.userStats.RankWithInTeam} rank with in your team and 
                    you are in {challenge.userStats.OverallRankInThisChallenge} rank overall. Would you like
                    to review Team progress"
  
                };
            }
            //}
            /* catch{
                return new WebhookResponse{
                    FulfillmentText= $@"Sorry I could not perform the action you requested"
  
                };
            }*/
                       
            return new WebhookResponse{
                FulfillmentText=$@"Sorry I could not find any challenges to review"
            };
            

        }



    }
}