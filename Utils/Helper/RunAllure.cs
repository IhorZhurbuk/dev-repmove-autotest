using Allure.Net.Commons;
using Microsoft.Playwright;

namespace dev_repmove_autotest.Utils.Helper
{
    public static class RunAllure
    {
        public static async Task RunAllureStepAsync(string stepName, Func<Task> stepAction)
        {
            var stepResult = new StepResult { name = stepName };
            AllureLifecycle.Instance.StartStep(stepResult);

            try
            {
                await stepAction();

                AllureLifecycle.Instance.UpdateStep(s => s.status = Status.passed);
            }
            catch (Exception ex)
            {
                AllureLifecycle.Instance.UpdateStep(s =>
                {
                    s.status = Status.failed;
                    s.statusDetails = new StatusDetails
                    {
                        message = ex.Message,
                        trace = ex.StackTrace ?? string.Empty
                    };
                });
                throw;
            }
            finally
            {
                AllureLifecycle.Instance.StopStep();
            }
        }

        public static async Task AttachScreenshotAsync(IPage page, string fileNameWithoutExt)
        {
            string screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "screenshots");

            if (!Directory.Exists(screenshotsDir))
                Directory.CreateDirectory(screenshotsDir);

            string fullPath = Path.Combine(screenshotsDir, $"{fileNameWithoutExt}.png");

            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = fullPath,
                FullPage = true
            });

            AllureApi.AddAttachment(
                name: fileNameWithoutExt,
                type: "image/png",
                fullPath
            );
        }
    }
}
