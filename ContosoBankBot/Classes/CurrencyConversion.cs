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


        public double GetExchangeRate(string currencyFrom, string currencyTo, double currencyAmount, double currencyRate)
        {

       
       
            
             
                 double convertedAmount = currencyAmount*currencyRate;

                  

              
            return convertedAmount;
        }


            
        }



        //public static string CreateRequest(string currencyFrom)
        //{
        //    string UrlRequest = "http://api.fixer.io/latest?base=" + 
        //                        currencyFrom;
                               
        //    return (UrlRequest);
        //}



    }
