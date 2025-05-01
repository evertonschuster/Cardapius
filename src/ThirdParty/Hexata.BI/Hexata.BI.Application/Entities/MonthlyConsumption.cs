namespace Hexata.BI.Application.Entities
{
    public record MonthlyConsumption
    {
        public MonthlyConsumption(string id, DateTime month, int total)
        {
            Id = id;
            Month = new DateTime(month.Year, month.Month, day: 1);
            Total = total;
        }

        public string Id { get; set; }

        public DateTime Month { get; set; }

        public int Total { get; set; }
    }
}
