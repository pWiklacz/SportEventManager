using System.Threading.Tasks;

namespace SportEventManager.Core.Interfaces;

public interface IEmailSender
{
  Task SendEmailAsync(string to, string from, string subject, string body);
}
