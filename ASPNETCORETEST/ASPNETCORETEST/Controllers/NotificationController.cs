using Azure;
using AzureOperationsLib.NotificationBO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.NotificationHubs;

namespace ASPNETCORETEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        RegisterDevice _registerDevice;
        public NotificationController()
        {
            _registerDevice = new RegisterDevice(); ;
        }

        [HttpPost("Send")]
        public async Task<HttpResponseMessage> Send(string taggedUserName, string pns, string message, string to_tag)
        {
            var response = await AzureOperationsLib.NotificationBO.Notifications.Instance.SendNotificationAsync(taggedUserName, pns, message, to_tag);
            return response;
        }

        [HttpPost("RegisterDevice")]
        public async Task<RegistrationDescription?> RegisterDevice(string handle,string devicetype,string username)
        {
            var response = await _registerDevice.Post(handle, devicetype, username);
            return response;
        }

    }
}
