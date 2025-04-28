using Dapper;
using Hexata.BI.Application.DataBaseSyncs;
using Hexata.BI.Application.DataBaseSyncs.Customers.Models;
using Hexata.BI.Application.Repositories;
using System.Data;

namespace Hexata.Infrastructure.Firebird.Repositories
{
    internal class ErpCustomerRepository(IDbConnection dbConnection) : IErpCustomerRepository
    {
        public async Task<IEnumerable<Customer>> ListAsync(SyncDto syncDto, CancellationToken cancellationToken)
        {
            const string query = @"
                SELECT FIRST @PageSize SKIP @skip
                    CODIGO AS Id, 
                    NOMERAZAO AS Name, 
                    FANTASIAAPELIDO AS TradeName, 
                    ENDERECO AS Address, 
                    NUMENDERECO AS AddressNumber, 
                    BAIRRO AS District, 
                    COMPLEMENTO AS Complement,  
                    FONE AS Phone, 
                    FONE2 AS Phone2,  
                    CELULAR AS Mobile,   
                    CGCCPF AS CgcCpf,  
                    DATACADASTRO AS RegistrationDate,  
                    CREDITO AS Credit, 
                    SITUACAOCREDITO AS CreditStatus, 
                    TIPO AS Type,  
                    TAXAENTREGA AS DeliveryFee,   
                    DIAPADRAOVENCTO AS DefaultDueDay, 
                    OBS AS Notes, 
                    CREDITOPORTEMPO AS TemporaryCredit,      
                    AUTORIZARCORTESIA AS AllowCourtesy,      
                    QTDEVENDAS AS SalesCount
                FROM CLIENTES
                ORDER BY CODIGO DESC;
                ";

            var param = new
            {
                PageSize = syncDto.PageSize,
                Skip = (syncDto.Page - 1) * syncDto.PageSize
            };

            return await dbConnection.QueryAsync<Customer>(query, param);
        }
    }
}
