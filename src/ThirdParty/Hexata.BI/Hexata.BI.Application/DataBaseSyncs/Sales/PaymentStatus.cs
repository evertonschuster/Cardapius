using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Hexata.BI.Application.DataBaseSyncs.Sales
{
    public enum PaymentStatus
    {
        [Display(Name = "Pendente")]
        [Description("Pendente")]
        Pending,

        [Display(Name = "Pago")]
        [Description("Pago")]
        Paid,
    }
}
