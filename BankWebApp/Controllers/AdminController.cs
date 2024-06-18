using BankDetailStructure;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankWebApp.Controllers
{
    /**
     * AdminController is a ASP.NET Web API controller class.
     * This class contains a rest service http method to save data to disk and process all the transactions.
     */
    public class AdminController : ApiController
    {
        /**
         * ProcessAllTransactions rest service process all the transaction of the data.
         * ProcessAllTransactions returns void.
         * Any error is handled by catching exception that will throw a http response exception.
         */
        [Route("api/Admin/ProcessAllTransactions")]
        [HttpPost]
        public void ProcessAllTransactions()
        {
            try
            {
                BankModel.bankDB.ProcessAllTransactions();
            }
            //catch exception if ProcessAllTransactions method fails and throw a http response exception
            catch (Exception)
            {
                //create an error response
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent("[ERROR] ProcessAllTransactions() - unable to process all transaction. cannot retrieve its access method.");
                //throw a http response exception
                throw new HttpResponseException(httpResponseMessage);
            }
        }

        /**
         * SaveToDisk rest service save the bank data of created users and accounts to disk.
         * SaveToDisk returns void.
         * Any error is handled by catching exception that will throw a http response exception.
         */
        [Route("api/Admin/SaveToDisk")]
        [HttpPost]
        public void SaveToDisk()
        {
            try
            {
                BankModel.bankDB.SaveToDisk();
            }
            //catch exception if SaveToDisk method fails and throw a http response exception
            catch (Exception)
            {
                //create an error response
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent("[ERROR] SaveToDisk() - unable to save data to disk. cannot retrieve its access method.");
                //throw a http response exception
                throw new HttpResponseException(httpResponseMessage);
            }
        }
    }
}