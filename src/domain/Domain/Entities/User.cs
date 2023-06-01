using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class User : IdentityUser<int>
    {   
        public string Name { get; set; }
        public ICollection<Audit> Audits { get; set; }
    }
}
