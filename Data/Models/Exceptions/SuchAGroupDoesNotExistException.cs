namespace BoredApi.Data.Models.Exceptions
{
    public class SuchAGroupDoesNotExistException : Exception
    {
        public SuchAGroupDoesNotExistException(int id)
            : base(String.Format("There is no group with an id {0}.", id)) { }
    }
}
