using Dapper;
using Hexata.BI.Application.Workflows.SendOrderBI.Dtos;
using System.Data;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Steps
{
    class ExtractHexataDataStep : StepBodyAsync
    {
        private readonly IDbConnection dbConnection;
        private readonly SendOrderBIInstrument sendOrderBIInstrument;

        public int Page { get; set; }
        public int PageSize { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            using var activity = sendOrderBIInstrument.ExecuteDataBaseQuery();

            var sales = await dbConnection.QueryAsync<SaleDto>(@"SELECT 
                    CODIGO AS Id,
                    CLIENTE AS Customer,
                    DATA AS Date,
                    HORARIO AS Time,
                    VALORSDESC AS ValueWithDiscount,
                    DESCONTO AS Discount,
                    VALORCDESC AS ValueWithoutDiscount,
                    PRAZO AS Term,
                    TROCO AS Change,
                    VALORCLIENTE AS CustomerValue,
                    FUNCIONARIO AS Employee,
                    CAIXA AS CashRegister,
                    VALORCOMISSAO AS CommissionValue,
                    ESTADO AS Status,
                    CARTAO AS IsCardPayment,
                    MESA AS TableNumber,
                    QTDEPESSOAS AS PeopleCount,
                    CARTAOCREDITO AS IsCreditCardPayment,
                    OPERADORCAIXA AS CashOperator,
                    CONTROLE AS Control,
                    PC AS PC,
                    JAIMPRIMIU AS HasPrinted,
                    PLACA AS Plate,
                    CARTAOTEMP AS TemporaryCard,
                    ENTREGADOR AS DeliveryPerson,
                    DATAENTREGA AS DeliveryDate,
                    DATACHEGADAENTREGA AS ArrivalDate,
                    HORACHEGADAENTREGA AS ArrivalTime,
                    HORAENTREGA AS DeliveryTime,
                    DESCONTOCARTAO AS CardDiscount,
                    TAXA AS Fee,
                    OBS AS Notes,
                    TIPOVENDA AS SaleType,
                    RETIRADA AS IsPickup,
                    NOMECLIENTE AS CustomerName,
                    VALORDINHEIRO AS CashValue,
                    VALORCARTAO1 AS CardValue1,
                    VALORCARTAO2 AS CardValue2,
                    VALORCHEQUE AS CheckValue,
                    VALORPRAZO AS TermValue,
                    VALORDESCONTO AS DiscountValue,
                    ENDERECOENTREGA AS DeliveryAddress,
                    TERMINAL AS Terminal,
                    PRINTCONTA AS PrintAccount,
                    REFERENCIAALFA AS AlphaReference,
                    SITUACAOPAGAMENTO AS PaymentStatus,
                    ATENDIMENTOCARTAO AS CardService,
                    CODIGOCARTAOTEMP AS TemporaryCardCode,
                    BAIRRO AS Neighborhood,
                    HORARIOAPROXIMADO AS ApproximateTime,
                    HORARIOAPROXIMADOBALCAO AS ApproximateCounterTime,
                    ATENCAO AS Attention,
                    KM AS DistanceKm,
                    HORARIOAGENDADO AS ScheduledTime,
                    NOMECARTAO1 AS CardHolderName1,
                    NOMECARTAO2 AS CardHolderName2,
                    DESCRICAOALFA AS AlphaDescription,
                    VALORTROCODOADO AS DonatedChangeValue,
                    EMESPERA AS IsPending,
                    GERADORDOPEDIDOWEB AS WebOrderGenerator,
                    REFERENCIAGERADORWEB AS WebReferenceGenerator,
                    CODIGOGERADORWEB AS WebGeneratorCode,
                    RELACAOGERADORWEB AS WebGeneratorRelation,
                    VALORCARTAOIFOOD AS IfoodCardValue,
                    PAGOONLINE AS IsPaidOnline,
                    HORAFECHAMENTO AS ClosingTime,
                    DATAFECHAMENTO AS ClosingDate,
                    NOMEATENDENTEFECHAMENTO AS ClosingAttendantName,
                    CPFNANOTA AS CPFNote,
                    IMPRIMIRNOCONTROLEDEPRODUCAO AS PrintOnProductionControl,
                    ENTREGAGRATUITA AS FreeDelivery,
                    BALCAOPRONTO AS CounterReady,
                    CHECKNET AS CheckNet,
                    CHECKPRINTCP AS CheckPrintCP,
                    INICIOUPRODUCAO AS ProductionStarted,
                    VALORPIX AS PixValue,
                    VALORDESCONTOAPPPLATAFORMA AS AppPlatformDiscount,
                    VALORDESCONTOAPPLOJA AS AppStoreDiscount,
                    VALOREMCONSUMACAO AS ConsumptionValue,
                    CHAMARINTEGRACAOMOTOBOY AS CallDeliveryIntegration,
                    AUXINTEGRACAOMOTOBOY AS AuxiliaryDeliveryIntegration
                FROM SAIDAS;");




            return ExecutionResult.Next();
        }
    }
}
