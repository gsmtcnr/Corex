using System.Collections.Generic;

namespace Corex.Push.Derived.OneSignal
{
    public class OneSignalRestResponse
    {
        public OneSignalRestResponse()
        {
            errors = new OneSignalRestResponseErrors();
        }
        public int recipients { get; set; }
        public string id { get; set; }
        public OneSignalRestResponseErrors errors { get; set; }

    }
    public class OneSignalRestResponseErrors
    {
        public OneSignalRestResponseErrors()
        {
            invalid_player_ids = new List<string>();
        }
        public List<string> invalid_player_ids { get; set; }
    }
}
