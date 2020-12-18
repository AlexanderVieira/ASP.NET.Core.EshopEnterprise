namespace ESE.WebAPI.Core.Auth
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpirationTime { get; set; }
        public string Emitter { get; set; }
        public string ValidIn { get; set; }
    }
}
