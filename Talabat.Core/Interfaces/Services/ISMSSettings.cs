using Talabat.Core.Entities.Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Talabat.Core.Interfaces.Services
{
    public interface ISMSSettings
    {
        MessageResource SendSMS(SMS sMS);
    }
}
