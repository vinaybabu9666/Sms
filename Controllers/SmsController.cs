using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Collections.Generic;

namespace SmsSenderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        // POST api/sms/send
        [HttpPost("send")]
        public IActionResult SendSms([FromBody] SmsRequest smsRequest)
        {
            try
            {
                // Twilio credentials
                const string accountSid = "AC554caa59ce66d6d4e10f2eb29446cc23"; // Your Twilio Account SID
                const string authToken = "e83a72edeeb6477d418b68110c55caa0"; // Your Twilio Auth Token

                // Initialize Twilio client
                TwilioClient.Init(accountSid, authToken);

                // Iterate over the people to send SMS
                foreach (var person in smsRequest.People)
                {
                    // Send SMS
                    MessageResource.Create(
                        from: new PhoneNumber("+13158600206"), // Your Twilio phone number
                        to: new PhoneNumber(person.Key),       // Recipient's phone number
                        body: $"Hey {person.Value}, {smsRequest.Message}"); // Message content
                }

                return Ok("SMS sent successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    // Model to capture the SMS request
    public class SmsRequest
    {
        public string Message { get; set; } // The message content
        public Dictionary<string, string> People { get; set; } // Dictionary of phone numbers and names
    }
}
