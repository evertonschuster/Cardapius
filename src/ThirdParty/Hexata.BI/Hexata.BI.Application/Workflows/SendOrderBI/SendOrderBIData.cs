using Hexata.BI.Application.Workflows.SendOrderBI.Dtos;
using Hexata.BI.Application.Workflows.SendOrderBI.Models;

namespace Hexata.BI.Application.Workflows.SendOrderBI
{
    public class SendOrderBIData
    {
        public int Page { get; set; } = 11;
        public int PageSize { get; set; } = 100;
        public int Total { get; set; }

        public List<SaleDto> Sales { get; set; }

        public Order Order { get; set; }

        public List<Order> Orders { get; set; } = [];

        public static SendOrderBIData Create()
        {
            return new SendOrderBIData();
        }
    }
}
