namespace BankDetailStructure
{
    /**
     * AccountModel represent the templates for the .NET JSON serializers.
     * It is going to be the go-betweens the web data.
     * AccountModel defines the Accounts.
     * It contains an ID, an associated UserID, and an account balance which can never be negative.
     */
    public class AccountModel
    {
        //public fields
        public uint id;
        public uint idOfUser;
        public uint accBal;

        //alternate constructor
        public AccountModel(uint id, uint idOfUser, uint accBal)
        {
            this.id = id;
            this.idOfUser = idOfUser;
            this.accBal = accBal;
        }
    }
}
