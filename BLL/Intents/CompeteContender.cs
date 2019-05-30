using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using DSEHackatthon.CustomAttributes;
using DSEHackatthon.services.interfaces;
using DSEHackatthon.services.mocks;
using System;
using System.Text;
using System.Linq;

namespace DSEHackatthon.BLL{   
    [IntentHandler("compete-contender")]
    public class CompeteContender : BaseHandler
    {
        private IGo365ChallengerService _service;
        public CompeteContender(Conversation conversation) : base(conversation)
        {
            _service = new Go365ChallengerService();
        }

        public override WebhookResponse Handle(WebhookRequest request){  
                  
            var contenderName = request.QueryResult.Parameters.Fields["membername"].StringValue;

           

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
                    FulfillmentText= $@"Congrats, you are {difSteps} steps ahead of {contenderStats.contentderGivenName}.
                    keep up."  
                };
             }
             if(difSteps==0){
                   return new WebhookResponse{
                    FulfillmentText= $@"you are and {contenderStats.contentderGivenName} has same numnber of steps.
                    keep up."  
                };
             }

                return new WebhookResponse{
                    FulfillmentText= $@"you are {contentderSteps-userSteps} steps lagging to {contenderStats.contentderGivenName}.
                    just try to more active to beat  {contenderStats.contentderGivenName}."  
                };
            }
            /*           catch{
                return new WebhookResponse{
                    FulfillmentText= $@"Sorry I could not perform the action you requested"
  
                };
            }*/
                       
            return new WebhookResponse{
                FulfillmentText=$@"Sorry I could not find {contenderName}"
            };
            

        }



    }
}