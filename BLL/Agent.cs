using System.IO;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace DSEHackatthon.BLL{
    public class Agent{
       
        private static Dictionary<string, Conversation> conversations = new Dictionary<string, Conversation>();

        private static readonly JsonParser jsonParser =
            new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true));
        
        public async Task<WebhookResponse> HandleRequest(HttpRequest httpRequest){
           WebhookRequest request ;
           var reader = new StreamReader(httpRequest.Body);
           request= jsonParser.Parse<WebhookRequest>(reader);
           var conversation = GetConversation(request.Session);
           return await conversation.HandleRequest(request);

        }

        private Conversation GetConversation(string requestSessionId){
            Conversation conversation;
            lock(conversations){            
            if(!conversations.TryGetValue(requestSessionId,out conversation)){
               conversation = new Conversation();
               conversations.Add(requestSessionId,conversation);
            }
            }
            return conversation;
        }
    }
}