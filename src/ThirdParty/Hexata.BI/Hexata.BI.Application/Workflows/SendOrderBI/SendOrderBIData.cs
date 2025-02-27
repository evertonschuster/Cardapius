using Hexata.BI.Application.Workflows.SendOrderBI.Dtos;

namespace Hexata.BI.Application.Workflows.SendOrderBI
{
    public class SendOrderBIData
    {
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 1000;
        public int Total { get; set; }

        public List<SaleDto> Sales { get; set; }

        public static SendOrderBIData Create()
        {
            return new SendOrderBIData();
        }
    }
}
