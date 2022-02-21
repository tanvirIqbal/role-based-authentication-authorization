namespace DotnetAuth.DTOs
{
    public class RoleDTO
    {
        public string Name { get; set; }
        public RoleDTO(string name)
        {
            Name = name;
        }
    }
}