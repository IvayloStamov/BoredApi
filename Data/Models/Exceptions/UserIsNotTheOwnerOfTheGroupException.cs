namespace BoredApi.Data.Models.Exceptions
{
    public class UserIsNotTheOwnerOfTheGroupException : Exception
    {
        public UserIsNotTheOwnerOfTheGroupException(int id)
            : base(String.Format("The user with an id {0} does not have the necessary rights for this action.", id)) { }
    }
}
