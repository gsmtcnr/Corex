using Corex.Push.Infrastructure;
using Corex.Utility.Infrastructure;
using OneSignal.RestAPIv3.Client;
using OneSignal.RestAPIv3.Client.Resources;
using OneSignal.RestAPIv3.Client.Resources.Notifications;
using RestSharp.Serializers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Corex.Push.Derived.OneSignal
{
    public abstract class BaseOneSignalPushSender : IPushSender
    {
        public abstract OneSignalInformation CreateInformation();

        public IPushOutput Send(IPushInput input)
        {
            OneSignalOutPut oneSignalOutPut = new OneSignalOutPut();
            OneSignalInformation information = CreateInformation();
            OneSignalClient client = new OneSignalClient(information.ApiKey);
            NotificationCreateOptions options = new NotificationCreateOptions
            {
                AppId = new Guid(information.AppId)
            };
            if (!string.IsNullOrEmpty(input.SubTitle))
                options.Subtitle.Add(LanguageCodes.English, input.SubTitle);
            options.Priority = input.Priority;
            options.Headings.Add(LanguageCodes.English, input.Title);
            options.IncludePlayerIds.Add(input.To);
            options.Contents.Add(LanguageCodes.English, input.Message);
            NotificationCreateResult result = client.Notifications.Create(options);
            if (result.Recipients == 3)
                oneSignalOutPut.IsSuccess = true;
            oneSignalOutPut.Message = $"{result.Id}-{result.Recipients}";
            return oneSignalOutPut;
        }
        public async Task<IPushOutput> SendAsync(IPushInput input)
        {
            OneSignalInformation information = CreateInformation();
            RestUtility<OneSignalRestResponse> restUtility = new RestUtility<OneSignalRestResponse>(information.ApiUrl, method: "POST");
            OneSignalOutPut oneSignalOutPut = new OneSignalOutPut();
            OneSignalRequest request = new OneSignalRequest
            {
                priority = input.Priority,
                app_id = information.AppId
            };
            request.AddPlayer(input.To);
            request.AddHeading("en", input.Title);
            request.AddContent("en", input.Message);
            if (!string.IsNullOrEmpty(input.Payload))
                request.AddData("data", input.Payload);
            if (!string.IsNullOrEmpty(input.SubTitle))
                request.AddSubTitle("en", input.SubTitle);
            JsonSerializer jsonSerializer = new JsonSerializer();
            string requestSerialize = jsonSerializer.Serialize(request);
            OneSignalRestResponse result = await restUtility.CallAsync(request, information.ApiKey);
            if (result.recipients == 1)
                oneSignalOutPut.IsSuccess = true;
            else
            {
                string resultMessageFormat = "id:{0}-recipients:{1}-invalid_player_ids:{2}";
                string errors = string.Join(",", result.errors.invalid_player_ids.Select(s => s));
                string resultMessage = string.Format(resultMessageFormat, result.id, result.recipients, errors);
                oneSignalOutPut.Message = resultMessage;
            }
            return oneSignalOutPut;
        }
    }
}
