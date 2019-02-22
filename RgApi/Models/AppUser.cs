﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RgApi.Models
{
    // Add profile data for application users by adding properties to the RgApiUser class
    public class AppUser : IdentityUser
    {
        public string ProfileImageUrl   { get; set; }
        public string FirstName         { get; set; }
        public string LastName          { get; set; }
        public DateTime MemberSince     { get; set; }

        public virtual IEnumerable<IdentityUserClaim<string>> Claims    { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens    { get; set; }
    }
}
