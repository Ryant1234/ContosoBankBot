using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContosoBankBot.Models;
using Newtonsoft.Json;

namespace ContosoBankBot.Classes
{
    public class CurrencyConversion
    {


        public string GetExchangeRate(/*string currencyFrom,*/ string currencyTo, int currencyAmount)
        {


            //if (currencyFrom == CurrencyTo)
            //{
            //    return "Sorry, but you cannot convert the same currency to itself";
            //}
            string message;
            
                if (currencyAmount != 0)
                {

                FixerCurrencyObject.RootObject rootObject;


                    message = "";

                }
                else
                {
          return "Please enter an amount to be converted";
                }

            return message;
        }


            
        }



        public static string CreateRequest(string currencyFrom)
        {
            string UrlRequest = "http://api.fixer.io/latest?base=" + 
                                currencyFrom;
                               
            return (UrlRequest);
        }



    }
}