using System.ComponentModel.DataAnnotations;

namespace Hexata.BI.Application.DataBaseSyncs.Sales
{
    public enum PaymentStatus
    {
        [DisplayAttribute(Name = "Pendente")]
        Pending,

        [DisplayAttribute(Name = "Pago")]
        Paid,
    }
}
