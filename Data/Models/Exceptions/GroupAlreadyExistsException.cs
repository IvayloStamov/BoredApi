namespace BoredApi.Data.Models.Exceptions
{
    public class GroupAlreadyExistsException : Exception
    {
        public GroupAlreadyExistsException(string username)
            : base(String.Format("Group with the same name {0} already exists.", username)) { }
    }
}
