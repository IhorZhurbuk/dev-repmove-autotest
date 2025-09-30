using Microsoft.Playwright;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace dev_repmove_autotest.Pages
{
    public class BasePage
    {
        protected readonly IPage page;

        public BasePage(IPage page)
        {
            this.page = page;
        }
        protected async Task SelectComboBoxOptionAsync(ILocator comboBox, string optionText, int timeout = 5000)
        {
            try
            {
                await comboBox.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = timeout
                });

                await comboBox.ClickAsync();

                await page.WaitForTimeoutAsync(1000);

                var option = page.GetByRole(AriaRole.Option, new() { Name = optionText, Exact = true });
                if (await option.CountAsync() == 0)
                {

                    option = page.GetByRole(AriaRole.Option, new() { Name = optionText });
                }

                await option.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Attached,
                    Timeout = timeout
                });

                await page.WaitForTimeoutAsync(500);

                await option.ClickAsync(new LocatorClickOptions { Force = true });
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to select option '{optionText}' in combobox: {ex.Message}", ex);
            }
        }
        protected T GetRandomEnum<T>() where T : struct, System.Enum
        {
            var random = new Random();
            var values = System.Enum.GetValues<T>();
            return values[random.Next(values.Length)];
        }
        protected string GetDisplayName(System.Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DisplayAttribute>();
            return attribute?.Name ?? value.ToString();
        }
    }

}
