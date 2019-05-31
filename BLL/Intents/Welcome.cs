using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using DSEHackatthon.CustomAttributes;
using DSEHackatthon.services.interfaces;
using DSEHackatthon.services.mocks;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using DSEHackatthon.models;

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
            var accesstoken =request.OriginalDetectIntentRequest.Payload.Fields["user"].StructValue.Fields["accessToken"].StringValue;

             var memberName= GetMemberName(accesstoken);

            //var memberName= request.QueryResult.Parameters.Fields["membername"].StringValue;
                      
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
                FulfillmentText=$@"it's a new day,{userChallenges.UserName}. what can I do for you?"
            };
            /*}
            catch{
                return new WebhookResponse{
                  FulfillmentText="Sorry I could not perform the action you requested"
                };
            } */

        }

        private string GetMemberName(string accessToken){
            
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization 
                         = new AuthenticationHeaderValue("Bearer", accessToken);
            Task<string> s = httpClient.GetStringAsync("https://dev-9kam6mdo.auth0.com/userinfo");
             string r= s.Result;            
            var user =JsonConvert.DeserializeObject<user>(r);
            return user.name;
            
        }
    }
}