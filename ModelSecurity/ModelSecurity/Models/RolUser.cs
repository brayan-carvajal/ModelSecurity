namespace ModelSecurity.Models
{
    public class RolUser
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }

        // Relaciones
        public Rol? Rol { get; set; }
        public User? User { get; set; }
    }
}
