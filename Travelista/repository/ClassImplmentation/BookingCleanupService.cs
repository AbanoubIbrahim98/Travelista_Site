namespace Travelista.repository.ClassImplmentation
{
    public class BookingCleanupService : IHostedService, IDisposable

    {
        private readonly BookingSerivce _bookingService;
        private Timer _timer;

        public BookingCleanupService(BookingSerivce bookingService)
        {
            _bookingService = bookingService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _bookingService.DeleteFinishedBookings();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

