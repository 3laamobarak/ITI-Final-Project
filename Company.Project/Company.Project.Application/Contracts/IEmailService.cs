using Company.Project.DTO.DTO.OTPs;

namespace Company.Project.Application.Contracts;

public interface IEmailService
{
    public Task SendEmailAsync(EmailDTO emailDto);
}