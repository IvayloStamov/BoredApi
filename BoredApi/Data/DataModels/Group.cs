using System.ComponentModel.DataAnnotations;

namespace BoredApi.Data.DataModels
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
