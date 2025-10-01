using System.ComponentModel.DataAnnotations;

namespace dev_repmove_autotest.Utils.Enum
{
    public enum ECountryCode
    {
        [Display(Name = "+44", Description = "fi-gb")]
        GreatBritan,
        [Display(Name = "+1", Description = "fi-us")]
        Usa,
        [Display(Name = "+1", Description = "fi-ca")]
        Canada,
        [Display(Name = "+380", Description = "fi-ua")]
        Ukraine
        // Add more country codes 
    }
}
