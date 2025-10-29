
namespace ModelSecurity.Models
{
    public class Rol
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }

        // Relaciones
        public ICollection<RolUser>? RolUser { get; set; }
        public ICollection<RolFormPermit>? RolFormPermit { get; set; }
    }
}
