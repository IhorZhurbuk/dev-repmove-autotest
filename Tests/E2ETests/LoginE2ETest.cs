using Allure.NUnit.Attributes;
using dev_repmove_autotest.Pages.LoginPage;
using dev_repmove_autotest.Utils.ConfigManager;
using dev_repmove_autotest.Utils.Enum;
using dev_repmove_autotest.Utils.Helper;

namespace dev_repmove_autotest.Tests.E2ETests
{
    [TestFixtureSource(typeof(FixtureSources), nameof(FixtureSources.BrowserTypes))]
    [Parallelizable(ParallelScope.Fixtures)]
    [AllureOwner("Ihor Zhurbuk")]
    [AllureFeature("Login E2E Feature")]
    internal class LoginE2ETest : BaseTest
    {
        public LoginE2ETest(EBrowserType browserType) : base(browserType)
        {
        }
     
        [Test(Description = "Full Login validation flow")]
        [AllureName("Full Login Flow Validation")]
        [AllureTag("Regression", "Login", "FE")]
        public async Task FullLoginFlowWithValidation()
        {
            await RunAllure.RunAllureStepAsync($"[1] Login[{CurrentBrowserType}]", Login);

        }
        private async Task Login()
        {
            SignInPage signInPage = new SignInPage(Page!);
            await signInPage.SignIn(ConfigurationManager.Instance.User.Email, ConfigurationManager.Instance.User.MainPassword);
        }
    }
}
