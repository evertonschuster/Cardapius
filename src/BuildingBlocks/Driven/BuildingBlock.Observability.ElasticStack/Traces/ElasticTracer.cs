using BuildingBlock.Observability.Traces;

namespace BuildingBlock.Observability.ElasticStack.Traces
{
    ////https://www.elastic.co/guide/en/apm/agent/dotnet/current/public-api.html
    internal class ElasticTracer : ITracer
    {
        public void SetLabel<T>(string key, T value)
        {
            var currentTransaction = Elastic.Apm.Agent.Tracer.CurrentTransaction;

            if (value is string valueString)
            {
                currentTransaction.SetLabel(key, valueString);
                return;
            }
            if (value is bool valueBool)
            {
                currentTransaction.SetLabel(key, valueBool);
                return;
            }
            if (value is double valueDouble)
            {
                currentTransaction.SetLabel(key, valueDouble);
                return;
            }
            if (value is int valueInt)
            {
                currentTransaction.SetLabel(key, valueInt);
                return;
            }
            if (value is long valueLong)
            {
                currentTransaction.SetLabel(key, valueLong);
                return;
            }
            if (value is decimal valueDecimal)
            {
                currentTransaction.SetLabel(key, valueDecimal);
                return;
            }

            currentTransaction.SetLabel(key, value?.ToString());
        }

        public ITracerSpan StartSpan(string name, string type, string? subType = null, string? action = null)
        {
            var currentTransaction = Elastic.Apm.Agent.Tracer.CurrentTransaction;
            var span = currentTransaction.StartSpan(name, type, subType, action);
            return new ElasticTracerSpan(span);
        }
    }
}
