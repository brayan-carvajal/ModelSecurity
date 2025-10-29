
namespace ModelSecurity.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        // Relaciones
        public ICollection<FormModule>? FormModule { get; set; }
    }
}
