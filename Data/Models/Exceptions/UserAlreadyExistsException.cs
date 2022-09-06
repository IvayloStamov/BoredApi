namespace BoredApi.Data.Models.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() { }

        public UserAlreadyExistsException(string username)
            : base(String.Format("User with the same username {0} already exists.", username)) { }
    }
}
