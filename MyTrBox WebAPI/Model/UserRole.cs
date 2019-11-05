using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyTrBox_WebAPI.Model
{
    public class UserRole : IdentityRole<Guid>
    {
        public UserRole()
            : base() {

        } 
        public UserRole(string roleName)
            : base(roleName) {

        }
    }
}
