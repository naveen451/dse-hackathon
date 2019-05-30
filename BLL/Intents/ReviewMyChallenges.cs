using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using DSEHackatthon.CustomAttributes;
using DSEHackatthon.services.interfaces;
using DSEHackatthon.services.mocks;
using System;
using System.Text;

namespace DSEHackatthon.BLL{
    [IntentHandler("review.challenges")]
    public class ReviewMyChallenges : BaseHandler
    {
        private IGo365ChallengerService _service;
        public ReviewMyChallenges(Conversation conversation) : base(conversation)
        {
            _service = new Go365ChallengerService();
        }

        public override WebhookResponse Handle(WebhookRequest request){           
            var userChallenges =_conversation.conversationState.UserChallengeModel;    

            if(userChallenges.Challenges.Count>1){    

                var sb = new StringBuilder();

                foreach(var challenge in userChallenges.Challenges){
                    sb.AppendLine(challenge.ChallengeName);
                }

                var s= sb.ToString();

                return new WebhookResponse{
                FulfillmentText=$@"I see you have {userChallenges.Challenges.Count} active challenges. {s}. What would like to review "
            };
            }
            else if(userChallenges.Challenges.Count==1){
                _conversation.conversationState.ChallengeName=userChallenges.Challenges[0].ChallengeName;
                return new WebhookResponse{
                FulfillmentText=$@"I see you have 1 active challenge, {userChallenges.Challenges[0].ChallengeName}",
                
            };

            }
                       
            return new WebhookResponse{
                FulfillmentText=$@"Sorry I could not find any challenges to review"
            };
            

        }



    }
}