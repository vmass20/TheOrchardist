using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheOrchardist.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);

    Task SendEmail(string callbackUrl, Pages.Account.RegisterModel.InputModel input);
    Task SendEmail(string callbackUrl, Pages.Account.LoginModel.InputModel input);
    Task SendEmail(string callbackUrl, Pages.SharedPages.PaymentFormModel.InputModel input);
  }
}
