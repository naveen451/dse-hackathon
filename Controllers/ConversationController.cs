using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DSEHackatthon.BLL;

namespace DSEHackatthon.Controllers
{
    public class ConverationController:Controller{
   
    private Agent _agent;
    public ConverationController(){
       _agent = new Agent();
    }

    [Route("Conversation")]
    public async Task<ActionResult> Conversation(){
        var response= await _agent.HandleRequest(Request);
        var responseJson = response.ToString();
        return Content(responseJson, "application/json; charset=utf-8");
    }

    }
}