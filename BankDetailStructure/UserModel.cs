namespace BankDetailStructure
{
    /**
     * UserModel represent the templates for the .NET JSON serializers.
     * It is going to be the go-betweens the web data.
     * UserModel defines the Users which are people who use the bank
     * It contains an id, a first name, and a last name.
     */
    public class UserModel
    {
        //public fields
        public uint id;
        public string fName;
        public string lName;

        //alternate constructor
        public UserModel(uint id, string fName, string lName)
        {
            this.id = id;
            this.fName = fName;
            this.lName = lName;
        }
    }
}
