using Allure.NUnit.Attributes;
using dev_repmove_autotest.Pages;
using dev_repmove_autotest.Pages.LoginPages;
using dev_repmove_autotest.Utils.Enum;
using dev_repmove_autotest.Utils.Helper;

namespace dev_repmove_autotest.Tests.E2ETests
{
    [TestFixtureSource(typeof(FixtureSources), nameof(FixtureSources.BrowserTypes))]
    [Parallelizable(ParallelScope.Fixtures)]
    [AllureOwner("Ihor Zhurbuk")]
    [AllureFeature("Login")]
    internal class LoginE2ETest : BaseTest
    {
        private SignInPage? _signInPage;
        private SignInPage SignInPage => _signInPage ??= new SignInPage(Page!);
        public LoginE2ETest(EBrowserType browserType) : base(browserType)
        {
        }

        [Test(Description = "Full Login validation flow")]
        [AllureName("Full Login Flow Validation")]
        [AllureTag("Regression", "Login", "FE")]
        public async Task FullLoginFlowWithValidation()
        {
            await RunAllure.RunAllureStepAsync($"[1] Validate Invalid Email Input[{CurrentBrowserType}]", VerifyInvalidEmailMessage);
            await RunAllure.RunAllureStepAsync($"[2] Validate Required Email Input[{CurrentBrowserType}]", VerifyRequiredEmailInput);
            await RunAllure.RunAllureStepAsync($"[3] Validate Required Password Input[{CurrentBrowserType}]", VerifyRequiredPasswordInput);
            await RunAllure.RunAllureStepAsync($"[4] Try to sign in with non-existing email [{CurrentBrowserType}]", VerifyUnExistsEmailLogin);
            await RunAllure.RunAllureStepAsync($"[5] Try to sign in with wrong password [{CurrentBrowserType}]", VerifyUnExistsPasswordLogin);
            await RunAllure.RunAllureStepAsync($"[6] Successful login [{CurrentBrowserType}]", VerifySuccessfulLogin);

        }
        private async Task VerifyInvalidEmailMessage()
        {
            await SignInPage.FillEmail(Generator.GenerateRandomEmail().Replace("@", ""));
            Assert.That(await SignInPage.IsInvalidEmailErrorVisible(), Is.True, "Invalid email error message should be visible");
        }
        private async Task VerifyRequiredEmailInput()
        {
            await SignInPage.FillEmail("");
            Assert.That(await SignInPage.IsRequiredEmailErrorVisible(), Is.True, "Required email error message should be visible");

        }
        private async Task VerifyRequiredPasswordInput()
        {
            await SignInPage.SignIn(Generator.GenerateRandomEmail(), "");
            Assert.That(await SignInPage.IsRequiredPasswordErrorVisible(), Is.True, "Required password error message should be visible");
        }
        private async Task VerifyUnExistsEmailLogin()
        {
            SignInPage signInPage = new SignInPage(Page!);
            await signInPage.SignIn(Generator.GenerateRandomEmail(), Generator.GenerateRandomPassword(10));
            Assert.That(await signInPage.IsInvalidLoginErrorVisible(), Is.True, "Invalid login error message should be visible");
        }
        private async Task VerifyUnExistsPasswordLogin()
        {
            await SignInPage.SignIn(User.Email, Generator.GenerateRandomPassword(10));
            Assert.That(await SignInPage.IsInvalidLoginErrorVisible(), Is.True, "Invalid login error message should be visible");
        }
        private async Task VerifySuccessfulLogin()
        {
            await SignInPage.SignIn(User.Email, User.MainPassword);

            DashboardPage dashboardPage = new DashboardPage(Page!);
            await dashboardPage.AssertDashboardIsDisplayed();
        }
    }
}
