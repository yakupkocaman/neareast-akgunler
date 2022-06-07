using Newtonsoft.Json;

namespace Akgunler.Models
{
    public class Error
    {
        public int Code { get; set; }

        public string Key { get; set; }

        public string Message { get; set; }

        public Error() { }

        public Error(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
