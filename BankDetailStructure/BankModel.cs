namespace BankDetailStructure
{
    /**
     * BankModel contains an instance of BankDB.
     * It has a Static Object in C# of BankDB to prevent IO boatload problems.
     * It is used in Bank Web app.
     */
    public class BankModel
    {
        //a public static object of BankDB to prevent IO boatload problems.
        public static BankDB.BankDB bankDB = new BankDB.BankDB();
    }
}
