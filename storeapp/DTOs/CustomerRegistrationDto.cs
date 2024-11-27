namespace storeapp.DTOs
{
    public class CustomerRegistrationDto
    {
        

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }
        public string? Password { get; set; }


    }
}
