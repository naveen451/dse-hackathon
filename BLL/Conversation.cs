using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using System;
using System.Linq;
using DSEHackatthon.CustomAttributes;

namespace DSEHackatthon.BLL
{
    public class Conversation{

        public State conversationState;

        public Conversation(){
            conversationState= new State();
        }
       public async Task<WebhookResponse> HandleRequest(WebhookRequest request){
         var intentName = request.QueryResult.Intent.DisplayName;
         var handler = FindHandler(intentName);
         if(handler==null){
             return new WebhookResponse{
                   FulfillmentText= $"Sorry I could not find an intent you requested {intentName}"
             };
         }
         //try{
         return handler.Handle(request)??
              await handler.HandleAsync(request)??
              new WebhookResponse{
                  FulfillmentText="Error, Handler did not return a valid response",
              };
         //}
         /*catch(Exception e){
             return new WebhookResponse{
                 FulfillmentText="Sorry I could not perform an action you requested"
             };
         }*/
       }

       private BaseHandler FindHandler(string handlerName){

           var baseHandlerTypes = typeof(BaseHandler).Assembly.GetTypes().
           Where(t=>t.IsClass && t.IsSubclassOf(typeof(BaseHandler)) );        

           var typeList = from baseHandlerType in baseHandlerTypes
                          from attribute in baseHandlerType.GetCustomAttributes(typeof(IntentHandlerAttribute),true)
                          where ((IntentHandlerAttribute)attribute).Name==handlerName
                          select baseHandlerType;
            
            var type= typeList.FirstOrDefault();
            if(type==null)return null;
            var constructorInfo = type.GetConstructor(new[] { GetType() });
            var instance = (BaseHandler)constructorInfo.Invoke(new object[] { this });
            return instance;          
           
                                
       }
    }
}