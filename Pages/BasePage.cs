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
        protected async Task SelectComboBoxOptionAsync(ILocator comboBox, string optionText, string? alternativeText = null, bool useClassSelector = false, int timeout = 5000)
        {
            try
            {
                await comboBox.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = timeout
                });

                await comboBox.ClickAsync();

                ILocator option;

                if (useClassSelector && !string.IsNullOrEmpty(alternativeText))
                {
                    option = page.Locator($"[role='option'] .fi.{alternativeText}");
                }
                else if (!string.IsNullOrEmpty(alternativeText))
                {
                    option = page.GetByRole(AriaRole.Option, new() { Name = alternativeText });
                }
                else
                {
                    option = page.GetByRole(AriaRole.Option, new() { Name = optionText });
                }

                await option.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = timeout
                });

                await option.ClickAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to select option '{optionText}' in combobox: {ex.Message}", ex);
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

        protected string GetDisplayDescription(System.Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DisplayAttribute>();
            return attribute?.Description ?? string.Empty;
        }
    }
}
