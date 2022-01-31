# Demo Blazor Authentication

Demo application developed in Blazor Web Assembly with the help of SQL Server database

## Scaffolding Database

![image](https://user-images.githubusercontent.com/49655304/151779519-fc9ade9a-4236-4bcb-a7ea-69060b8a91ca.png)

## Scaffolding ASP.NET Core Identity

dotnet aspnet-codegenerator identity --files "Account._StatusMessage;Account.AccessDenied;Account.ConfirmEmail;Account.ConfirmEmailChange;Account.ExternalLogin;Account.ForgotPassword;Account.ForgotPasswordConfirmation;Account.Lockout;Account.Login;Account.LoginWith2fa;Account.LoginWithRecoveryCode;Account.Logout;Account.Manage._Layout;Account.Manage._ManageNav;Account.Manage._StatusMessage;Account.Manage.ChangePassword;Account.Manage.DeletePersonalData;Account.Manage.Disable2fa;Account.Manage.DownloadPersonalData;Account.Manage.Email;Account.Manage.EnableAuthenticator;Account.Manage.ExternalLogins;Account.Manage.GenerateRecoveryCodes;Account.Manage.Index;Account.Manage.PersonalData;Account.Manage.ResetAuthenticator;Account.Manage.SetPassword;Account.Manage.ShowRecoveryCodes;Account.Manage.TwoFactorAuthentication;Account.Register;Account.RegisterConfirmation;Account.ResetPassword;Account.ResetPasswordConfirmation" --dbContext DemoBlazorAuthentication.Server.Models.Services.Infrastructure.ApplicationDbContext

## Note

This project was created using the Visual Studio 2022 Community Edition wizard, choosing the "App WebAssembly Blazor" project model, then choosing the .NET 5.0 framework, ticked the 2 flags below: Configure for HTTPS and Hosted ASP.NET Core)

## License

[![MIT License](https://img.shields.io/apm/l/atomic-design-ui.svg?)](https://github.com/tterb/atomic-design-ui/blob/master/LICENSEs)
