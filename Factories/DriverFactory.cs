using dev_repmove_autotest.Utils.Enum;
using Microsoft.Playwright;

namespace dev_repmove_autotest.Factories
{
    public static class DriverFactory
    {
        public static async Task<(IBrowser Browser, IBrowserContext Context)> CreateBrowserAsync(IPlaywright playwright, EBrowserType browserType, bool headless = true)
        {
            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = headless,
                Args = new[] {
                    "--start-maximized",
                    "--window-size=1920,1080",
                    "--window-position=0,0"
                }
            };

            var browser = browserType switch
            {
                EBrowserType.Chromium => await playwright.Chromium.LaunchAsync(launchOptions),
                EBrowserType.Firefox => await playwright.Firefox.LaunchAsync(launchOptions),
                EBrowserType.WebKit => await playwright.Webkit.LaunchAsync(launchOptions),
                _ => throw new ArgumentOutOfRangeException(nameof(browserType), $"Not suported browser: {browserType}")
            };

            var context = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1920, Height = 1080 },
                ScreenSize = new ScreenSize { Width = 1920, Height = 1080 },
                HasTouch = false,
                IsMobile = false
            });

            return (browser, context);
        }
    }
}
