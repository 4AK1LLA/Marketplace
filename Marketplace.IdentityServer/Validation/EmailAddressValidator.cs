using Marketplace.IdentityServer.Interfaces;
using System.Net.Mail;

namespace Marketplace.IdentityServer.Validation;

internal class EmailAddressValidator : IEmailAddressValidator
{
    public bool IsValid(string emailAddress)
    {
        try
        {
            var validAddress = new MailAddress(emailAddress);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
