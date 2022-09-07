namespace BoredApi.Data.Models.Exceptions
{
    public class NoActiveRequestsException : Exception
    {
        public NoActiveRequestsException()
            : base(String.Format("There are currently no requests.")) { }
    }
}
