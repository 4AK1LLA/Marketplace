namespace Marketplace.IdentityServer.Interfaces;

public interface IEmailAddressValidator
{
    bool IsValid(string emailAddress);
}
