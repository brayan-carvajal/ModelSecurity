namespace ModelSecurity.Dtos
{
    public class RolDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
