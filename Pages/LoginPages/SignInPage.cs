using dev_repmove_autotest.Shared.Validaton;
using Microsoft.Playwright;

namespace dev_repmove_autotest.Pages.LoginPages
{
    internal class SignInPage : BasePage
    {
        private readonly ILocator signUpButton;
        private readonly ILocator emailInput;
        private readonly ILocator passwordInput;
        private readonly ILocator signInButton;
        private readonly ILocator forgotPassButton;

        private readonly ILocator invalidEmailError;
        private readonly ILocator requiredEmailError;
        private readonly ILocator requiredPasswordError;
        private readonly ILocator invalidLogin;
        public SignInPage(IPage page) : base(page)
        {
            signUpButton = page.GetByRole(AriaRole.Button, new() { Name = "Sign Up Now" });
            emailInput = page.Locator("input[type=\"email\"]");
            passwordInput = page.Locator("input[type=\"password\"]");
            signInButton = page.GetByRole(AriaRole.Button, new() { Name = "Sign In", Exact = true });
            forgotPassButton = page.GetByRole(AriaRole.Button, new() { Name = "Forgot password" });

            #region ErrorValidation
            invalidEmailError = page.GetByText(LoginErrorMessages.InvalidEmail);
            requiredEmailError = page.GetByText(LoginErrorMessages.RequiredEmail);
            requiredPasswordError = page.GetByText(LoginErrorMessages.RequiredPassword);
            invalidLogin = page.GetByRole(AriaRole.Alert, new() { Name = LoginErrorMessages.InvalidLogin });
            #endregion
        }
        public async Task FillEmail(string email)
        {
            await emailInput.FillAsync(email);
        }
        public async Task FillPassword(string password)
        {
            await passwordInput.FillAsync(password);
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
        public async Task<bool> IsInvalidEmailErrorVisible()
        {
            return await invalidEmailError.IsVisibleAsync();
        }
        public async Task<bool> IsRequiredEmailErrorVisible()
        {
            return await requiredEmailError.IsVisibleAsync();
        }
        public async Task<bool> IsRequiredPasswordErrorVisible()
        {
            return await requiredPasswordError.IsVisibleAsync();
        }
        public async Task<bool> IsInvalidLoginErrorVisible()
        {
            try
            {
                await invalidLogin.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 1000 });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
