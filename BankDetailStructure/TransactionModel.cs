namespace BankDetailStructure
{
    /**
     * TransactionModel represent the templates for the .NET JSON serializers.
     * It is going to be the go-betweens the web data.
     * TransactionModel define transactions of money between accounts
     * It contains an ID, 2 associated account ID’s (one sending the money, one receiving the money), and an amount
     */
    public class TransactionModel
    {
        //public fields
        public uint id;
        public uint idOfSender;
        public uint idOfReceiver;
        public uint amount;

        //alternate constructor
        public TransactionModel(uint id, uint idOfSender, uint idOfReceiver, uint amount)
        {
            this.id = id;
            this.idOfSender = idOfSender;
            this.idOfReceiver = idOfReceiver;
            this.amount = amount;
        }
    }
}
