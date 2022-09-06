namespace BoredApi.Data.Models.Exceptions
{
    public class OwnerCannotRemoveThemselvesFromTheGroupException : Exception
    {
        public OwnerCannotRemoveThemselvesFromTheGroupException()
            : base(String.Format("The owner of the group can not remove him/her-self from the group.")){ }
    }
}
