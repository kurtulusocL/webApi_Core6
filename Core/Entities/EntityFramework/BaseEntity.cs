using System.ComponentModel.DataAnnotations;

namespace WebApiCore6.Core.Entities.EntityFramework
{
    public class BaseEntity : IEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public void SetCreatedDate()
        {
            CreatedDate = DateTime.Now.ToLocalTime();
        }
        public BaseEntity()
        {
            SetCreatedDate();
        }
    }
}
