using System.Collections.Generic;

namespace LabServices.Services
{
    public interface IDataService
    {
        List<string> GetKeys();
        List<string> GetEngineers();
        bool GetTriggerUri(string key, out string result);
        void SetTriggerUri(string key, string uri);
    }
}
