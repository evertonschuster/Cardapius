namespace BuildingBlock.Observability.Traces
{
    public interface ITracer
    {
        ITracerSpan StartSpan(string name, string type, string? subType = null, string? action = null);

        void SetLabel<T>(string key, T value);
    }
}
