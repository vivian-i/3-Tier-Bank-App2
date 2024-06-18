using BankDetailStructure;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Web.Http;
using BankBusinessTierWebApp.Models;

namespace BankBusinessTierWebApp.Controllers
{
    /**
     * ApiController is a ASP.NET Web API controller class.
     * ApiController is the Business Tier which contains a rest service http method that exposes an API for bank clients.
     * It is created to simplify the Data tier API to something easily usable as users should not be able to access some functionality.
     * ApiController logs message informations, details and errors to a file by using LogHelper class.
     */
    public class ApiController : System.Web.Http.ApiController
    {
        //private fields of LogHelper object to help log message to file
        LogHelper logHelper = new LogHelper();
        //private fields
        TheModel theModel;

        /**
         * CreateAUser rest service request a rest service and retrieve its returned rest request data if the input is of the correct type.
         * CreateAUser returns the Account details from the returned rest request data if the input is of the correct type.
         * If the input is of the correct type, it saves the data to disk and this http method will return the Account details.
         */
        [Route("Home/Users/api/Api/CreateAUser/{fName}/{lName}")]
        [HttpPost]
        public AccountModel CreateAUser(string fName, string lName)
        {
            //creates an AccountModel object
            AccountModel acctModel = null;
            //declare bool to true, this bool is used to check if first and last is all letters
            bool isfNameAllLetters = true;
            bool islNameAllLetters = true;

            //check if the inputted first name is of the correct type. Only letters in first name check
            foreach (char c in fName)
            {
                //if not all of the inputted first name is a letter, set bool to false
                if (!char.IsLetter(c))
                {
                    isfNameAllLetters = false;
                }
            }
            //check if the inputted last name is of the correct type. Only letters in last name check
            foreach (char c in lName)
            {
                //if not all of the inputted last name is a letter, set bool to false
                if (!char.IsLetter(c))
                {
                    islNameAllLetters = false;
                }
            }

            //if the inputted values is of the correct type, do create a user
            if (isfNameAllLetters == true && islNameAllLetters == true)
            {
                //log message to file
                logHelper.log("[INFO] CreateAUser() - all user inputs is valid, retrieving the rest request data.");

                //initialiaze the model
                theModel = new TheModel();
                //create a user
                acctModel = theModel.CreateAUser(fName, lName);

                //if acctModel is null, log error message to file
                if (acctModel == null)
                {
                    //log message to file
                    logHelper.log("[ERROR] CreateAUser() - no data found.");
                }
                //if acctModel is not null, set up and call the API method to save data to disk, and log success message to user
                else
                {
                    //set up and call the API method to save data to disk
                    theModel.SaveToDisk();

                    //log message to file
                    logHelper.log($"[INFO] CreateAUser() - success. Data with user id '{acctModel.idOfUser}', first name '{fName}' and last name '{lName}' is saved to disk.");
                }
            }
            //if the inputted values is not the correct type, log error message to file
            else
            {
                //log message to file
                logHelper.log("[ERROR] CreateAUser() - user input is invalid.");
            }

            //return the account details
            return acctModel;
        }

        /**
         * GetAUserNameById rest service request a rest service and retrieve its returned rest request data if the input is of the correct type.
         * The error is handled by catching a null reference exception. 
         * GetAUserNameById returns the User details information from the returned rest request data if succesful.
         * If the returned rest request data is null, null reference exception is catched and this http method will return null.
         */
        [Route("Home/Users/api/Api/GetAUserNameById/{id}")]
        [HttpGet]
        public UserModel GetAUserNameById(uint id)
        {
            try
            {
                //creates a UserModel object
                UserModel userModel = null;
                //check if the inputted values is of the correct type
                bool isNumber = int.TryParse(id.ToString(), out int numericValue);
                //if the inputted values is of the correct type, do get the user first and last name
                if (isNumber == true)
                {
                    //log message to file
                    logHelper.log("[INFO] GetAUserNameById() - all user inputs is valid, retrieving the rest request data.");

                    //initialiaze the model
                    theModel = new TheModel();
                    //get the user name by the id
                    userModel = theModel.GetAUserNameById(id);

                    //if userModel is null, log error message to file
                    if (userModel == null)
                    {
                        //log message to file
                        logHelper.log("[ERROR] GetAUserNameById() - no data found.");
                    }
                    //if userModel is not null, log success message to file
                    else
                    {
                        //log message to file
                        logHelper.log($"[INFO] GetAUserNameById() - success. User ID '{userModel.id}' with first name '{userModel.fName}' and last name '{userModel.lName}' is retrieved.");
                    }
                }
                //if the inputted values is not the correct type, log error message to file
                else
                {
                    //log message to file
                    logHelper.log("[ERROR] GetAUserNameById() - user input is invalid.");
                }

                //return the user details
                return userModel;
            }
            //catch exception if no user found. Log error message to file and return null
            catch (NullReferenceException)
            {
                //log error message to file
                logHelper.log("[ERROR] GetAUserNameById() - null reference exception as no data is found");
                return null;
            }
        }

        /**
        * GetAUserNameById rest service request a rest service and retrieve its returned rest request data if the input is of the correct type.
        * The error is handled by catching a null reference exception. 
        * GetAUserNameById returns the User details information from the returned rest request data if succesful.
        * If the returned rest request data is null, null reference exception is catched and this http method will return null.
        */
        [Route("Home/Users/api/Api/ChangeUserNameById/{id}/{fName}/{lName}")]
        [HttpPost]
        public UserModel ChangeUserNameById(uint id, string fName, string lName)
        {
            try
            {
                //creates a UserModel object
                UserModel userModel = null;
                //check if the inputted values is of the correct type
                bool isNumber = int.TryParse(id.ToString(), out int numericValue);
                //declare bool to true, this bool is used to check if first and last is all letters
                bool isfNameAllLetters = true;
                bool islNameAllLetters = true;

                //check if the inputted first name is of the correct type. Only letters in first name check
                foreach (char c in fName)
                {
                    //if not all of the inputted first name is a letter, set bool to false
                    if (!char.IsLetter(c))
                    {
                        isfNameAllLetters = false;
                    }
                }
                //check if the inputted last name is of the correct type. Only letters in last name check
                foreach (char c in lName)
                {
                    //if not all of the inputted last name is a letter, set bool to false
                    if (!char.IsLetter(c))
                    {
                        islNameAllLetters = false;
                    }
                }

                //if the inputted values is of the correct type, do get the user first and last name
                if (isNumber == true && isfNameAllLetters == true && islNameAllLetters == true)
                {
                    //log message to file
                    logHelper.log("[INFO] ChangeUserNameById() - all user inputs is valid, retrieving the rest request data.");

                    //initialiaze the model
                    theModel = new TheModel();
                    //get the user name by the id
                    userModel = theModel.ChangeUserNameById(id, fName, lName);

                    //if acctModel is null, log error message to file
                    if (userModel == null)
                    {
                        //log message to file
                        logHelper.log("[ERROR] ChangeUserNameById() - no data found.");
                    }
                    //if acctModel is not null, set up and call the API method to save data to disk, and log success message to user
                    else
                    {
                        //set up and call the API method to save data to disk
                        theModel.SaveToDisk();
                        //log message to file
                        logHelper.log($"[INFO] ChangeUserNameById() - success. User ID '{id}' has change their first name to '{fName}' and last name to '{lName}'. New data is saved to disk.");
                    }
                }
                //if the inputted values is not the correct type, log error message to file
                else
                {
                    //log message to file
                    logHelper.log("[ERROR] ChangeUserNameById() - user input is invalid.");
                }

                //return the user details
                return userModel;
            }
            //catch exception if no user found. Log error message to file and return null
            catch (NullReferenceException)
            {
                //log error message to file
                logHelper.log("[ERROR] ChangeUserNameById() - null reference exception as no data is found");
                return null;
            }
        }

        /**
         * Deposit rest service request a rest service and retrieve its returned rest request data if the input is of the correct type.
         * Deposit returns a boolean true from the returned rest request data if succesful.
         * If the returned rest request data is succesful or true, it saves the data to disk and this http method will return true.
         * If the returned rest request data is failed or false, it does not save data to disk and this http method will return false.
         */
        [Route("Home/Accounts/api/Api/Deposit/{id}/{amount}")]
        [HttpPost]
        public bool Deposit(uint id, uint amount)
        {
            //creates a bool
            bool res = false;
            //check if the inputted values is of the correct type
            bool isNumberAcctId = int.TryParse(id.ToString(), out int numericValue);
            bool isNumberAmount = int.TryParse(amount.ToString(), out int numericValue2);
            //if the inputted values is of the correct type, do deposit the money to the account id
            if (isNumberAcctId == true && isNumberAmount == true)
            {
                //log message to file
                logHelper.log("[INFO] Deposit() - all user inputs is valid, retrieving the rest request data.");

                //initialiaze the model
                theModel = new TheModel();
                //use RestClient and set the URL
                RestClient client = new RestClient("https://localhost:44369/");
                //set up and call the API method
                RestRequest request = new RestRequest("api/Account/Deposit/" + id.ToString() + "/" + amount.ToString());
                //use IRestResponse and set the request in the client post method
                IRestResponse resp = client.Post(request);
                //check if response is succesful
                if (resp.IsSuccessful)
                {
                    //use the JSON Deserializer to deseralize the object back
                    res = JsonConvert.DeserializeObject<bool>(resp.Content);
                }

                //if res is true or succesful, save data to disk
                if (res == true)
                {
                    //set up and call the API method to save data to disk
                    theModel.SaveToDisk();

                    //log message to file
                    logHelper.log($"[INFO] Deposit() - success. An amount of ${amount} has been depositted to ID '{id}'. Data is saved to disk.");
                }
                //if res is false, log error message to file
                else
                {
                    //log message to file
                    logHelper.log("[ERROR] Deposit() - deposit fails.");
                }
            }
            //if the inputted values is not the correct type, log error message to file
            else
            {
                //log message to file
                logHelper.log("[ERROR] Deposit() - user input is invalid.");
            }

            //return the res
            return res;
        }

        /**
         * Withdraw rest service request a rest service and retrieve its returned rest request data if the input is of the correct type.
         * Withdraw returns a boolean true from the returned rest request data if succesful.
         * If the returned rest request data is succesful or true, it saves the data to disk and this http method will return true.
         * If the returned rest request data is failed or false, it does not save data to disk and this http method will return false.
         */
        [Route("Home/Accounts/api/Api/Withdraw/{id}/{amount}")]
        [HttpPost]
        public bool Withdraw(uint id, uint amount)
        {
            //creates a bool
            bool res = false;
            //check if the inputted values is of the correct type
            bool isNumberAcctId = int.TryParse(id.ToString(), out int numericValue);
            bool isNumberAmount = int.TryParse(amount.ToString(), out int numericValue2);
            //if the inputted values is of the correct type, do withdraw the money to the account id
            if (isNumberAcctId == true && isNumberAmount == true)
            {
                //log message to file
                logHelper.log("[INFO] Withdraw() - all user inputs is valid, retrieving the rest request data.");

                //initialiaze the model
                theModel = new TheModel();
                //withdraw an amount of money and return a bool
                res = theModel.Withdraw(id, amount);

                //if res is true or succesful, save data to disk
                if (res == true)
                {
                    //set up and call the API method to save data to disk
                    theModel.SaveToDisk();

                    //log message to file
                    logHelper.log($"[INFO] Withdraw() - success. An amount of ${amount} has been withdrawn to ID '{id}'. Data is saved to disk.");
                }
                //if res is null, log error message to file
                else
                {
                    //log message to file
                    logHelper.log("[ERROR] Withdraw() - withdraw fails.");
                }
            }
            //if the inputted values is not the correct type, log error message to file
            else
            {
                //log message to file
                logHelper.log("[ERROR] Withdraw() - user input is invalid.");
            }

            //return the res
            return res;
        }

        /**
         * GetAccountDetailsById rest service request a rest service and retrieve its returned rest request data if the input is of the correct type.
         * The error is handled by catching a null reference exception. 
         * GetAccountDetailsById returns the Account details information from the returned rest request data if succesful.
         * If the returned rest request data is null, null reference exception is catched and this http method will return null.
         */
        [Route("Home/Accounts/api/Api/GetAccountDetailsById/{id}")]
        [HttpGet]
        public AccountModel GetAccountDetailsById(uint id)
        {
            try
            {
                //creates an AccountModel object
                AccountModel accountModel = null;
                //check if the inputted values is of the correct type
                bool isNumber = int.TryParse(id.ToString(), out int numericValue);
                //if the inputted values is of the correct type, do get the account details 
                if (isNumber == true)
                {
                    //log message to file
                    logHelper.log("[INFO] GetAccountDetailsById() - all user inputs is valid, retrieving the rest request data.");

                    //initialiaze the model
                    theModel = new TheModel();
                    //get account details by the id
                    accountModel = theModel.GetAccountDetailsById(id);

                    //if accountModel is null, log error message to file
                    if (accountModel == null)
                    {
                        //log message to file
                        logHelper.log("[ERROR] GetAccountDetailsById() - no data found.");
                    }
                    //if accountModel is not null, log success message to file
                    else
                    {
                        //log message to file
                        logHelper.log($"[INFO] GetAccountDetailsById() - success. Account ID '{id}' details is retrieved. It has ${accountModel.accBal}.");
                    }
                }
                //if the inputted id is not the correct type, log error message to file
                else
                {
                    //log message to file
                    logHelper.log("[ERROR] GetAccountDetailsById() - user input is invalid.");
                }
                //return the accountmodel
                return accountModel;
            }
            //catch exception if no account found. Log error message to file and return null
            catch (NullReferenceException)
            {
                //log error message to file
                logHelper.log("[ERROR] GetAccountDetailsById() - null reference exception as no data is found");
                return null;
            }
        }

        /**
         * CreateATransaction rest service request a rest service and retrieve its returned rest request data if the input is of the correct type.
         * CreateATransaction returns an uint of the transaction id from the returned rest request data if succesful.
         * If the returned rest request data is succesful or not 0, it saves the data to disk and this http method will return the transaction id.
         * If the returned rest request data is failed or is 0, it does not save data to disk and this http method will return 0.
         */
        [Route("Home/Transactions/api/Api/CreateATransaction/{idOfSender}/{idOfReceiver}/{amount}")]
        [HttpPost]
        public uint CreateATransaction(uint idOfSender, uint idOfReceiver, uint amount)
        {
            //creates an uint
            uint transactionRes = 0;
            //check if the inputted values is of the correct type
            bool isNumberSenderId = int.TryParse(idOfSender.ToString(), out int numericValue);
            bool isNumberReceiverId = int.TryParse(idOfReceiver.ToString(), out int numericValue2);
            bool isNumberAmount = int.TryParse(amount.ToString(), out int numericValue3);
            //if the inputted values is of the correct type, do create a transaction
            if (isNumberSenderId == true && isNumberReceiverId == true && isNumberAmount == true)
            {
                //log message to file
                logHelper.log("[INFO] CreateATransaction() - all user inputs is valid, retrieving the rest request data.");

                //initialiaze the model
                theModel = new TheModel();
                //create a transaction
                transactionRes = theModel.CreateATransaction(idOfSender, idOfReceiver, amount);

                //if res is not 0 or succesful, save data to disk
                if (transactionRes != 0)
                {
                    //set up and call the API method to save data to disk
                    theModel.SaveToDisk();

                    //log message to file
                    logHelper.log($"[INFO] CreateATransaction() - success. A transaction from ID '{idOfSender}' to ID '{idOfReceiver}' with an amount of ${amount} is created and sent. Data is saved to disk.");
                }
                //if transactionRes is null, log error message to file
                else
                {
                    //log message to file
                    logHelper.log("[ERROR] CreateATransaction() - trasaction fails.");
                }

            }
            //if the inputted values is not the correct type, log error message to file
            else
            {
                //log message to file
                logHelper.log("[ERROR] CreateATransaction() - user input is invalid.");
            }

            //return the transaction id
            return transactionRes;
        }

        /**
         * GetTransactionDetailsById rest service request a rest service and retrieve its returned rest request data if the input is of the correct type.
         * The error is handled by catching a null reference exception. 
         * GetTransactionDetailsById returns the Transaction details information from the returned rest request data if succesful.
         * If the returned rest request data is null, null reference exception is catched and this http method will return null.
         */
        [Route("Home/Transactions/api/Api/GetTransactionDetailsById/{id}")]
        [HttpGet]
        public TransactionModel GetTransactionDetailsById(uint id)
        {
            try
            {
                //creates a TransactionModel object
                TransactionModel transactionModel = null;
                //check if the inputted values is of the correct type
                bool isNumber = int.TryParse(id.ToString(), out int numericValue);
                //if the inputted values is of the correct type, do get the transaction details
                if (isNumber == true)
                {
                    //log message to file
                    logHelper.log("[INFO] GetTransactionDetailsById() - all user inputs is valid, retrieving the rest request data.");

                    //initialiaze the model
                    theModel = new TheModel();
                    //create a transaction
                    transactionModel = theModel.GetTransactionDetailsById(id);

                    //if transactionModel is null, log error message to file
                    if (transactionModel == null)
                    {
                        //log message to file
                        logHelper.log("[ERROR] GetTransactionDetailsById() - no data found.");
                    }
                    //if transactionModel is not null, log success message to file
                    else
                    {
                        //log message to file
                        logHelper.log($"[INFO] GetTransactionDetailsById() - success. Transaction details of ID '{id}' is retrieved. It has details of a transaction from ID '{transactionModel.idOfSender}' to ID '{transactionModel.idOfReceiver}' with an amount of ${transactionModel.amount}.");
                    }
                }
                //if the inputted values is not the correct type, log error message to file
                else
                {
                    //log message to file
                    logHelper.log("[ERROR] GetTransactionDetailsById() - user input is invalid.");
                }

                //return the transaction model
                return transactionModel;
            }
            //catch exception if no transaction found. Log error message to file and return null
            catch (NullReferenceException)
            {
                //log error message to file
                logHelper.log("[ERROR] GetTransactionDetailsById() - null reference exception as no data is found");
                return null;
            }
        }
    }
}