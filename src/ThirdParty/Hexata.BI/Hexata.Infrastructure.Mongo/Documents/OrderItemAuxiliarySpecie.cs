using System.ComponentModel;

namespace Hexata.Infrastructure.Mongo.Documents
{
    internal record OrderItemAuxiliarySpecie
    {
        public OrderItemAuxiliarySpecie(int orderId, int orderItemId, string auxiliarySpecie)
        {
            Id = $"{orderId}:{orderItemId}:{auxiliarySpecie}";
            OrderId = orderId;
            OrderItemId = orderItemId;
            AuxiliarySpecie = auxiliarySpecie;
        }

        [DisplayName("Codigo saida item especie auxiliar")]
        public string Id { get; set; }

        [DisplayName("Codigo de saida")]
        public int OrderId { get; set; }

        [DisplayName("Codigo saida item")]
        public int OrderItemId { get; set; }

        [DisplayName("Especie auxiliar")]
        public string AuxiliarySpecie { get; set; }
    }
}
