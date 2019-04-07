namespace Core_Service_MiddleWare.Service
{
    public interface IWelcomService
    {
        string WelcomeMessage();
    }
    public class WelcomService : IWelcomService
    {
        public string WelcomeMessage()
        {
            return "Hello!!!     __From WelcomService";
        }
    }
}
