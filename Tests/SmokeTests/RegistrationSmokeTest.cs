using Allure.NUnit.Attributes;
using dev_repmove_autotest.Pages.DashBoardPages;
using dev_repmove_autotest.Pages.LoginPages;
using dev_repmove_autotest.Pages.RegistrationPages;
using dev_repmove_autotest.Utils.Enum;
using dev_repmove_autotest.Utils.Helper;

namespace dev_repmove_autotest.Tests.SmokeTests
{
    [TestFixtureSource(typeof(FixtureSources), nameof(FixtureSources.BrowserTypes))]
    [Parallelizable(ParallelScope.Fixtures)]
    [AllureOwner("Ihor Zhurbuk")]
    [AllureFeature("Registration")]
    internal class RegistrationSmokeTest : BaseTest
    {
        public RegistrationSmokeTest(EBrowserType browserType) : base(browserType)
        {
        }
        [Test(Description = "Success Registration")]
        [AllureName("Registration login")]
        [AllureTag("Smoke", "Registration", "FE")]
        public async Task FullRegistrFlow()
        {
            await RunAllure.RunAllureStepAsync($"[4] Successful signUp [{CurrentBrowserType}]", VerifySuccessfuSignUp);

        }
        private async Task VerifySuccessfuSignUp()
        {
            SignInPage signInPage = new SignInPage(Page!);
            await signInPage.ClickSignUp();

            SignUpPage signUpPage = new SignUpPage(Page!);
            await signUpPage.FillFirstName(Generator.GenerateRandomString(10));
            await signUpPage.FillLastName(Generator.GenerateRandomString(10));
            await signUpPage.FillCompanyName(Generator.GenerateRandomString(10));
            await signUpPage.ChooseRandomIndustry();
            await signUpPage.FillEmail(Generator.GenerateRandomEmail());
            await signUpPage.ChooseRandomCountryCode();
            await signUpPage.FillPhone(signUpPage.GetValidPhoneNumberForSelectedCountry());
            await signUpPage.FillPassword(Generator.GenerateRandomPassword(9));
            await signUpPage.ClickSignUpButton();

            SelectPlanPage planPage = new SelectPlanPage(Page!);
            await planPage.AssertPlanIsDisplayed();

        }
    }
}
