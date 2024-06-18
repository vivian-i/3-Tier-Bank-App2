using BankDetailStructure;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankWebApp.Controllers
{
    /**
     * UserController is a ASP.NET Web API controller class.
     * This class contains a rest service http method to create user and get the user first and last name by the user id
     */
    public class UserController : ApiController
    {
        //private fields of BankDB User Interface
        static BankDB.UserAccessInterface u;

        //userAccess is used to give access to its user interfaces
        static BankDB.UserAccessInterface userAccess
        {
            get
            {
                //if user interfaces is null, create the new access to its user interfaces
                if (u == null)
                {
                    //create the new access to its user interfaces
                    u = BankModel.bankDB.GetUserAccess();
                }
                //return the user interfaces
                return u;
            }
        }

        /**
         * GetAUserNameById rest service retrieve the User details by the inputted user id.
         * It select an account, get its first and last name and then retreive the UserModel fields.
         * The error is handled by catching exception that will throw a http response exception.
         * GetAUserNameById returns the User details information.
         */
        [Route("api/User/GetAUserNameById/{id}")]
        [HttpGet]
        public UserModel GetAUserNameById(uint id)
        {
            //create a UserModel object
            UserModel userModel = null;

            try
            {
                //select a user by the user id
                userAccess.SelectUser(id);
                //get the user first and last name
                userAccess.GetUserName(out string fName, out string lName);
                //set all user details
                userModel = new UserModel(id, fName, lName);
            }
            //catch exception if no user if found and throw a http response exception
            catch (Exception)
            {
                //create an error response
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent("[ERROR] GetAUserNameById() - unable to get user name by id. cannot retrieve its access method.");
                //throw a http response exception
                throw new HttpResponseException(httpResponseMessage);
            }

            //return the user model
            return userModel;
        }

        /**
         * ChangeUserNameById rest service selects a user id and change its name to the new inputted first and last name.
         * It select an account, set its first and last name and then retreive the UserModel fields.
         * The error is handled by catching exception that will throw a http response exception
         * ChangeUserNameById returns the new User details information.
         */
        [Route("api/User/ChangeUserNameById/{id}/{fName}/{lName}")]
        [HttpPost]
        public UserModel ChangeUserNameById(uint id, string fName, string lName)
        {
            //create a UserModel object
            UserModel userModel = null;

            try
            {
                //select a user by the user id
                userAccess.SelectUser(id);
                //set the user first and last name
                userAccess.SetUserName(fName, lName);
                //set all user details
                userModel = new UserModel(id, fName, lName);
            }
            //catch exception if no user if found and throw a http response exception
            catch (Exception)
            {
                //create an error response
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent("[ERROR] ChangeUserNameById() - unable to change user name by id. cannot retrieve its access method.");
                //throw a http response exception
                throw new HttpResponseException(httpResponseMessage);
            }

            //return the user model
            return userModel;
        }

        /**
         * CreateAUser rest service retrieve creates a user and account by the inputted first and last name
         * It creates a user id and set the inputted first and last name and creates the account by the newly created user id.
         * It selects the account by the id and retrives the account details information.
         * CreateAUser returns the Account details information.
         */
        [Route("api/User/CreateAUser/{fName}/{lName}")]
        [HttpPost]
        public AccountModel CreateAUser(string fName, string lName)
        {
            //create a new AccountModel object
            AccountModel accountModel = null;

            try
            {
                //create a user id
                uint userId = userAccess.CreateUser();
                //select user by id
                userAccess.SelectUser(userId);
                //set the user first and last name
                userAccess.SetUserName(fName, lName);

                //get the access to account interfaces and create an account id with the associated user id
                uint acctId = AccountController.acctAccess.CreateAccount(userId);
                //select an account by the id
                AccountController.acctAccess.SelectAccount(acctId);
                //set all the account fields
                accountModel = new AccountModel(acctId, userId, 0);
            }
            //catch exception if it fails creating a user and throw a http response exception.
            catch (Exception)
            {
                //create an error response
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new StringContent("[ERROR] CreateAUser() - unable to create user. cannot retrieve its access method.");
                //throw a http response exception
                throw new HttpResponseException(httpResponseMessage);
            }

            //return the account model
            return accountModel;
        }
    }
}