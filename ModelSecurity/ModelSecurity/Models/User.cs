using System;

namespace ModelSecurity.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Active { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsDeleted { get; set; }

        // Clave foranea hacia Person
        public int PersonId { get; set; }

        // Relaciones
        public Person? Person { get; set; }
        public ICollection<RolUser>? RolUser { get; set; }
    }
}
