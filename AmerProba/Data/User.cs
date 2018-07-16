using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmerProba.Data
{
    public class User:IdentityUser
    {
        public List<Transakcija> Transakcije { get; set; }
    }
}
