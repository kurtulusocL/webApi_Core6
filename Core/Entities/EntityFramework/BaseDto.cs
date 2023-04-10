namespace WebApiCore6.Core.Entities.EntityFramework
{
    public class BaseDto : IDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public void SetCreatedDate()
        {
            CreatedDate = DateTime.Now.ToLocalTime();
        }
        public BaseDto()
        {
            SetCreatedDate();
        }
    }
}
