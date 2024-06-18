using BankDetailStructure;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankBusinessTierWebApp.Models
{
    /**
     * TheModel is a model class of the api web application.
     * It contains method of CreateAUser, SaveToDisk, GetAUserNameById, Deposit, Withdraw, GetAccountDetailsById, CreateATransaction and GetTransactionDetailsById.
     * It is a class that helps call or connect with the bank web application.
     */
    public class TheModel
    {
        //private fields
        RestClient client;
        string URL;
        //private fields of LogHelper object to help log message to file
        LogHelper logHelper = new LogHelper();

        //default constructor
        public TheModel()
        {
            //set the base URl
            URL = "https://localhost:44369/";
            //use RestClient and set the URL
            client = new RestClient(URL);
        }

        /**
         * CreateAUser method request the server CreateAUser rest service.
         * It creates a user. 
         */
        public AccountModel CreateAUser(string fName, string lName)
        {
            //set up and call the API method
            RestRequest request = new RestRequest("api/User/CreateAUser/" + fName + "/" + lName);
            //use IRestResponse and set the request in the client post method
            IRestResponse resp = client.Post(request);

            //initialize the account
            AccountModel acctModel = null;
            //check if response is succesful
            if (resp.IsSuccessful)
            {
                //use the JSON Deserializer to deseralize the object back
                acctModel = JsonConvert.DeserializeObject<AccountModel>(resp.Content);
            }
            //if response is not succesful, log the error message to file
            else
            {
                //log error message to file
                logHelper.log(resp.Content);
            }

            //return the account details
            return acctModel;
        }

        /**
         * SaveToDisk method request the server SaveToDisk rest service.
         * It saves data to disk.
         */
        public void SaveToDisk()
        {
            //set up and call the API method to save data to disk
            RestRequest requestSaveToDisk = new RestRequest("api/Admin/SaveToDisk");
            //use IRestResponse and set the request in the client post method
            IRestResponse resp = client.Post(requestSaveToDisk);

            //check if response is succesful
            if (resp.IsSuccessful)
            {
                //log error message to file
                logHelper.log("[INFO] SaveToDisk() - succesfully called SaveToDisk() method of Data-tier.");
            }
            //if response is not succesful, log the error message to file
            else
            {
                //log error message to file
                logHelper.log(resp.Content);
            }
        }

        /**
         * GetAUserNameById method request the server GetAUserNameById rest service.
         * It retrieves the user name by the inputted id.
         */
        public UserModel GetAUserNameById(uint id)
        {
            //set up and call the API method
            RestRequest request = new RestRequest("api/User/GetAUserNameById/" + id.ToString());
            //use IRestResponse and set the request in the client get method
            IRestResponse resp = client.Get(request);

            //initialize the user
            UserModel userModel = null;
            //check if response is succesful
            if (resp.IsSuccessful)
            {
                //use the JSON Deserializer to deseralize the object back
                userModel = JsonConvert.DeserializeObject<UserModel>(resp.Content);
            }
            //if response is not succesful, log the error message to file
            else
            {
                //log error message to file
                logHelper.log(resp.Content);
            }

            //return the user details
            return userModel;
        }

        /**
         * ChangeUserNameById method request the server ChangeUserNameById rest service.
         * It retrieves the new user details by the inputted id and names.
         */
        public UserModel ChangeUserNameById(uint id, string fName, string lName)
        {
            //set up and call the API method
            RestRequest request = new RestRequest("api/User/ChangeUserNameById/" + id.ToString() + "/" + fName + "/" + lName);
            //use IRestResponse and set the request in the client post method
            IRestResponse resp = client.Post(request);

            //initialize the user
            UserModel userModel = null;
            //check if response is succesful
            if (resp.IsSuccessful)
            {
                //use the JSON Deserializer to deseralize the object back
                userModel = JsonConvert.DeserializeObject<UserModel>(resp.Content);
            }
            //if response is not succesful, log the error message to file
            else
            {
                //log error message to file
                logHelper.log(resp.Content);
            }

            //return the new user details
            return userModel;
        }

        /**
         * Deposit method request the server Deposit rest service.
         * It deposits an amount of money to the id inputted.
         */
        public bool Deposit(uint id, uint amount)
        {
            //set up and call the API method
            RestRequest request = new RestRequest("api/Account/Deposit/" + id.ToString() + "/" + amount.ToString());
            //use IRestResponse and set the request in the client post method
            IRestResponse resp = client.Post(request);

            //initialize the bool
            bool res = false;
            //check if response is succesful
            if (resp.IsSuccessful)
            {
                //use the JSON Deserializer to deseralize the object back
                res = JsonConvert.DeserializeObject<bool>(resp.Content);
            }
            //if response is not succesful, log the error message to file
            else
            {
                //log error message to file
                logHelper.log(resp.Content);
            }

            //return the bool
            return res;
        }

        /**
         * Withdraw method request the server Withdraw rest service.
         * It withdraw an amount of money to the id inputted.
         */
        public bool Withdraw(uint id, uint amount)
        {
            //set up and call the API method
            RestRequest request = new RestRequest("api/Account/Withdraw/" + id.ToString() + "/" + amount.ToString());
            //use IRestResponse and set the request in the client post method
            IRestResponse resp = client.Post(request);

            //initialize the bool
            bool res = false;
            //check if response is succesful
            if (resp.IsSuccessful)
            {
                //use the JSON Deserializer to deseralize the object back
                res = JsonConvert.DeserializeObject<bool>(resp.Content);
            }
            //if response is not succesful, log the error message to file
            else
            {
                //log error message to file
                logHelper.log(resp.Content);
            }

            //return the bool
            return res;
        }

        /**
         * GetAccountDetailsById method request the server GetAccountDetailsById rest service.
         * It retrieves the account details by the inputted id.
         */
        public AccountModel GetAccountDetailsById(uint id)
        {
            RestRequest request = new RestRequest("api/Account/GetAccountDetailsById/" + id.ToString());
            //use IRestResponse and set the request in the client get method
            IRestResponse resp = client.Get(request);

            //initialize the account
            AccountModel acctModel = null;
            //check if response is succesful
            if (resp.IsSuccessful)
            {
                //use the JSON Deserializer to deseralize the object back
                acctModel = JsonConvert.DeserializeObject<AccountModel>(resp.Content);
            }
            //if response is not succesful, log the error message to file
            else
            {
                //log error message to file
                logHelper.log(resp.Content);
            }

            //return the account
            return acctModel;
        }

        /**
         * CreateATransaction method request the server CreateATransaction rest service.
         * It creates a transaction by the inputted values.
         */
        public uint CreateATransaction(uint idOfSender, uint idOfReceiver, uint amount)
        {
            //set up and call the API method
            RestRequest request = new RestRequest("api/Transaction/CreateATransaction/" + idOfSender.ToString() + "/" + idOfReceiver.ToString() + "/" + amount.ToString());
            //use IRestResponse and set the request in the client post method
            IRestResponse resp = client.Post(request);

            //initialize the transaction result
            uint transactionRes = 0;
            //check if response is succesful
            if (resp.IsSuccessful)
            {
                //use the JSON Deserializer to deseralize the object back
                transactionRes = JsonConvert.DeserializeObject<uint>(resp.Content);
            }
            //if response is not succesful, log the error message to file
            else
            {
                //log error message to file
                logHelper.log(resp.Content);
            }

            //return the transaction id
            return transactionRes;
        }

        /**
         * GetTransactionDetailsById method request the server GetTransactionDetailsById rest service.
         * It retrieves the transaction details by the inputted id
         */
        public TransactionModel GetTransactionDetailsById(uint id)
        {
            //set up and call the API method
            RestRequest request = new RestRequest("api/Transaction/GetTransactionDetailsById/" + id);
            //use IRestResponse and set the request in the client get method
            IRestResponse resp = client.Get(request);

            //initialize the transaction
            TransactionModel transactionModel = null;
            //check if response is succesful
            if (resp.IsSuccessful)
            {
                //use the JSON Deserializer to deseralize the object back
                transactionModel = JsonConvert.DeserializeObject<TransactionModel>(resp.Content);
            }
            //if response is not succesful, log the error message to file
            else
            {
                //log error message to file
                logHelper.log(resp.Content);
            }

            //return the transaction details
            return transactionModel;
        }
    }
}