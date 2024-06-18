using BankDetailStructure;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankWebApp.Controllers
{
    /**
     * TransactionController is a ASP.NET Web API controller class.
     * This class contains a rest service http method to create transaction and get the transaction details by the transaction id
     */
    public class TransactionController : ApiController
    {
        //private static fields of BankDB Transaction Interface
        static BankDB.TransactionAccessInterface t;

        //transactionAccess is used to give access to its transaction interfaces
        static BankDB.TransactionAccessInterface transactionAccess
        {
            get
            {
                //if transaction interfaces is null, create the new access to its transaction interfaces
                if (t == null)
                {
                    //create the new access to its transaction interfaces
                    t = BankModel.bankDB.GetTransactionInterface();
                }
                //return the transaction interfaces
                return t;
            }
        }

        /**
         * GetTransactionDetailsById rest service retrieve the transaction details by the id 
         * It select a transaction and retreive the TransactionModel fields.
         * The error is handled by catching exception that will throw a http response exception.
         * GetTransactionDetailsById returns the Transaction details information.
         */
        [Route("api/Transaction/GetTransactionDetailsById/{id}")]
        [HttpGet]
        public TransactionModel GetTransactionDetailsById(uint id)
        {
            //creates a new TransactionModel object
            TransactionModel transactionModel = null;

            try
            {
                //select a transaction by the id
                transactionAccess.SelectTransaction(id);
                //set all the transaction model fields
                transactionModel = new TransactionModel(id, transactionAccess.GetSendrAcct(), transactionAccess.GetRecvrAcct(), transactionAccess.GetAmount());
            }
            //catch exception if transation fails or no transation found and throw a http response exception
            catch (Exception)
            {
                //create an error response
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent("[ERROR] GetTransactionDetailsById() - unable to get transaction details by id. cannot retrieve its access method.");
                //throw a http response exception
                throw new HttpResponseException(httpResponseMessage);
            }

            //return the transaction model object
            return transactionModel;
        }

        /**
         * CreateATransaction rest service creates a transaction by sender id giving an amount of money to the receiver id.
         * It select an account and check if the amount of money to be transact is enough in the balance.
         * If balance is enough, then it will subtract the sender amount of money and add that money for the receiver.
         * It will then select transaction by id and set the sender, receiver and amount of transaction.
         * The error is handled by catching exception that will throw a http response exception.
         * CreateATransaction returns the Account details information.
         */
        [Route("api/Transaction/CreateATransaction/{idOfSender}/{idOfReceiver}/{amount}")]
        [HttpPost]
        public uint CreateATransaction(uint idOfSender, uint idOfReceiver, uint amount)
        {
            uint id = 0;
            try
            {
                //get the account access interface and select an account by id of sender
                AccountController.acctAccess.SelectAccount(idOfSender);
                //get the account balance
                uint bal = AccountController.acctAccess.GetBalance();

                //if balance is more than or equal to amount, do transaction
                if (bal >= amount)
                {
                    //create a transaction id
                    id = transactionAccess.CreateTransaction();

                    //subtract total money of sender by the amount inputted
                    AccountController.acctAccess.Withdraw(amount);
                    //select an account by id of receiver
                    AccountController.acctAccess.SelectAccount(idOfReceiver);
                    //add total money of receiver by the amount inputted
                    AccountController.acctAccess.Deposit(amount);

                    //select transaction by id
                    transactionAccess.SelectTransaction(id);
                    //set the transaction fields
                    transactionAccess.SetSendr(idOfSender);
                    transactionAccess.SetRecvr(idOfReceiver);
                    transactionAccess.SetAmount(amount);
                }
            }
            //catch exception if transation fails and throw a http response exception
            catch (Exception)
            {
                //create an error response
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent("[ERROR] CreateATransaction() - unable to create a transaction. cannot retrieve its access method.");
                //throw a http response exception
                throw new HttpResponseException(httpResponseMessage);
            }

            //return the id
            return id;
        }
    }
}