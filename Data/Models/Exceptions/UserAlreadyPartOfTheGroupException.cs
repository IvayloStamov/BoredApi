namespace BoredApi.Data.Models.Exceptions
{
    public class UserAlreadyPartOfTheGroupException : Exception
    {
        public UserAlreadyPartOfTheGroupException(int id)
            : base(String.Format("The user with an id {0} is already part of the group.", id)) { }
    }
}
