namespace ClassSchedule.Application.Services
{
    public class AdditionalService
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"[Notification] {message}");
        }
    }
}
