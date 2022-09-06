namespace BoredApi.Data.Models.Exceptions
{
    public class UserIsNotPartOfTheGroupException : Exception
    {
        public UserIsNotPartOfTheGroupException(int id)
            : base(String.Format("The user with an id {0} is not part of the group.", id)) { }
    }
}
