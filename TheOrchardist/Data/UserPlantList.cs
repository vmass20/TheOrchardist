using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrchardist.Data
{
  public class UserPlantList
  {
  public int ID { get; set; }

    public String UserID { get; set; }
    [Display(Name = "Orchard Name")]
    public string OrchardName { get; set; }

    [Required]
    [StringLength(60, MinimumLength = 2)]
    [Display(Name ="Variety Name")]
    public string PlantName { get; set; }

    [StringLength(60, MinimumLength = 3)]
    [Required]
    public string FruitVariety { get; set; }

    [StringLength(200)]
    public string Origin { get; set; }

    public string Use { get; set; }

    [Display(Name = "Year Developed")]
    public string YearDeveloped { get; set; }

    public string Comments { get; set; }

    [Display(Name = "Date Planted")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime DatePlanted { get; set; }

    [Display(Name = "Tree Count")]
    public int TreeCount { get; set; }

    // [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
    [StringLength(200)]
    public string Parentage { get; set; }

    public string Rootstock { get; set; }

    public string RootstockHeight { get; set; }
    public string RootstockWidth { get; set; }

    [Display(Name = "Harvest Season")]
    public string HarvestSeason { get; set; }

    [Display(Name = "Harvest Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime ActualHarvestDate { get; set; }

    public string MaintainedHeight { get; set; }
    public string MaintainedWidth { get; set; }

    public string Location { get; set; }
    public string Area { get; set; }
    public string Section { get; set; }
    public string Number { get; set; }
  }
}
