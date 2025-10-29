namespace ModelSecurity.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsDeleted { get; set; }
        public int PersonId { get; set; }
    }
}
