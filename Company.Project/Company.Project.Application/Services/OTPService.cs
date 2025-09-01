using Company.Project.Application.Contracts;
using Company.Project.Domain.Interfaces;
using Company.Project.Domain.Models;
using Company.Project.DTO.DTO.OTPs;

namespace Company.Project.Application.Services
{
    public class OTPService : IOTPService
    {
        private readonly IOTPRepository _otpRepository;
        private readonly IEmailService _emailService;
        
        public OTPService(IOTPRepository otpRepository, IEmailService emailService)
        {
            _otpRepository = otpRepository;
            _emailService = emailService;
        }
        public async Task<string> GenerateAndSendOTPAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }
            var otp= new Random().Next(100000, 999999).ToString();
            var otpEntity = new OTP
            {
                Code = otp,
                Email = email,
                ExpirationTime = DateTime.UtcNow.AddMinutes(5),
                IsUsed = false
            };
            await _otpRepository.AddAsync(otpEntity);
            
            var emailDto = new EmailDTO()
            {
                To = email,
                Subject = "Email Verification ",
                //Body = $"<h1>Email Verification Code</h1><p>Your verification code is: <strong>{otp}</strong></p><p>This code will expire in 5 minutes.</p>"
                Body = $@"
<!DOCTYPE html>
<html lang=""en"">
  <head>
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"" />
    <title>Static Template</title>

    <link
      href=""https://fonts.googleapis.com/css2?family=Poppins:wght@300;400;500;600&display=swap""
      rel=""stylesheet""
    />
  </head>
  <body
    style=""
      margin: 0;
      font-family: 'Poppins', sans-serif;
      background: #ffffff;
      font-size: 14px;
    ""
  >
    <div
      style=""
        max-width: 680px;
        margin: 0 auto;
        padding: 45px 30px 60px;
        background: #f4f7ff;
        background-image: url(https://archisketch-resources.s3.ap-northeast-2.amazonaws.com/vrstyler/1661497957196_595865/email-template-background-banner);
        background-repeat: no-repeat;
        background-size: 800px 452px;
        background-position: top center;
        font-size: 14px;
        color: #434343;
      ""
    >
      <header>
        <table style=""width: 100%;"">
          <tbody>
            <tr style=""height: 0;"">
              <td>
              </td>
              <td style=""text-align: right;"">
              </td>
            </tr>
          </tbody>
        </table>
      </header>

      <main>
        <div
          style=""
            margin: 0;
            margin-top: 70px;
            padding: 92px 30px 115px;
            background: #ffffff;
            border-radius: 30px;
            text-align: center;
          ""
        >
          <div style=""width: 100%; max-width: 489px; margin: 0 auto;"">
            <h1
              style=""
                margin: 0;
                font-size: 24px;
                font-weight: 500;
                color: #1f1f1f;
              ""
            >
              Your OTP
            </h1>
            <p
              style=""
                margin: 0;
                margin-top: 17px;
                font-size: 16px;
                font-weight: 500;
              ""
            >
              Hey There,
            </p>
            <p
              style=""
                margin: 0;
                margin-top: 17px;
                font-weight: 500;
                letter-spacing: 0.56px;
              ""
            >
              Thank you for choosing IHerb Company. Use the following OTP
              to complete the procedure to Verify your email address. OTP is
              valid for
              <span style=""font-weight: 600; color: #1f1f1f;"">5 minutes</span>.
              Do not share this code with others, including Iherb
              employees.
            </p>
            <p
              style=""
                margin: 0;
                margin-top: 60px;
                font-size: 40px;
                font-weight: 600;
                letter-spacing: 25px;
                color: #ba3d4f;
              ""
            >
              {otp}
            </p>
          </div>
        </div>

        <p
          style=""
            max-width: 400px;
            margin: 0 auto;
            margin-top: 90px;
            text-align: center;
            font-weight: 500;
            color: #8c8c8c;
          ""
        >
          Need help? Ask at
          <a
            href=""3laa.m0o0barak@gmail.com""
            style=""color: #499fb6; text-decoration: none;""
            >3laa.m0o0barak@gmail.com</a
          >
          or visit our
          <a
            href=""""
            target=""_blank""
            style=""color: #499fb6; text-decoration: none;""
            >Help Center</a
          >
        </p>
      </main>

      <footer
        style=""
          width: 100%;
          max-width: 490px;
          margin: 20px auto 0;
          text-align: center;
          border-top: 1px solid #e6ebf1;
        ""
      >
        <p
          style=""
            margin: 0;
            margin-top: 40px;
            font-size: 16px;
            font-weight: 600;
            color: #434343;
          ""
        >
          IHerb Company
        </p>
        <p style=""margin: 0; margin-top: 8px; color: #434343;"">
          ElNsagoon 540, Qena, Egypt.
        </p>
        <div style=""margin: 0; margin-top: 16px;"">
          <a href="""" target=""_blank"" style=""display: inline-block;"">
            <img
              width=""36px""
              alt=""Facebook""
              src=""https://archisketch-resources.s3.ap-northeast-2.amazonaws.com/vrstyler/1661502815169_682499/email-template-icon-facebook""
            />
          </a>
          <a
            href=""""
            target=""_blank""
            style=""display: inline-block; margin-left: 8px;""
          >
            <img
              width=""36px""
              alt=""Instagram""
              src=""https://archisketch-resources.s3.ap-northeast-2.amazonaws.com/vrstyler/1661504218208_684135/email-template-icon-instagram""
          /></a>
          <a
            href=""""
            target=""_blank""
            style=""display: inline-block; margin-left: 8px;""
          >
            <img
              width=""36px""
              alt=""Twitter""
              src=""https://archisketch-resources.s3.ap-northeast-2.amazonaws.com/vrstyler/1661503043040_372004/email-template-icon-twitter""
            />
          </a>
          <a
            href=""""
            target=""_blank""
            style=""display: inline-block; margin-left: 8px;""
          >
            <img
              width=""36px""
              alt=""Youtube""
              src=""https://archisketch-resources.s3.ap-northeast-2.amazonaws.com/vrstyler/1661503195931_210869/email-template-icon-youtube""
          /></a>
        </div>
        <p style=""margin: 0; margin-top: 16px; color: #434343;"">
          Copyright © 2022 Company. All rights reserved.
        </p>
      </footer>
    </div>
  </body>
</html>"
                
            };
            await _emailService.SendEmailAsync(emailDto);
            return otp;
        }

        public async Task<bool> ValidateOTPAsync(string email, string otp)
        {
            var otpEntity = await _otpRepository.GetByEmailAsync(email);
            if (otpEntity == null || otpEntity.IsUsed || otpEntity.ExpirationTime < DateTime.UtcNow)
            {
                return false; // OTP not found, used, or expired
            }
            if (otpEntity.Code != otp)
            {
                return false; // OTP does not match
            }
            
            otpEntity.IsUsed = true;
            await _otpRepository.UpdateAsync(otpEntity);
            return true;

        }
        
        
    }
}
