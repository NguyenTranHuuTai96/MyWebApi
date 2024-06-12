using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserIdentity : IdentityUser
    {
        [StringLength(250)]
        public string? FullName { get; set; }
        [StringLength(500)]
        public string? Address { get; set; } 
    }
}
