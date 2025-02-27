using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Hexata.BI.Application.Observabilities
{
    public class Instrument : IDisposable
    {
        private bool _disposed = false;
        private ActivitySource Tracer { get; }

        public Instrument(IMeterFactory meterFactory, ActivitySource tracer)
        {
            Tracer = tracer;
            var meter = meterFactory.Create("Hexata.SendOrderBI");
        }

        public Activity? ExecuteDataBaseQuery()
        {
            return Tracer.StartActivity("ExecuteDataBaseQuery");
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Tracer.Dispose();
                }

                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
