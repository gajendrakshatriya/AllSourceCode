using Azure.Core;
using Microsoft.Azure.NotificationHubs.Messaging;
using Microsoft.Azure.NotificationHubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AzureOperationsLib.NotificationBO
{
    public class RegisterDevice
    {

        private NotificationHubClient hub;

        public RegisterDevice()
        {
            hub = Notifications.Instance.Hub;
        }

       
        // POST api/register
        // This creates a registration id
        public async Task<RegistrationDescription?> Post(string handle, string deviceType, string username)
        {
            string newRegistrationId = null;

            // make sure there are no existing registrations for this push handle (used for iOS and Android)
            if (handle != null)
            {
                var registrations = await hub.GetRegistrationsByChannelAsync(handle, 100);

                foreach (RegistrationDescription registration in registrations)
                {
                    if (newRegistrationId == null)
                    {
                        newRegistrationId = registration.RegistrationId;
                    }
                    else
                    {
                        await hub.DeleteRegistrationAsync(registration);
                    }
                }
            }

            if (newRegistrationId == null)
                newRegistrationId = await hub.CreateRegistrationIdAsync();

            var result = await CreateOrUpdateRegistrationAsync(username, newRegistrationId, deviceType);
            return result;
            //return newRegistrationId;
        }

        private async Task<RegistrationDescription?> CreateOrUpdateRegistrationAsync(string username,string newRegistrationId, string deviceType)
        {
            RegistrationDescription registrationDescription = null;
            switch (deviceType)
            {
                case PNSDeviceType.Apple:
                    registrationDescription = new AppleRegistrationDescription(username);
                    break;
                case PNSDeviceType.Android:
                    registrationDescription = new FcmRegistrationDescription(username);
                    break;
                case PNSDeviceType.Windows:
                    registrationDescription = new WindowsRegistrationDescription(username);
                    break;
                default:
                    break;
            }

            if (registrationDescription != null)
            {
                registrationDescription.RegistrationId = newRegistrationId;
                registrationDescription.Tags = new HashSet<string>() { username, deviceType };
            }
            var response = await hub.CreateOrUpdateRegistrationAsync(registrationDescription);
            return response;
        }

        // PUT api/register/5
        // This creates or updates a registration (with provided channelURI) at the specified id
        public async Task<HttpResponseMessage> Put(string id, DeviceRegistration deviceUpdate, string usernameToTag)
        {
            RegistrationDescription registration = null;
            switch (deviceUpdate.Platform)
            {
                case "mpns":
                    registration = new MpnsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "wns":
                    registration = new WindowsRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "apns":
                    registration = new AppleRegistrationDescription(deviceUpdate.Handle);
                    break;
                case "fcm":
                    registration = new FcmRegistrationDescription(deviceUpdate.Handle);
                    break;
                default:
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            registration.RegistrationId = id;
            var username = usernameToTag;// HttpContext.Current.User.Identity.Name;

            // add check if user is allowed to add these tags
            registration.Tags = new HashSet<string>(deviceUpdate.Tags);
            registration.Tags.Add("username:" + username);

            try
            {
                await hub.CreateOrUpdateRegistrationAsync(registration);
            }
            catch (MessagingException e)
            {
                ReturnGoneIfHubResponseIsGone(e);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);// Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/register/5
        public async Task<HttpResponseMessage> Delete(string id)
        {
            await hub.DeleteRegistrationAsync(id);
            return new HttpResponseMessage(HttpStatusCode.OK);// Request.CreateResponse(HttpStatusCode.OK);
        }

        private static void ReturnGoneIfHubResponseIsGone(MessagingException e)
        {
            var webex = e.InnerException as WebException;
            if (webex.Status == WebExceptionStatus.ProtocolError)
            {
                var response = (HttpWebResponse)webex.Response;
                if (response.StatusCode == HttpStatusCode.Gone)
                    throw new HttpRequestException(HttpStatusCode.Gone.ToString());
            }
        }
    }

    
}
