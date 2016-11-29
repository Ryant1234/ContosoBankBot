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



        [LuisIntent("GetStockValue")]
        public async Task GetStockValue(IDialogContext context, LuisResult result)

        {
            string stockName = string.Empty;
            string replyText = string.Empty;

            try
            {


                replyText = GenerateResponseForStockValue(stockName, 5);
                await context.PostAsync(replyText);
            }
            catch (Exception)
            {

                await context.PostAsync("Something went wrong, please try again later");
            }
            finally
            {
                context.Wait(MessageReceived);
            }

        }





        /// <summary>
        /// Generates a response for the requested stock value
        /// </summary>
        /// <param name="stockname"></param>
        /// <returns></returns>
        private string GenerateResponseForStockValue(string stockname, int value)
        {
            if (string.IsNullOrWhiteSpace(stockname))
            {
                return "Error: Nothing detected";
            }

            //string replyMessage = string.Empty;
           string replyMessage = $"The value of {stockname} is currently ${value}";

            return replyMessage;
        }

    }
}
