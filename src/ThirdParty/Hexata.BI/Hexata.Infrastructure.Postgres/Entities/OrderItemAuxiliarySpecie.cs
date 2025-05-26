using System.ComponentModel.DataAnnotations.Schema;

namespace Hexata.Infrastructure.Postgres.Entities
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

        [Column("Codigo saida item especie auxiliar")]
        public string Id { get; set; }

        [Column("Codigo de saida")]
        public int OrderId { get; set; }

        [Column("Codigo saida item")]
        public int OrderItemId { get; set; }

        [Column("Especie auxiliar")]
        public string AuxiliarySpecie { get; set; }
    }
}