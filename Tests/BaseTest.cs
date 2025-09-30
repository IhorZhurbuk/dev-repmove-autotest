using Allure.Net.Commons;
using Allure.NUnit;
using dev_repmove_autotest.Factories;
using dev_repmove_autotest.Utils.ConfigManager;
using dev_repmove_autotest.Utils.Enum;
using dev_repmove_autotest.Utils.Helper;
using Microsoft.Playwright;

namespace dev_repmove_autotest.Tests
{
    [AllureNUnit]
    public abstract class BaseTest
    {
        private  IPlaywright? _playwright;
        protected IBrowser? Browser;
        protected IBrowserContext? Context;
        protected IPage? Page;
        protected EBrowserType CurrentBrowserType;
        private string baseUrl;

        protected BaseTest(EBrowserType browserType)
        {
            CurrentBrowserType = browserType;

        }

        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            _playwright = await Playwright.CreateAsync();
             baseUrl = ConfigurationManager.Instance.BaseUrl;
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            _playwright?.Dispose();
        }

        [SetUp]
        public async Task PerTestSetup()
        {
            var (browser, context) = await DriverFactory.CreateBrowserAsync(_playwright, CurrentBrowserType, headless: false);
            Browser = browser;
            Context = context;
            Page = await Context.NewPageAsync();

            AllureLifecycle.Instance.UpdateTestCase(tc =>
            {
                if (tc != null && !tc.parameters.Any(p => p.name == "Browser"))
                {
                    tc.parameters.Add(new Parameter { name = "Browser", value = CurrentBrowserType.ToString() });
                }
                if (tc != null && !tc.labels.Any(l => l.name == "suite"))
                {
                    tc.labels.Add(Label.Suite("RepMove Test Suite"));
                }
            });

            await Page.GotoAsync(baseUrl);
        }

        [TearDown]
        public async Task PerTestTeardown()
        {
            var testName = TestContext.CurrentContext.Test.Name;
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var hasFailed = status == NUnit.Framework.Interfaces.TestStatus.Failed;

            if (hasFailed && Page != null && !Page.IsClosed)
            {
                var screenshotName = $"[FAIL]_{testName}_{CurrentBrowserType}".Replace(" ", "_");
                await RunAllure.AttachScreenshotAsync(Page, screenshotName);
            }

            if (Page != null && !Page.IsClosed)
            {
                await Page.CloseAsync();
            }

            if (Context != null)
            {
                await Context.CloseAsync();
            }

            if (Browser != null)
            {
                await Browser.CloseAsync();
            }
        }
    }
}
