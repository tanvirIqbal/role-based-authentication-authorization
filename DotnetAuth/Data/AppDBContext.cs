using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetAuth.Data.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotnetAuth.Data
{
    public class AppDBContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }
    }
}