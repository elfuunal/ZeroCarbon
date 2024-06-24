using System.ComponentModel.DataAnnotations;

namespace NeyeTech.ZeroCarbon.Core.Enums
{
    public enum ESort
    {
        [Display(Name = "OrderBy")]
        ASC = 1,

        [Display(Name = "OrderByDescending")]
        DESC = 2
    }
}
