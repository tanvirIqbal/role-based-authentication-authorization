using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetAuth.DTOs
{
    public class UserDTO
    {
        public UserDTO(string fullName, string email, string userName, DateTime dateCreated, DateTime dateModified, List<string> roles)
        {
            FullName = fullName;
            Email = email;
            UserName = userName;
            DateCreated = dateCreated;
            DateModified = dateModified;
            Roles = roles;
        }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Token { get; set; }
        public List<string> Roles { get; set; }
    }
}