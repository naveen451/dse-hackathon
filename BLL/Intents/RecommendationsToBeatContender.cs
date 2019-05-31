using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using DSEHackatthon.CustomAttributes;
using DSEHackatthon.services.interfaces;
using DSEHackatthon.services.mocks;
using System;
using System.Text;
using System.Linq;

namespace DSEHackatthon.BLL{   
    [IntentHandler("recommed-to-beat-contender")]
    public class RecommendationsToBeatContender : BaseHandler
    {
        private IGo365ChallengerService _service;
        public RecommendationsToBeatContender(Conversation conversation) : base(conversation)
        {
            _service = new Go365ChallengerService();
        }

        public override WebhookResponse Handle(WebhookRequest request){  
                  
            var contenderName = _conversation.conversationState.ContenderName;
           var userChallenges =_conversation.conversationState.UserChallengeModel;    
            
            var challenge = userChallenges.Challenges.FirstOrDefault(x=>x.ChallengeName==_conversation.conversationState.ChallengeName);
              
            var contenderStats= challenge.contentders.FirstOrDefault(x=>x.contentderGivenName==contenderName);
            
            

             
            //try{
            if(contenderStats!=null){
                _conversation.conversationState.ContenderName=contenderName;
                int userSteps;

            Int32.TryParse(challenge.userStats.DailyAverageSteps,out userSteps);
            
            int contentderSteps;

            Int32.TryParse(contenderStats.contentderStats.DailyAverageSteps,out contentderSteps); 

            int difSteps = userSteps-contentderSteps;       

             if(difSteps>0){
                   return new WebhookResponse{
                    FulfillmentText= $@"Congrats, you are already {difSteps} steps ahead of {contenderStats.contentderGivenName}. keep up."  
                };
             }
             if(difSteps==0){
                   return new WebhookResponse{
                    FulfillmentText= $@"you and {contenderStats.contentderGivenName} have same numnber of steps. keep up and try to be more active"  
                };
             }

                return new WebhookResponse{
                    FulfillmentText= $@"{contenderStats.contentderGivenName} is leading by {contentderSteps-userSteps} steps.just try to more active to beat  {contenderStats.contentderGivenName}.
                    you may want to register for the Humana walkathon at Tom Saywer park in Louisville on june 8th "  
                };
            }
            /*           catch{
                return new WebhookResponse{
                    FulfillmentText= $@"Sorry I could not perform the action you requested"
  
                };
            }*/
                       
            return new WebhookResponse{
                FulfillmentText=$@"{contenderName} is not enrolled in this challenge. can I help you with something else?"
            };
            

            

        }



    }
}