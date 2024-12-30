namespace ClassSchedule.Application.Services
{
    namespace ClassSchedule.Application.Services
    {
        public class SingletonService
        {
            public Guid ServiceId { get; }

            public SingletonService()
            {
                ServiceId = Guid.NewGuid();
            }

            public void LogGlobalActivity(string message)
            {
                Console.WriteLine($"[SingletonService: {ServiceId}] {DateTime.UtcNow}: {message}");
            }
        }
    }
}
