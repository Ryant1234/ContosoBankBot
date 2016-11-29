using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ContosoBankBot.DataModels;
using Microsoft.WindowsAzure.MobileServices;

namespace ContosoBankBot
{
    public class AzureManager
    {

        // Interacts with back end data / back end
        private static AzureManager instance;
        private MobileServiceClient client;
        private IMobileServiceTable<Country> countryTable;
        private IMobileServiceTable<ConversionHistory> conversionHistoryTable;

        private AzureManager()
        {
            this.client = new MobileServiceClient("http://rtcontosobackend.azurewebsites.net");
            this.countryTable = this.client.GetTable<Country>();
            this.conversionHistoryTable = this.client.GetTable<ConversionHistory>();
        }

        public MobileServiceClient AzureClient
        {
            get { return client; }
        }

        public static AzureManager AzureManagerInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AzureManager();
                }
                return instance;
            }
           
        }





        /// <summary>
        /// Adds the currency conversion to history
        /// </summary>
        /// <param name="conversionhistory"></param>
        /// <returns></returns>
        public async Task AddCurrencyConversionToHistory(ConversionHistory conversionhistory)
        {
            await this.conversionHistoryTable.InsertAsync(conversionhistory);
        }


        /// <summary>
        /// Gets all the data back from the conversion hitory table
        /// </summary>
        /// <returns></returns>
        public async Task<List<ConversionHistory>> GetConversionHistory()
        {
            return await this.conversionHistoryTable.ToListAsync();
        }


        

    }
}