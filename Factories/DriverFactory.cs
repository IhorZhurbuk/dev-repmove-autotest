using dev_repmove_autotest.Utils.Enum;
using Microsoft.Playwright;
using System.Runtime.InteropServices;

public static class DriverFactory
{
    public static async Task<(IBrowser Browser, IBrowserContext Context)> CreateBrowserAsync(
        IPlaywright playwright,
        EBrowserType browserType,
        bool headless = true)
    {

        var args = new List<string>
        {
            "--window-size=1920,1080"
        };

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            args.Add("--start-maximized");
            args.Add("--window-position=0,0");
        }

        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = headless,
            Args = args.ToArray()
        };

        var browser = browserType switch
        {
            EBrowserType.Chromium => await playwright.Chromium.LaunchAsync(launchOptions),
            EBrowserType.Firefox => await playwright.Firefox.LaunchAsync(launchOptions),
            EBrowserType.WebKit => await playwright.Webkit.LaunchAsync(launchOptions),
            _ => throw new ArgumentOutOfRangeException(nameof(browserType), $"Not supported browser: {browserType}")
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
