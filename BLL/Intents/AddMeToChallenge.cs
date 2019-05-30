using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using DSEHackatthon.CustomAttributes;


namespace DSEHackatthon.BLL.Intents
{
    [IntentHandler("Add.ToChallenge")]
    public class AddMeToChallenge : BaseHandler
    {
        public AddMeToChallenge(Conversation conversation) : base(conversation)
        {

        }

        public override async Task<WebhookResponse> HandleAsync(WebhookRequest request){
            var challengeName = request.QueryResult.Parameters.Fields["Challengename"].StringValue;
            //call go365 challenge service to add challengename for a given memeberid
            await Task.Delay(100);

            return new WebhookResponse{
                FulfillmentText=$"You have been successfully added to {challengeName} challenge."
            };
        }

    }

}