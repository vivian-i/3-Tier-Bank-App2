using System.Web.Mvc;

namespace BankBusinessTierWebApp.Controllers
{
    /**
     * HomeController is a ASP.NET Web API controller class.
     * This class is used to connect its view in the .cshtml file that has been customly created.
     * It contains 4 methods that returns a View. The methods are Index for the home main page, Users, Accounts and Transactions
     */
    public class HomeController : Controller
    {
        /**
         * Index method returns an ActionResult which is View()
         * Index is the home main page
         * The view is in folder View>Home>Index.cshtml
         */
        public ActionResult Index()
        {
            return View();
        }

        /**
         * Users method returns an ActionResult which is View()
         * Users is the home main page
         * The view is in folder View>Home>Users.cshtml
         */
        public ActionResult Users()
        {
            return View();
        }

        /**
         * Accounts method returns an ActionResult which is View()
         * Accounts is the home main page
         * The view is in folder View>Home>Accounts.cshtml
         */
        public ActionResult Accounts()
        {
            return View();
        }

        /**
         * Transactions method returns an ActionResult which is View()
         * Transactions is the home main page
         * The view is in folder View>Home>Transactions.cshtml
         */
        public ActionResult Transactions()
        {
            return View();
        }
    }
}