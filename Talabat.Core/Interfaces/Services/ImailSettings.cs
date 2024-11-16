using Talabat.Core.Entities.EmailSettings;

namespace Talabat.Core.Interfaces.Services
{
    public interface ImailSettings
    {
        public void SendMail(Email email);
    }
}
