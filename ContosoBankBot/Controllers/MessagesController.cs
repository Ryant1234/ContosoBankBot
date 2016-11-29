using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ContosoBankBot.Classes;
using ContosoBankBot.Dialogs;
using ContosoBankBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace ContosoBankBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {


            if (activity.Type == ActivityTypes.Message)
            {
                //await Conversation.SendAsync(activity, () => new ContosoLuisDialog());
                ConnectorClient connecter = new ConnectorClient(new Uri(activity.ServiceUrl));

                StateClient stateClient = activity.GetStateClient();
                BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);

                var userMessage = activity.Text;

                string endOutput = "Hello";


                bool isLuisDialog = true;

                if (userMessage.Length > 10)
                {
                    if (userMessage.ToLower().Substring(0, 10).Equals("Convert To"))
                    {
                        string convertTo = userMessage.Substring(11, 13);
                        int currencyAmount = int.Parse(userMessage.Substring(17));


                       

                        HttpClient client = new HttpClient();
                        string x = await client.GetStringAsync(new Uri("http://api.fixer.io/latest?base=" + "NZD" //activity.Text
                        ));

                        FixerCurrencyObject.RootObject rootObject;

                        rootObject = JsonConvert.DeserializeObject<FixerCurrencyObject.RootObject>(x);
                        double currencyRate;

                        switch (convertTo)
                        {


                            case "AUD":
                            currencyRate = rootObject.rates.AUD;
                                break;
                            case "BGN":
                                currencyRate = rootObject.rates.BGN;
                                break;
                            case "BRL":
                                currencyRate = rootObject.rates.BRL;
                                break;
                            case "CAD":
                                currencyRate = rootObject.rates.CAD;
                                break;
                            case "CHF":
                                currencyRate = rootObject.rates.CHF;
                                break;
                            case "CNY":
                                currencyRate = rootObject.rates.CNY;
                                break;
                            case "DKK":
                                currencyRate = rootObject.rates.DKK;
                                break;
                            case "GBP":
                                
                                currencyRate = rootObject.rates.GBP;
                                break;
                            case "HKD":
                                currencyRate = rootObject.rates.HKD;
                                break;
                            case "HRK":
                                currencyRate = rootObject.rates.HRK;
                                break;
                            case "HUF":
                                currencyRate = rootObject.rates.HUF;
                                break;
                            case "IDR":
                                currencyRate = rootObject.rates.IDR;
                                break;
                            case "ILS":
                                currencyRate = rootObject.rates.ILS;
                                break;
                            case "INR":
                                currencyRate = rootObject.rates.INR;
                                break;
                            case "JPY":
                                currencyRate = rootObject.rates.JPY;
                                break;
                            case "KRW":
                                currencyRate = rootObject.rates.KRW;
                                break;
                            case "MXN":
                                currencyRate = rootObject.rates.MXN;
                                break;
                            case "MYR":
                                currencyRate = rootObject.rates.MYR;
                                break;
                            case "NOK":
                                currencyRate = rootObject.rates.NOK;
                                break;
                            case "PHP":
                                currencyRate = rootObject.rates.PHP;
                                break;
                            case "PLN":
                                currencyRate = rootObject.rates.PLN;
                                break;                
                            case "RON":
                                currencyRate = rootObject.rates.RON;
                                break;
                            case "RUB":
                                currencyRate = rootObject.rates.RUB;
                                break;
                            case "SEK":
                                currencyRate = rootObject.rates.SEK;
                                break;
                            case "SGD":
                                currencyRate = rootObject.rates.SGD;
                                break;
                            case "THB":
                                currencyRate = rootObject.rates.THB;
                                break;
                            case "TRY":
                                currencyRate = rootObject.rates.TRY;
                                break;
                            case "USD":
                                currencyRate = rootObject.rates.USD;
                                break;
                            case "ZAR":
                                currencyRate = rootObject.rates.ZAR;
                                break;



                        }




                        ;
                        //double currencyRate = rootObject.rates.convertTo;
                       

                        var currencyCoversion = new CurrencyConversion();
                     var exchangeRate = ( currencyCoversion.GetExchangeRate( convertTo, currencyAmount));
                    



                    }
                }


            
            }

            else
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                var reply = HandleSystemMessage(activity);
                if (reply != null)
                    await connector.Conversations.ReplyToActivityAsync(reply);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }








        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels

                string replyMessage = string.Empty;
                replyMessage += "Hi there\n\n";

                replyMessage += "I am ContasoBot and I am here to help you  \n";


                replyMessage += "Currently I have following features  \n";

                replyMessage += "* '\n\n";

                replyMessage += "If you require help at any time, please type help";

                return message.CreateReply(replyMessage);

            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }













    }
}