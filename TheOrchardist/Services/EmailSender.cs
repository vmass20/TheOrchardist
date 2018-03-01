using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;
using Newtonsoft.Json;
using System.Text;

namespace TheOrchardist.Services
{
  // This class is used by the application to send email for account confirmation and password reset.
  // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
  public class EmailSender : IEmailSender
  {
    public Task SendEmailAsync(string email, string subject, string message)
    {
      return Task.CompletedTask;
    }
    public async Task SendEmail(string callbackUrl, Pages.Account.RegisterModel.InputModel input)
    {
      // Execute(callbackUrl, input);
      await ExecuteEmail(callbackUrl, input, null,null, null, null);
    }

    public async Task SendEmail(string callbackUrl, Pages.SharedPages.PaymentFormModel.InputModel input)
    {
      // Execute(callbackUrl, input);
      await ExecuteEmail(callbackUrl, null,null, input, null, null);
    }
    public async Task SendEmail(string callbackUrl, Pages.Account.LoginModel.InputModel input)
    {
      // Execute(callbackUrl, input);
      await ExecuteEmail(callbackUrl, null, input, null, null, null);
    }
    //public static void SendEmail(string callbackUrl, Pages.Account.Manage.IndexModel.InputModel input)
    //{
    //  // Execute(callbackUrl, input);
    //  ExecuteEmail(callbackUrl, null, null, input, null).Wait();
    //}
    //public static void SendEmail(string callbackUrl, Pages.Account.ExternalLoginModel.InputModel input)
    //{
    //  // Execute(callbackUrl, input);
    //  ExecuteEmail(callbackUrl, null, null, null, input).Wait();
    //}
    static async Task ExecuteEmail(string callbackUrl, Pages.Account.RegisterModel.InputModel registerInput, Pages.Account.LoginModel.InputModel loginInput, Pages.SharedPages.PaymentFormModel.InputModel paymentFormInput , Pages.Account.Manage.IndexModel.InputModel ManageIndexInput,
                                      Pages.Account.ExternalLoginModel.InputModel externalLoginsInput)
    {
      var sb = new StringBuilder("<body style='margin: 0px;'>");
      sb.AppendFormat("<div> Confirm your account by <a href='{0}'>clicking here.</a> Happy Orcharding!</div>", callbackUrl);
      sb.Append("</body>");
      var content = sb.ToString();
      String apiKey = Startup.Configuration["AppSettings:APIKey"].ToString();

      var client = new SendGridClient(apiKey);

      var from = new EmailAddress("vmass20@comcast.net", "Orchadist Admin");

      var subject = "Confirmation Email - Orchardist";

      var to = new EmailAddress();

      if (registerInput != null)
      {
        to = new EmailAddress(registerInput.Email, "Orchardist User");
      }
      else if (loginInput != null)
      {
        to = new EmailAddress(loginInput.Email, "Orchardist User");
      }
      else if (paymentFormInput != null)
      {
        to = new EmailAddress(paymentFormInput.Email, "Orchardist User");
      }
      else if (ManageIndexInput != null)
      {
        to = new EmailAddress(ManageIndexInput.Email, "Orchardist User");
      }
      else if (externalLoginsInput != null)
      {
        to = new EmailAddress(externalLoginsInput.Email, "Orchardist User");
      }

      var plainTextContent = "Orchardists are aweseome!";

      var htmlContent = "<strong>" + content + "</strong>";

      var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

      var response = await client.SendEmailAsync(msg);
    }
  }
}
