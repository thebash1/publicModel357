using Model357App.Model;
using Model357App.View;
using System;

namespace Model357App.Controller
{
    public class AuthController
    {
        private readonly ILoginView view;
        private readonly UserAuth userAuth;
        private string currentUsername;

        public AuthController(ILoginView view)
        {
            this.view = view;
            userAuth = new UserAuth();
            
            // Wire up view events
            this.view.LoginClick += OnLoginClick;
            this.view.CancelClick += OnCancelClick;
        }

        private void OnLoginClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(view.Username) || string.IsNullOrEmpty(view.Password))
            {
                view.ShowError("Username and password are required.");
                return;
            }

            if (userAuth.ValidateCredentials(view.Username, view.Password))
            {
                currentUsername = view.Username;
                view.ShowMessage("Login successful!");
                view.CloseView();
            }
            else
            {
                view.ShowError("Invalid credentials.");
            }
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            view.CloseView();
        }

        public bool IsSessionActive() => !string.IsNullOrEmpty(currentUsername);
        
        public string GetCurrentUser() => currentUsername;
        
        public void CloseSession() => currentUsername = null;
    }
}