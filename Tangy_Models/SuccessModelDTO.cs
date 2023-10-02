using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_Models;
public class SuccessModelDTO
{
    public int StatusCode { get; set; }
    public string ErrorMessage { get; set; }
    public string Data { get; set; }
}
