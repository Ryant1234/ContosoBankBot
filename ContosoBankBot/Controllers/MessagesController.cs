using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ContosoBankBot.Classes;
using ContosoBankBot.DataModels;
using ContosoBankBot.Dialogs;
using ContosoBankBot.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace ContosoBankBot.Controllers
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

                if (userMessage.Length > 6)
                {
                    if (userMessage.ToLower().Substring(0, 7).Equals("convert"))
                    //if (userMessage.ToLower().Contains("convert to"))
                    {

                        string[] words = userMessage.Split(' ');
                        int currencyAmount = int.Parse(words[1]);
                        string convertFrom = words[2].ToUpper();
                        string convertTo = words[4].ToUpper();




                        HttpClient client = new HttpClient();
                        string x = await client.GetStringAsync(new Uri("http://api.fixer.io/latest?base=" + convertFrom //activity.Text
                        ));

                        FixerCurrencyObject.RootObject rootObject;

                        rootObject = JsonConvert.DeserializeObject<FixerCurrencyObject.RootObject>(x);

                        double currencyRate = 0;
                        string message = "";
                        

                        var currencyCoversion = new CurrencyConversion();


                        // This is a blaetent violation of Do Not Repeat yourself.. hopefully can fix in time 
                        switch (convertTo)
                        {


                            case "AUD":
                            currencyRate = rootObject.rates.AUD;
                                message = (currencyCoversion.GetExchangeRate(convertFrom, convertTo, currencyAmount, currencyRate));
                                break;
                            case "BGN":
                                currencyRate = rootObject.rates.BGN;
                                message = (currencyCoversion.GetExchangeRate(convertFrom, convertTo, currencyAmount, currencyRate));
                                break;
                            case "BRL":
                                currencyRate = rootObject.rates.BRL;
                                message = (currencyCoversion.GetExchangeRate(convertFrom, convertTo, currencyAmount, currencyRate));
                                break;
                            case "CAD":
                                currencyRate = rootObject.rates.CAD;
                                message = (currencyCoversion.GetExchangeRate(convertFrom, convertTo, currencyAmount, currencyRate));
                                break;
                            case "CHF":
                                currencyRate = rootObject.rates.CHF;
                                message = (currencyCoversion.GetExchangeRate(convertFrom, convertTo, currencyAmount, currencyRate));
                                break;
                            case "CNY":
                                currencyRate = rootObject.rates.CNY;
                                message = (currencyCoversion.GetExchangeRate(convertFrom, convertTo, currencyAmount, currencyRate));
                                break;
                            case "DKK":
                                currencyRate = rootObject.rates.DKK;
                                message = (currencyCoversion.GetExchangeRate(convertFrom, convertTo, currencyAmount, currencyRate));
                                break;
                            case "GBP":
                                
                                currencyRate = rootObject.rates.GBP;
                                message = (currencyCoversion.GetExchangeRate(convertFrom, convertTo, currencyAmount, currencyRate));
                                break;
                            case "HKD":
                                currencyRate = rootObject.rates.HKD;
                                message = (currencyCoversion.GetExchangeRate(convertFrom, convertTo, currencyAmount, currencyRate));
                                break;
                            case "HRK":
                                currencyRate = rootObject.rates.HRK;
                                message = (currencyCoversion.GetExchangeRate(convertFrom, convertTo, currencyAmount, currencyRate));
                                break;
                            case "HUF":
                                currencyRate = rootObject.rates.HUF;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "IDR":
                                currencyRate = rootObject.rates.IDR;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "ILS":
                                currencyRate = rootObject.rates.ILS;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "INR":
                                currencyRate = rootObject.rates.INR;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "JPY":
                                currencyRate = rootObject.rates.JPY;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "KRW":
                                currencyRate = rootObject.rates.KRW;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "MXN":
                                currencyRate = rootObject.rates.MXN;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "MYR":
                                currencyRate = rootObject.rates.MYR;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "NOK":
                                currencyRate = rootObject.rates.NOK;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "NZD":
                                currencyRate = rootObject.rates.NZD;
                                message = (currencyCoversion.GetExchangeRate(convertFrom, convertTo, currencyAmount, currencyRate));
                                break;
                            case "PHP":
                                currencyRate = rootObject.rates.PHP;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "PLN":
                                currencyRate = rootObject.rates.PLN;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;                
                            case "RON":
                                currencyRate = rootObject.rates.RON;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "RUB":
                                currencyRate = rootObject.rates.RUB;
                                message = (currencyCoversion.GetExchangeRate(convertFrom, convertTo, currencyAmount, currencyRate));
                                break;
                            case "SEK":
                                currencyRate = rootObject.rates.SEK;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "SGD":
                                currencyRate = rootObject.rates.SGD;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "THB":
                                currencyRate = rootObject.rates.THB;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "TRY":
                                currencyRate = rootObject.rates.TRY;
                                message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;
                            case "USD":
                                currencyRate = rootObject.rates.USD;
                                message = currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate);

                                break;
                            case "ZAR":
                                currencyRate = rootObject.rates.ZAR;
                                 message = (currencyCoversion.GetExchangeRate(convertFrom,  convertTo, currencyAmount, currencyRate));
                                break;

                            

                        }


                        ConversionHistory conversionHistory = new ConversionHistory()
                        {
                            CurrencyFrom = convertFrom,
                            CurrencyTo = convertTo,
                            CurrencyAmount = currencyAmount,
                            ExchangeRate = currencyRate,
                            DateChecked = DateTime.Now
                        };

   
                        Activity currencyReply = activity.CreateReply(message);
                        currencyReply.Recipient = activity.From;
                        currencyReply.Type = "message";
                        await connecter.Conversations.SendToConversationAsync(currencyReply);

                        await AzureManager.AzureManagerInstance.AddCurrencyConversionToHistory(conversionHistory);

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