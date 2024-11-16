using Microsoft.Extensions.Options;
using Talabat.Core.Entities.Twilio;
using Talabat.Core.Interfaces.Services;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Talabat.Services
{
    public class SMSService : ISMSSettings
    {
        private readonly TwilioSettings _settings;

        public SMSService(IOptions<TwilioSettings> settings)
        {
            _settings = settings.Value;
        }
        public MessageResource SendSMS(SMS sMS)
        {
            TwilioClient.Init(_settings.AccountSID, _settings.AuthToken);
            var result = MessageResource.Create(
                body : sMS.body,
                from : new Twilio.Types.PhoneNumber(_settings.TwilioPhoneNumber),
                to: sMS.mobileNumber
                );
            return result;
        }
    }
}
