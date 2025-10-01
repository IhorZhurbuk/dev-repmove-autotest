using dev_repmove_autotest.Shared.ValidationMessages;
using dev_repmove_autotest.Utils.Enum;
using dev_repmove_autotest.Utils.Helper;
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

        private readonly ILocator requiredFirstName;
        private readonly ILocator requiredLastName;
        private readonly ILocator requiredCompanyName;
        private readonly ILocator requiredCompanyNameIndustry;
        private readonly ILocator requiredEmail;
        private readonly ILocator requiredPhone;
        private readonly ILocator requiredPassword;

        private readonly ILocator minLengthFirstName;
        private readonly ILocator minLengthLastName;
        private readonly ILocator minLengthPassword;
        private readonly ILocator invalidEmailAdress;
        private readonly ILocator invalidPhoneNumber;

        private EIndustry industry { get; set; }
        private ECountryCode countryCode { get; set; }
        public SignUpPage(IPage page) : base(page)
        {
            // Використовуємо app-input компонент з фільтрацією по тексту
            firsNameInput = page.Locator("app-input").Filter(new() { HasText = "First Name" }).GetByRole(AriaRole.Textbox);
            lastNameInput = page.Locator("app-input").Filter(new() { HasText = "Last Name" }).GetByRole(AriaRole.Textbox);
            companyNameInput = page.Locator("app-input").Filter(new() { HasText = "Company Name" }).GetByRole(AriaRole.Textbox);
            industryCombobox = page.Locator("ng-select").Filter(new() { HasText = "Industry" }).GetByRole(AriaRole.Combobox);
            emailInput = page.Locator("input[type='email']");
            countryCodeCombobox = page.Locator("app-phone-number").GetByRole(AriaRole.Combobox);
            phoneNumberInput = page.Locator("app-input").Filter(new() { HasText = "Phone" }).GetByRole(AriaRole.Textbox);
            passwordInput = page.Locator("input[type='password']");
            signUpButton = page.GetByRole(AriaRole.Button, new() { Name = "Sign Up", Exact = true });
            SelectRandomOptions();

            #region ErrorValidation
            requiredFirstName = page.GetByText(SignUpErrorMessages.RequiredFirstName);
            requiredLastName = page.GetByText(SignUpErrorMessages.RequiredLastName);
            requiredCompanyName = page.GetByText(SignUpErrorMessages.RequiredCompanyName);
            requiredCompanyNameIndustry = page.GetByText(SignUpErrorMessages.RequiredIndustry);
            requiredEmail = page.GetByText(SignUpErrorMessages.RequiredEmail);
            requiredPhone = page.GetByText(SignUpErrorMessages.RequiredPhone);
            requiredPassword = page.GetByText(SignUpErrorMessages.RequiredPassword);

            minLengthFirstName = page.GetByText(SignUpErrorMessages.MinLengthFirstName);
            minLengthLastName = page.GetByText(SignUpErrorMessages.MinLengthLastName);
            minLengthPassword = page.GetByText(SignUpErrorMessages.MinLengthPassword);
            invalidEmailAdress = page.GetByText(SignUpErrorMessages.InvalidEmailAdress);
            invalidPhoneNumber = page.GetByText(SignUpErrorMessages.Invalidnumber);
            #endregion
        }
        public async Task ChooseRandomIndustry()
        {
            await SelectComboBoxOptionAsync(industryCombobox, GetDisplayName(industry));
        }

        public async Task ChooseRandomCountryCode()
        {
            await SelectComboBoxOptionAsync(countryCodeCombobox, GetDisplayName(countryCode), GetDisplayDescription(countryCode), useClassSelector: true);
        }

        public string GetValidPhoneNumberForSelectedCountry()
        {
            return Generator.GenerateValidPhoneNumber(countryCode);
        }

        private void SelectRandomOptions()
        {
            industry = GetRandomEnum<EIndustry>();
            countryCode = GetRandomEnum<ECountryCode>();
        }

        public async Task ClickSignUpButton()
        {
            await signUpButton.ClickAsync();
        }
        public async Task<bool> AreAllRequiredFieldsVisible()
        {
            var allVisible = await Task.WhenAll(
                requiredFirstName.IsVisibleAsync(),
                requiredLastName.IsVisibleAsync(),
                requiredCompanyName.IsVisibleAsync(),
                requiredCompanyNameIndustry.IsVisibleAsync(),
                requiredEmail.IsVisibleAsync(),
                requiredPhone.IsVisibleAsync(),
                requiredPassword.IsVisibleAsync()
            );

            return allVisible.All(v => v);
        }
        public async Task<bool> AreAllMinLengthValidationMessagesVisible()
        {
            var allVisible = await Task.WhenAll(
                minLengthFirstName.IsVisibleAsync(),
                minLengthLastName.IsVisibleAsync(),
                minLengthPassword.IsVisibleAsync(),
                invalidEmailAdress.IsVisibleAsync(),
                invalidPhoneNumber.IsVisibleAsync()
            );
            return allVisible.All(v => v);
        }
        public async Task FillFirstName(string text)
        {
            await firsNameInput.FillAsync(text);
        }
        public async Task FillLastName(string text)
        {
            await lastNameInput.FillAsync(text);
        }
        public async Task FillCompanyName(string text)
        {
            await companyNameInput.FillAsync(text);
        }
        public async Task FillEmail(string text)
        {
            await emailInput.FillAsync(text);
        }
        public async Task FillPhone(string text)
        {
            await phoneNumberInput.FillAsync(text);
        }
        public async Task FillPassword(string text)
        {
            await passwordInput.FillAsync(text);
        }
    }
}
