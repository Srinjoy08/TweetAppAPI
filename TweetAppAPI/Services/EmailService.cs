﻿using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TweetAppAPI.Models;

namespace TweetAppAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly SMTPConfig _smtpConfig;
        
        public EmailService(IOptions<SMTPConfig> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }

        public void SendEmail(string email, string firstName, string otp)
        {
            MailAddress sender = new MailAddress(_smtpConfig.SenderEmail, _smtpConfig.SenderDisplayName);
            MailAddress receiver = new MailAddress(email);

            MailMessage mailMessage = new MailMessage(sender, receiver)
            {
                Subject = "OTP for Password Reset",
                Body = string.Format("Hi {0},\nYour One Time Password for Password Reset is : {1}", firstName, otp),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.Username, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mailMessage.BodyEncoding = Encoding.Default;
            smtpClient.SendMailAsync(mailMessage);
        }
    }
}
