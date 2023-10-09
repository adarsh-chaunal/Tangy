using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Tangy_DataAccess;
public class ApplicationUser:IdentityUser // this will add name of the user in AspNetUser table by extending IdentityUser
{
    public string Name { get; set; }    
}
