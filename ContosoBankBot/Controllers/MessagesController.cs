using System;
using System.Collections.Generic;
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
        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            // Features to add in the future will include user accounts 

            if (activity.Type == ActivityTypes.Message)
            {





                //await Conversation.SendAsync(activity, () => new ContosoLuisDialog());
                ConnectorClient connecter = new ConnectorClient(new Uri(activity.ServiceUrl));

                StateClient stateClient = activity.GetStateClient();
                BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
                string endOutput = "";
                var userMessage = activity.Text;

                // calculate something for us to return


                //bool isLuisDialog = true;













                if (userMessage.ToLower().Substring(0, 4).Equals("help"))
                {
                    // Need more content here 
                    string message = "* For help with Currency Conversion please type currencyhelp" + "\n\n";
                    message += "* To set your name type 'My name is (name) , for example if your Name is Bob "
                               + "type My name is Bob";
                    Activity helpReply = activity.CreateReply(message);
                    helpReply.Recipient = activity.From;
                    helpReply.Type = "message";
                    await connecter.Conversations.SendToConversationAsync(helpReply);

                }


                if (userMessage.ToLower().Contains("currencyhelp"))
                {

                    string message = "* For example type Convert 100 NZD to AUD to convert " +
                                     "100 New Zealand Dollars to Australian Dollars " +
                                     "* To view previous currency conversions type 'get conversion history'";

                    Activity currencyhelpReply = activity.CreateReply(message);
                    currencyhelpReply.Recipient = activity.From;
                    currencyhelpReply.Type = "message";
                    currencyhelpReply.Attachments = new List<Attachment>();
                    List<CardImage> cardImages = new List<CardImage>();

                    cardImages.Add(
                        new CardImage(
                            url:
                            "https://www.moneysupermarket.com/medias/sys_master/h47/hc9/8819256295454/world-currencies.jpg"));
                    List<CardAction> cardButtons = new List<CardAction>();
                    CardAction plButton = new CardAction()
                    {
                        Value = "http://fixer.io/",
                        Type = "openUrl",
                        Title = "Our Conversion Rates Source"
                    };
                    cardButtons.Add(plButton);
                    ThumbnailCard plCard = new ThumbnailCard()
                    {
                        Title = "Fixer.Io",
                        Images = cardImages,
                        Buttons = cardButtons
                    };
                    Attachment plAttachment = plCard.ToAttachment();
                    currencyhelpReply.Attachments.Add(plAttachment);

                    await connecter.Conversations.SendToConversationAsync(currencyhelpReply);

                }


                if (userMessage.ToLower().Contains("hello") || userMessage.ToLower().Contains("hey"))
                {

                    string message = "* Hello there. If you need help type 'help'";

                    Activity currencyhelpReply = activity.CreateReply(message);
                    currencyhelpReply.Recipient = activity.From;
                    currencyhelpReply.Type = "message";
                    await connecter.Conversations.SendToConversationAsync(currencyhelpReply);

                }




                if (userMessage.Length > 7)
                {

                    if (userMessage.ToLower().Substring(0, 10).Equals("my name is"))
                    {
                        string myName = userMessage.Substring(11);
                        userData.SetProperty<string>("CustomerName", myName);
                        await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);
                        Activity setNameReply = activity.CreateReply(myName + " has been set as your name");
                        setNameReply.Recipient = activity.From;
                        setNameReply.Type = "message";
                        await connecter.Conversations.SendToConversationAsync(setNameReply);

                    }


                    if (userMessage.ToLower().Contains("what is my name"))
                    {
                        string myName = userData.GetProperty<string>("CustomerName");
                        if (myName == null)
                        {
                            endOutput = "You have not told me your name, for example, if your name is Bob type " +
                                        "My name is Bob";

                        }
                        else
                        {
                            endOutput = myName;
                        }

                        Activity customerNameReply = activity.CreateReply(endOutput);
                        customerNameReply.Recipient = activity.From;
                        customerNameReply.Type = "message";
                        await connecter.Conversations.SendToConversationAsync(customerNameReply);

                    }






                    if (userMessage.ToLower().Contains("get conversion history"))
                    {
                        List<ConversionHistory> history = await AzureManager.AzureManagerInstance.GetConversionHistory();

                        foreach (ConversionHistory t in history)
                        {
                            endOutput += " [" + t.DateChecked + "] you converted " + t.CurrencyAmount + " " +
                                         t.CurrencyFrom + " to " + t.CurrencyTo
                                         + " the converted amount was $" + t.ConvertedAmount + "\n\n";
                        }

                        Activity currencyReply = activity.CreateReply(endOutput);
                        currencyReply.Recipient = activity.From;
                        currencyReply.Type = "message";
                        await connecter.Conversations.SendToConversationAsync(currencyReply);

                    }











                    if (userMessage.ToLower().Substring(0, 7).Equals("convert"))

                    {

                        string[] words = userMessage.Split(' ');
                        int currencyAmount = int.Parse(words[1]);
                        string convertFrom = words[2].ToUpper();
                        string convertTo = words[4].ToUpper();




                        HttpClient client = new HttpClient();
                        string x =
                            await
                                client.GetStringAsync(new Uri("http://api.fixer.io/latest?base=" + convertFrom
                                    //activity.Text
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

                            case "NZD":
                                currencyRate = rootObject.rates.NZD;
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


                        double convertedAmount =
                        (currencyCoversion.GetExchangeRate(convertFrom, convertTo, currencyAmount,
                            currencyRate));



                        message = $"{currencyAmount} {convertFrom} converts to {convertedAmount} {convertTo}";
                        Activity currencyReply = activity.CreateReply(message);
                        currencyReply.Recipient = activity.From;
                        currencyReply.Type = "message";
                        await connecter.Conversations.SendToConversationAsync(currencyReply);


                        ConversionHistory conversionHistory = new ConversionHistory()
                        {
                            CurrencyFrom = convertFrom,
                            CurrencyTo = convertTo,
                            CurrencyAmount = currencyAmount,
                            ExchangeRate = currencyRate,
                            DateChecked = DateTime.Now,
                            ConvertedAmount = convertedAmount

                        };

                        await AzureManager.AzureManagerInstance.AddCurrencyConversionToHistory(conversionHistory);

                    }










                    else
                    {

                        ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                        var reply = HandleSystemMessage(activity);
                        if (reply != null)
                            await connector.Conversations.ReplyToActivityAsync(reply);
                    }






                }
               

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

                replyMessage += "I am ContasoBot and I am here to help you  ";


                replyMessage += "Currently I have following features  ";

                replyMessage += "* Converting a selection of Currencies ";
                replyMessage += "* For Example type Convert 100 NZD to AUD ";

                replyMessage += "* More features are being added daily ";

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