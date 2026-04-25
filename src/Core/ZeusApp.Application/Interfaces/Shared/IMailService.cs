using System.Threading.Tasks;
using ZeusApp.Application.DTOs.Mail;

namespace ZeusApp.Application.Interfaces.Shared;

public interface IMailService
{
    Task SendAsync(MailRequest request);
}
