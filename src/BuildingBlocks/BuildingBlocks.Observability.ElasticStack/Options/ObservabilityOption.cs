namespace BuildingBlocks.Observability.ElasticStack.Options
{
    public class ObservabilityOption
    {
        public ObservabilityElasticOption Elastic { get; set; } = new ObservabilityElasticOption();
    }

    public class ObservabilityElasticOption
    {
        public string URI { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Enabled { get; set; } = true;
    }
}
