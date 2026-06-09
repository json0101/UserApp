namespace UserApp.Service.Services.Emails
{
    public interface IEmailService
    {
        void Send(string to, string subject, string htmlBody);
        void SendPasswordResetPin(string to, string pin);
    }
}
