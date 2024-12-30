namespace ClassSchedule.Application.Services
{
    public class TransientService
    {
        public Guid ServiceId { get; }

        public TransientService()
        {
            ServiceId = Guid.NewGuid();
        }

        public void LogScheduleDetails(string message)
        {
            Console.WriteLine($"[TransientService: {ServiceId}] {DateTime.UtcNow}: {message}");
        }

    }
}
