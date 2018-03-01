using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrchardist.Data
{
    public class Orchard
    {
    public int OrchardID { get; set; }

    public string UserID { get; set; }
    [Required]
    [Display(Name = "Orchard Name")]
    public String OrchardName { get; set; }

    [DataType(DataType.Text, ErrorMessage = "Image Validation Failed")]
    public string OrchardMapDataUrl { get; set; }
  }
}
