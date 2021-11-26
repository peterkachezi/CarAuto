﻿
using CarMatt.Data.DTOs.FeedBackModule;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarMatt.Data.Services.SMSModule
{
   public  class MessagingService : IMessagingService
    {
        private readonly IConfiguration config;

        public MessagingService(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<FeedBackDTO> FeedBackSMSAlert(FeedBackDTO  feedBackDTO)
        {
            try
            {
                var url = "http://167.172.14.50:4002/v1/send-sms";

                var txtMessage = "Dear  " + feedBackDTO.Name + " Welcome to I & P Motors , one of our agents will attend to you shortly. Helpline :+254 729 884 569";

                var key = config.GetValue<string>("SMS_Settings:BongaSMSKey");

                var secrete = config.GetValue<string>("SMS_Settings:BongaSMSSecrete");

                var apiClientID = config.GetValue<string>("SMS_Settings:BongaSMSApiClientID");

                var serviceID = config.GetValue<string>("SMS_Settings:BongaSMSServiceID");

                var msisdn = formatPhoneNumber(feedBackDTO.PhoneNumber);

                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("apiClientID", apiClientID),
                new KeyValuePair<string, string>("secret", secrete),
                new KeyValuePair<string, string>("key", key),
                new KeyValuePair<string, string>("txtMessage", txtMessage),
                new KeyValuePair<string, string>("MSISDN", msisdn),
                new KeyValuePair<string, string>("serviceID", serviceID),
                new KeyValuePair<string, string>("enqueue", "yes"),
            });

                HttpClient client = new HttpClient();

                HttpResponseMessage apiResult = await client.PostAsync(url, formContent);

                apiResult.EnsureSuccessStatusCode();

                var response = await apiResult.Content.ReadAsStringAsync();


                //return new Tuple<bool, TextAlertDTO, string>(true, textAlertDTO, "Text Alert sent successfully!");

                return feedBackDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public string formatPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return string.Empty;

            string formatted = "";

            if (phoneNumber.StartsWith("0"))
                formatted = "+254" + phoneNumber.Substring(1, phoneNumber.Length - 1);

            if (phoneNumber.StartsWith("7"))
                formatted = "+254" + phoneNumber;

            if (phoneNumber.StartsWith("+254"))
                formatted = phoneNumber;

            if (phoneNumber.StartsWith("254"))
                formatted = "+" + phoneNumber;

            return formatted;
        }
    }
}
