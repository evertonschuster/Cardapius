namespace BuildingBlock.Observability.Exceptions
{
    public class EnvironmentNotSetException : Exception
    {
        public EnvironmentNotSetException() : base("Environment Not Set.")
        {
        }
    }
}
