using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace ContosoBankBot.Dialogs
{

    [LuisModel("21c1cd7a-f48d-4b35-9f82-5d629bd5a5cf", "01254d8c242a44fa9727582e0eebc2c4")]
    [Serializable]
    public class ContosoLuisDialog : LuisDialog<object>
    {

        [LuisIntent("WhyHaveBranchesClosed")]
        public async Task WhyHaveBranchesClosed(IDialogContext context, LuisResult result)
        {
            await
                context.PostAsync(
                    "Due to the lack of Customers visiting our Redmond branches, we have closed them"); 
                                  
            context.Wait(MessageReceived);

        }


    }
}