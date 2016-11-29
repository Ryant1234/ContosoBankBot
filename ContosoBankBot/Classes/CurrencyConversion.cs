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


        public string GetExchangeRate(string currencyFrom, string currencyTo, double currencyAmount, double currencyRate)
        {

       
            string message;
            
                if (currencyAmount != 0 )
                {

                 double convertedAmount = currencyAmount*currencyRate;

                    message = ($"{currencyAmount} {currencyFrom} is equal to {convertedAmount} " + $"{currencyTo}");

                }
                else
                {
          return "Please enter an amount to be converted";
                }

            return message;
        }


            
        }



        //public static string CreateRequest(string currencyFrom)
        //{
        //    string UrlRequest = "http://api.fixer.io/latest?base=" + 
        //                        currencyFrom;
                               
        //    return (UrlRequest);
        //}



    }
