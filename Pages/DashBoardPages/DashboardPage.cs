using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace dev_repmove_autotest.Pages
{
    internal class DashboardPage : BasePage
    {
        private readonly ILocator dashboardTitle;

        public DashboardPage(IPage page) : base(page)
        {
            dashboardTitle = page.GetByText("Dashboard My Stats Activity");
        }

        public async Task AssertDashboardIsDisplayed()
        {
            await Expect(dashboardTitle).ToBeVisibleAsync(new() { Timeout = 10000 });
        }
    }
}
