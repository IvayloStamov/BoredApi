namespace BoredApi.Data.Models.Exceptions
{
    public class ActivActivityException : Exception
    {
        public ActivActivityException()
            : base(String.Format("There is already an active activity.")) { }
    }
}
