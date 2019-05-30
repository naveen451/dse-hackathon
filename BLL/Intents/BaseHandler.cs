using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;

namespace DSEHackatthon.BLL{

    public abstract class BaseHandler{
       
       protected readonly Conversation _conversation;

       public BaseHandler(Conversation conversation){
            _conversation= conversation;
       }

       public virtual Task<WebhookResponse> HandleAsync(WebhookRequest WebhookRequest){
             return Task.FromResult<WebhookResponse>(null);
       }

       public virtual WebhookResponse Handle(WebhookRequest request){
           return null;
       }

    }
}