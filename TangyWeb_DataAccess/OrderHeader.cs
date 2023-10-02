using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tangy_DataAccess;
public class OrderHeader
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string UserId { get; set; }
    // add navigation property : # TODO

}
