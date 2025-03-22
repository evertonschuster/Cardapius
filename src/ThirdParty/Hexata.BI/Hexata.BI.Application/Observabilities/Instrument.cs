using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Hexata.BI.Application.Observabilities
{
    public class Instrument : IDisposable
    {
        private bool _disposed = false;
        private ActivitySource Tracer { get; }

        public Counter<int> RequestGoogleGeocodeCount { get; }
        public Counter<int> RequestGoogleGeocodeFailCount { get; }
        public Counter<int> RequestGoogleGeocodeSuccessCount { get; }

        public Counter<int> RequestNominatimGeocodeCount { get; }
        public Counter<int> RequestNominatimGeocodeFailCount { get; }
        public Counter<int> RequestNominatimGeocodeSuccessCount { get; }

        public Instrument(IMeterFactory meterFactory, ActivitySource tracer)
        {
            Tracer = tracer;
            
            var meter = meterFactory.Create("Hexata.SendOrderBI");
            RequestGoogleGeocodeCount = meter.CreateCounter<int>("RequestGoogleGeocode", "Request to Google Geocode API");
            RequestGoogleGeocodeFailCount = meter.CreateCounter<int>("RequestGoogleGeocodeFail", "Failed request to Google Geocode API");
            RequestGoogleGeocodeSuccessCount = meter.CreateCounter<int>("RequestGoogleGeocodeSuccess", "Successful request to Google Geocode API");

            RequestNominatimGeocodeCount = meter.CreateCounter<int>("RequestNominatimGeocode", "Request to Nominatim Geocode API");
            RequestNominatimGeocodeFailCount = meter.CreateCounter<int>("RequestNominatimGeocodeFail", "Failed request to Nominatim Geocode API");
            RequestNominatimGeocodeSuccessCount = meter.CreateCounter<int>("RequestNominatimGeocodeSuccess", "Successful request to Nominatim Geocode API");
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
