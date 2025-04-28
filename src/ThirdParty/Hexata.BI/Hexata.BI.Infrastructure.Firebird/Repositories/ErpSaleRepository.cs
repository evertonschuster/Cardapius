using Dapper;
using Hexata.BI.Application.DataBaseSyncs;
using Hexata.BI.Application.DataBaseSyncs.Sales;
using Hexata.BI.Application.DataBaseSyncs.Sales.Models;
using Hexata.BI.Application.Extensions;
using Hexata.BI.Application.Repositories;
using Hexata.BI.Application.Services.Localizations;
using Hexata.Infrastructure.Firebird.Extensions;
using System.Data;

namespace Hexata.Infrastructure.Firebird.Repositories
{
    internal class ErpSaleRepository(IDbConnection dbConnection) : IErpSaleRepository
    {
        public async Task<IEnumerable<Order>> ListAsync(SyncDto syncDto, CancellationToken cancellationToken)
        {
            const string sqlSaida = @"SELECT FIRST @PageSize SKIP @skip
                    S.CODIGO AS Id,
                    S.CLIENTE AS CustomerId,
                    S.DATA || ' ' || S.HORARIO AS ""Date"",
                    S.VALORSDESC AS ValueWithDiscount,
                    S.DESCONTO AS Discount,
                    S.VALORCDESC AS ValueWithoutDiscount,
                    S.PRAZO AS Term,
                    S.TROCO AS Change,
                    S.VALORCLIENTE AS CustomerValue,
                    S.FUNCIONARIO AS EmployeeId,
                    S.CAIXA AS CashRegister,
                    S.VALORCOMISSAO AS CommissionValue,
                    S.ESTADO AS Status,
                    S.CARTAO AS IsCardPayment,
                    S.MESA AS TableNumber,
                    S.QTDEPESSOAS AS PeopleCount,
                    S.CARTAOCREDITO AS IsCreditCardPayment,
                    S.OPERADORCAIXA AS CashOperator,
                    S.CONTROLE AS Control,
                    S.PC AS PC,
                    S.JAIMPRIMIU AS HasPrinted,
                    S.PLACA AS Plate,
                    S.CARTAOTEMP AS TemporaryCard,
                    S.ENTREGADOR AS DeliveryPersonId,
                    S.DATAENTREGA  || ' ' || S.HORAENTREGA AS DeliveryDate ,
                    S.DATACHEGADAENTREGA  || ' ' || S.HORACHEGADAENTREGA  AS ArrivalDate,
                    S.DESCONTOCARTAO AS CardDiscount,
                    S.TAXA AS Fee,
                    S.OBS AS Notes,
                    S.TIPOVENDA AS SaleType,
                    S.RETIRADA AS IsPickup,
                    S.NOMECLIENTE AS CustomerName,
                    S.VALORDINHEIRO AS CashValue,
                    S.VALORCARTAO1 AS CardValue1,
                    S.VALORCARTAO2 AS CardValue2,
                    S.VALORCHEQUE AS CheckValue,
                    S.VALORPRAZO AS TermValue,
                    S.VALORDESCONTO AS DiscountValue,
                    S.ENDERECOENTREGA AS DeliveryAddress,
                    S.TERMINAL AS Terminal,
                    S.PRINTCONTA AS PrintAccount,
                    S.REFERENCIAALFA AS AlphaReference,
                    S.SITUACAOPAGAMENTO AS PaymentStatus,
                    S.ATENDIMENTOCARTAO AS CardService,
                    S.CODIGOCARTAOTEMP AS TemporaryCardCode,
                    S.BAIRRO AS Neighborhood,
                    S.HORARIOAPROXIMADO AS ApproximateTime,
                    S.HORARIOAPROXIMADOBALCAO AS ApproximateCounterTime,
                    S.ATENCAO AS Attention,
                    S.KM AS DistanceKm,
                    S.HORARIOAGENDADO AS ScheduledTime,
                    S.NOMECARTAO1 AS CardHolderName1,
                    S.NOMECARTAO2 AS CardHolderName2,
                    S.DESCRICAOALFA AS AlphaDescription,
                    S.VALORTROCODOADO AS DonatedChangeValue,
                    S.EMESPERA AS IsPending,
                    S.GERADORDOPEDIDOWEB AS WebOrderGenerator,
                    S.REFERENCIAGERADORWEB AS WebReferenceGenerator,
                    S.CODIGOGERADORWEB AS WebGeneratorCode,
                    S.RELACAOGERADORWEB AS WebGeneratorRelation,
                    S.VALORCARTAOIFOOD AS IfoodCardValue,
                    S.PAGOONLINE AS IsPaidOnline,
                    S.HORAFECHAMENTO AS ClosingTime,
                    S.DATAFECHAMENTO AS ClosingDate,
                    S.NOMEATENDENTEFECHAMENTO AS ClosingAttendantName,
                    S.CPFNANOTA AS CPFNote,
                    S.IMPRIMIRNOCONTROLEDEPRODUCAO AS PrintOnProductionControl,
                    S.ENTREGAGRATUITA AS FreeDelivery,
                    S.BALCAOPRONTO AS CounterReady,
                    S.CHECKNET AS CheckNet,
                    S.CHECKPRINTCP AS CheckPrintCP,
                    S.INICIOUPRODUCAO AS ProductionStarted,
                    S.VALORPIX AS PixValue,
                    S.VALORDESCONTOAPPPLATAFORMA AS AppPlatformDiscount,
                    S.VALORDESCONTOAPPLOJA AS AppStoreDiscount,
                    S.VALOREMCONSUMACAO AS ConsumptionValue,
                    S.CHAMARINTEGRACAOMOTOBOY AS CallDeliveryIntegration,
                    S.AUXINTEGRACAOMOTOBOY AS AuxiliaryDeliveryIntegration,
                    '1' as Customer,
                    
                    c.ENDERECO AS ADDRESS, 
                    c.NUMENDERECO AS ADDRESSNUMBER, 
                    c.BAIRRO AS ADDRESSNEIGHBORHOOD, 
                    c.COMPLEMENTO AS ADDRESSCOMPLEMENT, 
                    c.CIDADE AS CITY, 
                    c.ESTADO AS STATE, 
                    c.CEP AS ZIPCODE
                    
                FROM SAIDAS S
                    INNER JOIN CLIENTES c ON c.CODIGO = S.CLIENTE

                ORDER BY S.CODIGO DESC
                ;";

            const string sqlItem = @"
                SELECT
                    SI.CODIGO AS Id,
                    SI.SAIDA AS EXITId,
                    SI.PRODUTO AS PRODUCT,
                    SI.DESCRICAO AS DESCRIPTION,
                    SI.QTDE AS QUANTITY,
                    SI.VALORUNITARIO AS UNITVALUE,
                    SI.DESCONTO AS DISCOUNT,
                    SI.TOTALCDESCONTO AS TOTALDISCOUNT,
                    SI.DATA || ' ' || SI.HORARIO AS ""Date"",
                    SI.CLIENTE AS CLIENTId,
                    SI.CONTEUDO AS CONTENT,
                    SI.UNIDADE AS UNIT,
                    SI.FUNCIONARIO AS EMPLOYEEId,
                    SI.VALORCOMISSAO AS COMMISSIONVALUE,
                    SI.PORCCOMISSAO AS COMMISSIONPERCENTAGE,
                    SI.CAIXA AS CashRegister,
                    SI.COMPUTADOR AS COMPUTER,
                    SI.CLIENTECARTAO AS CLIENTCARD,
                    SI.CARTAOCREDITO AS CREDITCARD,
                    SI.OBS AS NOTE,
                    SI.CAIXAOPERADOR AS CashRegisterOperator,
                    SI.FONECLIENTE AS CLIENTPHONE,
                    SI.AUXESPECIE AS AuxiliarySpecies,
                    SI.PLACA AS LICENSEPLATE,
                    SI.KM AS KM,
                    SI.DATAPAGTO AS PAYMENTDATE,
                    SI.SITUACAO AS SITUATION,
                    SI.FUNCIONARIOLANC AS EMPLOYEELANC,
                    SI.GRUPOA AS GROUPA,
                    SI.TIPOVENDA AS SALETYPE,
                    SI.VALIDADE AS VALIDITY,
                    SI.TEMPORARIO AS TEMPORARY,
                    SI.RETIROU AS WITHDREW,
                    SI.DATARETIRADA AS WITHDRAWALDATE,
                    SI.RESPONSAVELRETIRADA AS WITHDRAWALRESPONSIBLE,
                    SI.FUNCIONARIORETIRADA AS WITHDRAWALEMPLOYEE,
                    SI.TIPOITEM AS ITEMTYPE,
                    SI.COR AS COLOR,
                    SI.TAMANHO AS SIZE,
                    SI.GRUPOB AS GROUPB,
                    SI.NOMEFUNCIONARIO AS EMPLOYEENAME,
                    SI.VALORBRUTO AS GROSSVALUE,
                    SI.DATASAIDA AS EXITDATE,
                    SI.DATARETORNO AS RETURNDATE,
                    SI.VALORTAXASERVICO AS SERVICETAXVALUE,
                    SI.IMPRESSO AS PRINTED,
                    SI.ONDEIMPRIMIR AS WHEREPRINTED,
                    SI.IMPRIMIR AS PRINT,
                    SI.VALORCOMODATO AS LOANVALUE,
                    SI.CODIGONCM AS NCMCODE,
                    SI.CST AS CST,
                    SI.SITUACAOCOMISSAO AS COMMISSIONSITUATION,
                    SI.OBSMESA AS MESAOBS,
                    SI.IPVENDA AS SALESIP,
                    SI.CODIGOATENDIMENTO AS ATTENDANCECODE,
                    SI.REFERENCIACARTAO AS CARDREFERENCE,
                    SI.FUNCIONARIODESCONTO AS DISCOUNTEMPLOYEEId,
                    SI.NOMEFUNCIONARIODESCONTO AS DISCOUNTEMPLOYEENAME,
                    SI.REFERENCIACOMBO AS COMBOREFERENCE,
                    SI.CODESP1 AS CODESP1,
                    SI.CODESP2 AS CODESP2,
                    SI.CODESP3 AS CODESP3,
                    SI.CODESP4 AS CODESP4,
                    SI.CODESP5 AS CODESP5,
                    SI.CODESP6 AS CODESP6,
                    SI.CODESP7 AS CODESP7,
                    SI.CODESP8 AS CODESP8,
                    SI.CODESP9 AS CODESP9,
                    SI.CODESP10 AS CODESP10,
                    SI.ORDEMPEDIDO AS ORDERNUMBER
                FROM ITENSSAIDA SI
                WHERE
                    SI.SAIDA IN @saidaIds
            ";

            var param = new
            {
                PageSize = syncDto.PageSize,
                Skip = (syncDto.Page - 1) * syncDto.PageSize
            };

            var ordersReader = await dbConnection.ExecuteReaderAsync(sqlSaida, param);
            var orders = MapOrder(ordersReader).ToList();
            if (orders.Count == 0)
            {
                return orders;
            }

            var orderIds = orders.Select(o => o.Id);
            var items = await dbConnection.QueryAsync<OrderItem>(sqlItem, new { saidaIds = orderIds });
            var lookup = items.ToLookup(i => i.ExitId);
            orders.ForEach(o => o.Items = lookup[o.Id].ToList());

            return orders;
        }

        private IEnumerable<Order> MapOrder(IDataReader reader)
        {
            while (reader.Read())
            {
                var address = new AddressDto()
                {
                    Street = reader.GetValueOrDefault<string?>("ADDRESS")?.Sanitizing() ?? reader.GetValueOrDefault<string?>("DeliveryAddress")?.Sanitizing(),
                    Number = reader.GetValueOrDefault<string?>("ADDRESSNUMBER")?.Sanitizing(),
                    Neighborhood = reader.GetValueOrDefault<string?>("ADDRESSNEIGHBORHOOD").Sanitizing() ?? reader.GetValueOrDefault<string?>("NEIGHBORHOOD")?.Sanitizing(),
                    City = reader.GetValueOrDefault<string?>("CITY")?.Sanitizing(),
                    State = reader.GetValueOrDefault<string?>("STATE")?.Sanitizing(),
                    PostalCode = reader.GetValueOrDefault<string?>("ZIPCODE")?.Sanitizing()
                };

                if (address.Street == null)
                {
                    address = null;
                }

                var order = new Order
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    CustomerId = reader.GetValueOrDefault("CustomerId", 0),
                    Date = reader.GetValueOrDefault<DateTime>("Date"),
                    EmployeeId = reader.GetValueOrDefault("EmployeeId", 0),
                    ValueWithDiscount = reader.GetValueOrDefault<double?>("ValueWithDiscount"),
                    Discount = reader.GetValueOrDefault<double?>("Discount"),
                    ValueWithoutDiscount = reader.GetValueOrDefault<double>("ValueWithoutDiscount"),
                    Term = reader.GetValueOrDefault<int>("Term"),
                    Change = reader.GetValueOrDefault<double?>("Change"),
                    CustomerValue = reader.GetValueOrDefault<double>("CustomerValue"),
                    CashRegister = reader.GetValueOrDefault<double?>("CashRegister"),
                    CommissionValue = reader.GetValueOrDefault<double?>("CommissionValue"),
                    PaymentStatus = reader.GetValueOrDefault<PaymentStatus>("PaymentStatus"),
                    CashValue = reader.GetValueOrDefault<double?>("CashValue"),
                    PixValue = reader.GetValueOrDefault<double?>("PixValue"),
                    CardValue1 = reader.GetValueOrDefault<double?>("CardValue1"),
                    CardValue2 = reader.GetValueOrDefault<double?>("CardValue2"),
                    CheckValue = reader.GetValueOrDefault<double?>("CheckValue"),
                    CardHolderName1 = reader.GetValueOrDefault<string?>("CardHolderName1"),
                    CardHolderName2 = reader.GetValueOrDefault<string?>("CardHolderName2"),
                    TermValue = reader.GetValueOrDefault<double?>("TermValue"),
                    DiscountValue = reader.GetValueOrDefault<double?>("DiscountValue"),
                    DonatedChangeValue = reader.GetValueOrDefault<double?>("DonatedChangeValue"),

                    Status = reader.GetValueOrDefault<string?>("Status")?.Sanitizing(),
                    PC = reader.GetValueOrDefault<string>("PC")?.Sanitizing(),
                    ArrivalDate = reader.GetValueOrDefault<DateTime?>("ArrivalDate"),
                    Notes = reader.GetValueOrDefault<string?>("Notes")?.Sanitizing(),
                    SaleType = reader.GetValueOrDefault<string?>("SaleType")?.Sanitizing(),

                    FreeDelivery = reader.GetValueOrDefault<string?>("FreeDelivery")?.Sanitizing(),
                    DeliveryDate = reader.GetValueOrDefault<DateTime?>("DeliveryDate"),
                    DeliveryPersonId = reader.GetValueOrDefault<int?>("DeliveryPersonId"),
                    DeliveryAddress = reader.GetValueOrDefault<string?>("DeliveryAddress")?.Sanitizing(),
                    Neighborhood = reader.GetValueOrDefault<string?>("Neighborhood")?.Sanitizing(),
                    Terminal = reader.GetValueOrDefault<string?>("Terminal")?.Sanitizing(),

                    ApproximateTime = reader.GetValueOrDefault<string?>("ApproximateTime")?.Sanitizing(),
                    ApproximateCounterTime = reader.GetValueOrDefault<string?>("ApproximateCounterTime")?.Sanitizing(),

                    IsPending = reader.GetValueOrDefault<string?>("IsPending")?.Sanitizing(),
                    WebOrderGenerator = reader.GetValueOrDefault<string?>("WebOrderGenerator")?.Sanitizing(),
                    ClosingDate = reader.GetValueOrDefault<DateTime?>("ClosingDate"),
                    ClosingAttendantName = reader.GetValueOrDefault<string?>("ClosingAttendantName")?.Sanitizing(),
                    CounterReady = reader.GetValueOrDefault<string?>("CounterReady")?.Sanitizing(),
                    CheckNet = reader.GetValueOrDefault<string?>("CheckNet")?.Sanitizing(),

                    Address = address
                };

                yield return order;
            }
        }
    }
}
