using System.ComponentModel.DataAnnotations;

namespace dev_repmove_autotest.Utils.Enum
{
    public enum ECountryCode
    {
        [Display(Name = "+44")]
        GreatBritan,
        [Display(Name = "+1")]
        Usa,
        [Display(Name = "+1")]
        Canada,
        [Display(Name = "+380")]
        Ukraine
    }
}
