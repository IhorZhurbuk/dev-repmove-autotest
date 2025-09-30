using dev_repmove_autotest.Utils.Enum;
using Microsoft.Playwright;

namespace dev_repmove_autotest.Pages.RegistrationPages
{
    internal class SignUpPage : BasePage
    {
        private readonly ILocator firsNameInput;
        private readonly ILocator lastNameInput;
        private readonly ILocator companyNameInput;
        private readonly ILocator industryCombobox;
        private readonly ILocator emailInput;
        private readonly ILocator countryCodeCombobox;
        private readonly ILocator phoneNumberInput;
        private readonly ILocator passwordInput;
        private readonly ILocator signUpButton;

        private EIndustry industry { get; set; }
        private ECountryCode countryCode { get; set; }
        public SignUpPage(IPage page) : base(page)
        {
            firsNameInput = page.GetByRole(AriaRole.Textbox, new() { Name = "First Name" });
            lastNameInput = page.GetByRole(AriaRole.Textbox, new() { Name = "Last Name" });
            companyNameInput = page.GetByRole(AriaRole.Textbox, new() { Name = "Company Name" });
            industryCombobox = page.Locator("form span");
            emailInput = page.GetByRole(AriaRole.Textbox, new() { Name = "Email" });
            countryCodeCombobox = page.Locator("app-phone-number span");
            phoneNumberInput = page.GetByRole(AriaRole.Textbox, new() { Name = "Phone Number" });
            passwordInput = page.GetByRole(AriaRole.Textbox, new() { Name = "Password" });
            signUpButton = page.GetByRole(AriaRole.Button, new() { Name = "Sign Up", Exact = true });
            SelectRandomOptions();
        }
        public async Task ChooseRandomIndustry()
        {
            await SelectComboBoxOptionAsync(industryCombobox, GetDisplayName(industry));
        }

        public async Task ChooseRandomEducationLevel()
        {
            await SelectComboBoxOptionAsync(countryCodeCombobox, GetDisplayName(countryCode));
        }
        private void SelectRandomOptions()
        {
            industry = GetRandomEnum<EIndustry>();
            countryCode = GetRandomEnum<ECountryCode>();
        }
    }
}
