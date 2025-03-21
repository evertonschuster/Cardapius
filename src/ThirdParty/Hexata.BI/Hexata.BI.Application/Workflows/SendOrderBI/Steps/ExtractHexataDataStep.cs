using Dapper;
using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Workflows.SendOrderBI.Dtos;
using Microsoft.Extensions.Logging;
using System.Data;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Steps
{
    class ExtractHexataDataStep : StepBodyAsync
    {
        private readonly IDbConnection dbConnection;
        private readonly Instrument instrument;
        private readonly ILogger<ExtractHexataDataStep> logger;

        public ExtractHexataDataStep(IDbConnection dbConnection, Instrument instrument, ILogger<ExtractHexataDataStep> logger)
        {
            this.dbConnection = dbConnection;
            this.instrument = instrument;
            this.logger = logger;
        }

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int NextPage { get => Page + 1; }

        public List<SaleDto> Sales { get; set; }


        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            logger.LogInformation("Extracting data from Hexata database page {Page}", Page);

            const string sql = @"SELECT FIRST @PageSize SKIP @skip
                    S.CODIGO AS Id,
                    S.CLIENTE AS CustomerId,
                    S.DATA AS ""Date"",
                    S.HORARIO AS ""Time"",
                    S.VALORSDESC AS ValueWithDiscount,
                    S.DESCONTO AS Discount,
                    S.VALORCDESC AS ValueWithoutDiscount,
                    S.PRAZO AS Term,
                    S.TROCO AS Change,
                    S.VALORCLIENTE AS CustomerValue,
                    S.FUNCIONARIO AS Employee,
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
                    S.ENTREGADOR AS DeliveryPerson,
                    S.DATAENTREGA AS DeliveryDate,
                    S.DATACHEGADAENTREGA AS ArrivalDate,
                    S.HORACHEGADAENTREGA AS ArrivalTime,
                    S.HORAENTREGA AS DeliveryTime,
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
                    c.CODIGO AS CODE, 
                    c.NOMERAZAO AS COMPANYNAME, 
                    c.FANTASIAAPELIDO AS FANTASYNAME, 
                    c.ENDERECO AS ADDRESS, 
                    c.NUMENDERECO AS ADDRESSNUMBER, 
                    c.BAIRRO AS NEIGHBORHOOD, 
                    c.COMPLEMENTO AS ADDRESSCOMPLEMENT, 
                    c.CIDADE AS CITY, 
                    c.ESTADO AS STATE, 
                    c.CEP AS ZIPCODE, 
                    c.SITUACAOCREDITO AS CREDITSTATUS, 
                    c.TIPO AS TYPE, 
                    c.TAXAENTREGA AS DELIVERYFEE, 
                    c.QTDEVENDAS AS SALESQUANTITY
                    
                FROM SAIDAS S
                INNER JOIN CLIENTES c ON c.CODIGO = S.CLIENTE
                ORDER BY S.CODIGO ASC
                ;";

            using var activity = instrument.ExecuteDataBaseQuery();

            var sales = await dbConnection.QueryAsync<SaleDto, CustomerDto, SaleDto>(sql, (sale, customer) =>
                {
                    sale.Customer = customer;
                    return sale;
                },
                new
                {
                    PageSize,
                    Skip = Page * PageSize
                },
                splitOn: "Customer"
            );

            Sales?.Clear();
            Sales = sales.ToList();

            return ExecutionResult.Next();
        }
    }
}
