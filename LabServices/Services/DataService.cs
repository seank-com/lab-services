using System.Collections.Concurrent;
using System.Collections.Generic;

namespace LabServices.Services
{
    public class DataService : IDataService
    {
        List<string> _keys;
        List<string> _engineers;
        ConcurrentDictionary<string, string> _triggers;

        public DataService()
        {
            _keys = new List<string>()
        {
            "1F7E2660-8D21-41AF-8C5B-CF565498BE20",
            "A948FCB3-6E27-4AF2-A930-37B4AC3029CC"
        };

            _engineers = new List<string>()
        {
            "BARRON",
            "CONNOR",
            "MIKE",
            "SEAN",
            "WENDY"
        };

            _triggers = new ConcurrentDictionary<string, string>();

            _keys.ForEach((key) =>
            {
                _triggers.TryAdd(key, "");
            });
        }

        public List<string> GetKeys()
        {
            return _keys;
        }

        public List<string> GetEngineers()
        {
            return _engineers;
        }

        public bool GetTriggerUri(string key, out string result)
        {
            return _triggers.TryGetValue(key.ToUpper(), out result);
        }

        public void SetTriggerUri(string key, string uri)
        {
            if (_keys.Contains(key.ToUpper()))
            {
                _triggers.AddOrUpdate(key.ToUpper(), uri, (_, oldValue) => uri);
            }
        }
    }
}
