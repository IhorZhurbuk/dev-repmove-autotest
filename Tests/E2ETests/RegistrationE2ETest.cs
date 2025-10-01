using Allure.NUnit.Attributes;
using dev_repmove_autotest.Pages.DashBoardPages;
using dev_repmove_autotest.Pages.LoginPages;
using dev_repmove_autotest.Pages.RegistrationPages;
using dev_repmove_autotest.Utils.Enum;
using dev_repmove_autotest.Utils.Helper;

namespace dev_repmove_autotest.Tests.E2ETests
{
    [TestFixtureSource(typeof(FixtureSources), nameof(FixtureSources.BrowserTypes))]
    [Parallelizable(ParallelScope.Fixtures)]
    [AllureOwner("Ihor Zhurbuk")]
    [AllureFeature("Registration")]
    internal class RegistrationE2ETest : BaseTest
    {
        private SignUpPage? _signUpPage;
        private SignUpPage SignUpPage => _signUpPage ??= new SignUpPage(Page!);
        public RegistrationE2ETest(EBrowserType browserType) : base(browserType)
        {
        }
        [Test(Description = "Full Registration validation flow")]
        [AllureName("Full Registration Validation Flow")]
        [AllureTag("Regression", "Registration", "FE")]
        public async Task FullLoginFlowWithValidation()
        {
            await RunAllure.RunAllureStepAsync($"[1] Open Registration page [{CurrentBrowserType}]", OpenSignUpPage);
            await RunAllure.RunAllureStepAsync($"[2] Validate All Required feild [{CurrentBrowserType}]", VerifyRequiredFeild);
            await RunAllure.RunAllureStepAsync($"[3] Validate All Min Length Feild [{CurrentBrowserType}]", MinLengthFirstName);
            await RunAllure.RunAllureStepAsync($"[4] Successful signUp [{CurrentBrowserType}]", VerifySuccessfuSignUp);

        }
        private async Task OpenSignUpPage()
        {
            SignInPage signInPage = new SignInPage(Page!);
            await signInPage.ClickSignUp();
        }
        private async Task VerifyRequiredFeild()
        {
            await SignUpPage.ClickSignUpButton();
            Assert.That(await SignUpPage.AreAllRequiredFieldsVisible(), Is.True, "All required feild  should be visible");
        }
        private async Task MinLengthFirstName()
        {
            await SignUpPage.FillFirstName("a");
            await SignUpPage.FillLastName("b");
            await SignUpPage.FillPassword("123");
            await SignUpPage.FillEmail("invalidemail");
            await SignUpPage.ChooseRandomCountryCode();
            await SignUpPage.FillPhone("11111");
            Assert.That(await SignUpPage.AreAllMinLengthValidationMessagesVisible(), Is.True, "Error message should be visible for all feild");
        }
        private async Task VerifySuccessfuSignUp()
        {
            await SignUpPage.FillFirstName(Generator.GenerateRandomString(10));
            await SignUpPage.FillLastName(Generator.GenerateRandomString(10));
            await SignUpPage.FillCompanyName(Generator.GenerateRandomString(10));
            await SignUpPage.ChooseRandomIndustry();
            await SignUpPage.FillEmail(Generator.GenerateRandomEmail());
            await SignUpPage.ChooseRandomCountryCode();
            await SignUpPage.FillPhone(SignUpPage.GetValidPhoneNumberForSelectedCountry());
            await SignUpPage.FillPassword(Generator.GenerateRandomPassword(9));
            await SignUpPage.ClickSignUpButton();

            SelectPlanPage planPage = new SelectPlanPage(Page!);
            await planPage.AssertPlanIsDisplayed();

        }
    }
}
