namespace ModelSecurity.Dtos
{
    public class RolFormPermitDto
    {
        public int RolId { get; set; }
        public int FormId { get; set; }
        public int PermissionId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
