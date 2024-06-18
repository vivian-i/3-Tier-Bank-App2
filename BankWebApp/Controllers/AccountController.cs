using BankDetailStructure;
using System;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace BankWebApp.Controllers
{
    /**
     * AccountController is a ASP.NET Web API controller class.
     * This class contains a rest service http method to get the account details by id, deposit and withdraw.
     */
    public class AccountController : ApiController
    {
        //private fields of BankDB Account Interface
        static BankDB.AccountAccessInterface a;

        //acctAccess is used to give access to its account interfaces
        public static BankDB.AccountAccessInterface acctAccess
        {
            get
            {
                //if account interfaces is null, create the new access to its account interfaces
                if (a == null)
                {
                    //create the new access to its account interfaces
                    a = BankModel.bankDB.GetAccountInterface();
                }
                //return the account interfaces
                return a;
            }
        }

        /**
         * Deposit rest service deposits an amount of money for the account id.
         * It select an account and deposit the amount of money inputted and return true if deposit is succesful.
         * The error is handled by catching exception that will throw a http response exception.
         * Deposit returns a boolean.
         */
        [Route("api/Account/Deposit/{id}/{amount}")]
        [HttpPost]
        public bool Deposit(uint id, uint amount)
        {
            try
            {
                //select account by the id
                acctAccess.SelectAccount(id);
                //deposit an amount to the selected account
                acctAccess.Deposit(amount);
            }
            //catch exception if deposit fails and throw a http response exception
            catch (Exception)
            {
                //create an error response
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent("[ERROR] Deposit() - deposit failed. cannot retrieve its access method.");
                //throw a http response exception
                throw new HttpResponseException(httpResponseMessage);
            }

            //return true for successful
            return true;
        }

        /**
         * Withdraw rest service withdraw an amount of money for the account id.
         * It select an account and withdraw the amount of money inputted.
         * The error is handled by catching exception that will throw a http response exception.
         * Withdraw returns a boolean.
         */
        [Route("api/Account/Withdraw/{id}/{amount}")]
        [HttpPost]
        public bool Withdraw(uint id, uint amount)
        {
            try
            {
                //select account by the id
                acctAccess.SelectAccount(id);
                //deposit an amount to the selected account
                acctAccess.Withdraw(amount);
            }
            //catch exception if withdraw fails and throw a http response exception
            catch (Exception)
            {
                //create an error response
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent("[ERROR] Withdraw() - withdraw failed. cannot retrieve its access method.");
                //throw a http response exception
                throw new HttpResponseException(httpResponseMessage);
            }

            //return true for successful
            return true;
        }

        /**
         * GetAccountDetailsById rest service retrieve the Account details by the inputted account id.
         * It select an account and retreive the AccountModel fields.
         * The error is handled by catching exception that will throw a http response exception.
         * GetAccountDetailsById returns the Account details information.
         */
        [Route("api/Account/GetAccountDetailsById/{id}")]
        [HttpGet]
        public AccountModel GetAccountDetailsById(uint id)
        {
            //create a new AccountModel object
            AccountModel accountModel = null;

            try
            {
                //select account by the id
                acctAccess.SelectAccount(id);
                //set all account details
                accountModel = new AccountModel(id, acctAccess.GetOwner(), acctAccess.GetBalance());
            }
            //catch exception if there is no account found and throw a http response exception
            catch (Exception)
            {
                //create an error response
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent("[ERROR] GetAccountDetailsById() - unable to get account details by id. cannot retrieve its access method.");
                //throw a http response exception
                throw new HttpResponseException(httpResponseMessage);
            } 

            //return the accountModel
            return accountModel;
        }
    }
}