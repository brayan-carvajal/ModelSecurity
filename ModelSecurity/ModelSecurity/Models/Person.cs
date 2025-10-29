using System;

namespace ModelSecurity.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document_type { get; set; }
        public string Document { get; set; }
        public DateTime DateBorn { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string PersonExter { get; set; }
        public string EpsId { get; set; }
        public string SecondLastName { get; set; }
        public string MiddleName { get; set; }
        public bool Active { get; set; }
        public int CityId { get; set; }

        // Relaciones
        public User? User { get; set; }
    }
}
