
namespace ModelSecurity.Models
{
    public class FormModule
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public int ModuleId { get; set; }
        public bool IsDeleted { get; set; }

        // Relaciones
        public Form? Form { get; set; }
        public Module? Module { get; set; }
    }
}
