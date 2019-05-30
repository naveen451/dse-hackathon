using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using DSEHackatthon.CustomAttributes;
using DSEHackatthon.services.interfaces;
using DSEHackatthon.services.mocks;
using System;

namespace DSEHackatthon.BLL{
    [IntentHandler("Welcome.Go365")]
    public class Welcome : BaseHandler
    {
        private IGo365ChallengerService _service;
        public Welcome(Conversation conversation) : base(conversation)
        {
            _service = new Go365ChallengerService();
        }

        public override WebhookResponse Handle(WebhookRequest request){
            var memberName= request.QueryResult.Parameters.Fields["membername"].StringValue;
                      
            string   memberId = _service.GetMemberId(memberName);
            if(memberId==null){
                return new WebhookResponse{
                    FulfillmentText="Sorry we could not find you in our system"
                };
            }           
            //try{
            var userChallenges= _service.GetUserChallenges(memberId);
            _conversation.conversationState.MemberId=memberId; 
            _conversation.conversationState.UserChallengeModel=userChallenges;                 
            return new WebhookResponse{
                FulfillmentText=$@"Hello {userChallenges.UserName}, welcome to challengers service
                what can I do for you? I can provide your challenge progress or I can review your challenges"
            };
            /*}
            catch{
                return new WebhookResponse{
                  FulfillmentText="Sorry I could not perform the action you requested"
                };
            } */

        }
    }
}