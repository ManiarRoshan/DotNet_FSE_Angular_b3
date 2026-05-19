namespace ContactmgmtWebAPI.Exceptions
{
    public class ContactNotFoundException : Exception
    {
        public ContactNotFoundException(int id)
            : base($"Contact with ID {id} was not found.") { }
    }
}
