namespace Vila.Web.Models.customer
{
    public class CustomerModel
    {
        public int userId { get; set; }
        public string Mobile { get; set; }

        public string JwtSecret { get; set; }

        public string Role { get; set; }
    }
}
