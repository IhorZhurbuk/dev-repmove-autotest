using Allure.NUnit.Attributes;
using dev_repmove_autotest.Pages;
using dev_repmove_autotest.Pages.LoginPages;
using dev_repmove_autotest.Utils.Enum;
using dev_repmove_autotest.Utils.Helper;

namespace dev_repmove_autotest.Tests.SmokeTests
{
    [TestFixtureSource(typeof(FixtureSources), nameof(FixtureSources.BrowserTypes))]
    [Parallelizable(ParallelScope.Fixtures)]
    [AllureOwner("Ihor Zhurbuk")]
    [AllureFeature("Login")]
    internal class LoginSmokeTest : BaseTest
    {
        public LoginSmokeTest(EBrowserType browserType) : base(browserType)
        {
        }

        [Test(Description = "Success login")]
        [AllureName("Success login")]
        [AllureTag("Smoke", "Login", "FE")]
        public async Task FullLoginFlow()
        {

            await RunAllure.RunAllureStepAsync($"[1] Successful login [{CurrentBrowserType}]", VerifySuccessfulLogin);

        }
        private async Task VerifySuccessfulLogin()
        {
            SignInPage signInPage = new SignInPage(Page!);
            await signInPage.SignIn(User.Email, User.MainPassword);

            DashboardPage dashboardPage = new DashboardPage(Page!);
            await dashboardPage.AssertDashboardIsDisplayed();
        }
    }
}
