using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrchardist.Data
{
    public class GlobalPlantList
    {
    public int ID { get; set; }

    [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
    [Required]
    [StringLength(60, MinimumLength = 2)]
    public string Name { get; set; }

    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string FruitVariety { get; set; }

    public string Origin { get; set; }
    //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
    //[Required]
    //[StringLength(60)]
    //public string Parentage { get; set; }
    public string YearDeveloped { get; set; }

    public string Use { get; set; }

    public string Comments { get; set; }

    //public string OrchardName { get; set; }
  }
}
