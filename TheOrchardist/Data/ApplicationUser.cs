using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TheOrchardist.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
    }
  public class ApplicationUserTokens : IdentityUserToken<string>
  {
   
  }
  public class ApplicationIdentityRole : IdentityRole
  {
  }
  public class ApplicationIdentityRoleClaim : IdentityRoleClaim<string>
    {
    }
  public class ApplicationIdentityUserLogin : IdentityUserLogin<string>
  {
  }
    public class ApplicationIdentityUserRole : IdentityUserRole<string>
    {
    }
    public class ApplicationIdentityUserClaim : IdentityUserClaim<string>
    {
    }

 
}
