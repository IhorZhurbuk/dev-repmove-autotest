using Microsoft.Playwright;

namespace dev_repmove_autotest.Pages.LoginPage
{
    internal class SignInPage : BasePage
    {
        private readonly ILocator signUpButton;

        private readonly ILocator emailInput;
        private readonly ILocator passwordInput;
        private readonly ILocator signInButton;

        private readonly ILocator forgotPassButton;
        public SignInPage(IPage page) : base(page)
        {
            signUpButton = page.GetByRole(AriaRole.Button, new() { Name = "Sign Up Now" });

            emailInput = page.Locator("input[type=\"email\"]");
            passwordInput = page.Locator("input[type=\"password\"]");
            signInButton = page.GetByRole(AriaRole.Button, new() { Name = "Sign In", Exact = true });

            forgotPassButton = page.GetByRole(AriaRole.Button, new() { Name = "Forgot password" });
        }
        public async Task SignIn(string email, string password)
        {
            await emailInput.FillAsync(email);
            await passwordInput.FillAsync(password);
            await signInButton.ClickAsync();
        }
        public async Task ClickSignUp()
        {
            await signUpButton.ClickAsync();
        }
        public async Task ClickForgotPass()
        {
            await forgotPassButton.ClickAsync();
        }
    }
}
