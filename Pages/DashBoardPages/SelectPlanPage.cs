using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace dev_repmove_autotest.Pages.DashBoardPages
{
    internal class SelectPlanPage : BasePage
    {
        private readonly ILocator planLocator;
        public SelectPlanPage(IPage page) : base(page)
        {
            planLocator = page.Locator("div.modal-dialog").Filter(new() { HasText = "Select Plan" });
        }
        public async Task AssertPlanIsDisplayed()
        {
            await Expect(planLocator).ToBeVisibleAsync(new() { Timeout = 10000 });
        }
    }
}

