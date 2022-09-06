namespace BoredApi.Data.Models.Exceptions
{
    public class SuchAUserDoesNotExistException : Exception
    {
        public SuchAUserDoesNotExistException(int id)
            : base(String.Format("There is no user with an id {0}.", id)) { }
    }
}
