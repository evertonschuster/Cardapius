using BuildingBlock.Observability.Traces;
using Elastic.Apm.Api;

namespace BuildingBlock.Observability.ElasticStack.Traces
{
    internal class ElasticTracerSpan : ITracerSpan
    {
        private readonly ISpan span;

        public ElasticTracerSpan(ISpan span)
        {
            this.span = span;
        }

        public void End()
        {
            this.span.End();
        }
    }
}
