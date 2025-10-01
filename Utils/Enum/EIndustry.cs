using System.ComponentModel.DataAnnotations;

namespace dev_repmove_autotest.Utils.Enum
{
    public enum EIndustry
    {
        [Display(Name = "Equipment Rental")]
        EquipmentRental,
        [Display(Name = "Rep Agency")]
        RepAgency
        // Add more industries as needed
    }
}