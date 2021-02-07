using System.Collections.Generic;

namespace Corex.Push.Derived.OneSignal
{
    public class OneSignalRequest
    {

        public OneSignalRequest()
        {
            include_player_ids = new List<string>();
            headings = new Dictionary<string, string>();
            contents = new Dictionary<string, string>();
            data = new Dictionary<string, string>();
            subtitle = new Dictionary<string, string>();
        }
        public string app_id { get; set; }
        public List<string> include_player_ids { get; set; }
        public Dictionary<string, string> headings { get; set; }
        public Dictionary<string, string> contents { get; set; }
        public Dictionary<string, string> subtitle { get; set; }
        public Dictionary<string, string> data { get; set; }

        public byte priority { get; set; }
        public void AddPlayer(string id)
        {
            include_player_ids.Add(id);
        }
        public void AddHeading(string langCode, string item)
        {
            headings.Add(langCode, item);
        }
        public void AddContent(string langCode, string item)
        {
            contents.Add(langCode, item);
        }
        public void AddData(string code, string item)
        {
            data.Add(code, item);
        }
        public void AddSubTitle(string langCode, string item)
        {
            subtitle.Add(langCode, item);
        }
    }
}
