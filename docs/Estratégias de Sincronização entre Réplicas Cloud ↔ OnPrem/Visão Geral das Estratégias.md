# Estratégias de Sincronização entre Réplicas Cloud ↔ On-Prem

## 1. Visão Geral das Estratégias

| Estratégia                                | Modelo de Consistência | Principais Vantagens                           | Principais Desafios                           |
| ----------------------------------------- | ---------------------- | ---------------------------------------------- | --------------------------------------------- |
| Replicação Transacional (SQL Replication) | Quase síncrono         | Latência baixa (<1s); granularidade por tabela | Configuração e monitoramento complexos        |
| CDC + ETL (Data Factory / SSIS)           | Eventual               | Baixo impacto no OLTP; flexível                | Latência de 1–5 min; gestão de pipelines      |
| Azure SQL Data Sync                       | Eventual               | Gerenciado no portal; bidirecional             | Limites de tamanho; latência mínima de \~5min |
| Always On Availability Groups             | Síncrono/Assíncrono    | Alta disponibilidade; failover rápido          | Requer infra homóloga; latência de rede baixa |
| Event Sourcing Centralizado               | Eventual               | Histórico completo; fácil replay               | Reescrita do domínio; overhead operacional    |
| Dual-Writes + DTC                         | Síncrono               | Consistência imediata                          | Baixa escalabilidade; transações distribuídas |

## 2. Replicação Transacional (SQL Server Replication)

**Como funciona**:

1. Configura-se um **Publisher** on‑prem e um **Subscriber** no Azure SQL.
2. O **Log Reader Agent** lê o log de transações e empacota alterações.
3. O **Distribution Agent** aplica as mudanças no destino.

**Exemplo de configuração SQL**:

```sql
-- Habilitar publicação
EXEC sp_replicationdboption
    @dbname = N'PizzaDB',
    @optname = N'publish',
    @value = N'true';

-- Criar publication
EXEC sp_addpublication
    @publication = N'PizzaPublication',
    @status = N'active';

-- Adicionar artigos
EXEC sp_addarticle
    @publication = N'PizzaPublication',
    @article = N'Pedidos',
    @source_table = N'Pedidos';
```

> **Referência**: [SQL Server Replication – MS Docs](https://docs.microsoft.com/sql/relational-databases/replication)

## 3. Change Data Capture + ETL (Data Factory / SSIS)

**Como funciona**:

1. Habilita CDC nas tabelas críticas:

   ```sql
   EXEC sys.sp_cdc_enable_table
       @source_schema = N'dbo',
       @source_name   = N'Pedidos',
       @role_name     = NULL;
   ```
2. Um pipeline (Azure Data Factory, SSIS) consulta `cdc.<schema>_CT` para extrair mudanças.
3. Aplica-as em lotes no destino via UPSERT.

> **Referência**: [Change Data Capture – MS Docs](https://docs.microsoft.com/sql/relational-databases/track-changes/about-change-data-capture)

## 4. Azure SQL Data Sync

**Como funciona**:

1. Cria um Sync Group via Azure Portal ou ARM.
2. Define banco *Hub* (cloud) e *Membros* (on‑prem com agente).
3. Mapeia tabelas/colunas e intervalo de sync.

```jsonc
{
  "type": "Microsoft.Sql/servers/databases/syncGroups",
  "apiVersion": "2021-02-01-preview",
  "name": "<server>/PizzaDB/pizzaSyncGroup",
  "properties": {
    "interval": 5,
    "conflictResolutionPolicy": "HubWin"
  }
}
```

> **Referência**: [Azure SQL Data Sync – MS Docs](https://docs.microsoft.com/azure/azure-sql/database/sql-data-sync)

## 5. Always On Availability Groups Distribuídos

**Como funciona**:

1. Monta um AG primário on‑prem e outro no Azure (VMs ou Managed Instance).
2. Usa **Distributed AG** p/ espelhar  primário ↔ secundário.

```sql
ALTER AVAILABILITY GROUP AG_Pizza
ADD REPLICA ON 'azure-sqlmi.contoso.com'
WITH (
  ENDPOINT_URL = 'TCP://azure-sqlmi.contoso.com:5022',
  AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT
);
```

> **Referência**: [Always On Availability Groups – MS Docs](https://docs.microsoft.com/sql/database-engine/availability-groups/windows/)

## 6. Event Sourcing Centralizado

**Como funciona**:

1. Comandos → Agregados → Geram eventos em um **Event Store** (Kafka, EventStoreDB).
2. Cloud e on‑prem são *subscribers*, remontando projeções locais.

```csharp
// Exemplo simplificado de publicação de evento
var changeEvent = new PedidoCriadoEvent(pedidoId, itens);
await eventStore.AppendAsync(pedidoId, changeEvent);
```

> **Referência**: [Event Sourcing Pattern – MS Patterns](https://docs.microsoft.com/azure/architecture/patterns/event-sourcing)

## 7. Dual‑Writes + DTC

**Como funciona**:

1. A aplicação grava em ambos bancos usando `TransactionScope` distribuída.
2. Em falha parcial, executa lógica de compensação ou retry.

```csharp
using (var scope = new TransactionScope()) {
    contextLocal.SaveChanges();
    contextCloud.SaveChanges();
    scope.Complete();
}
```

> **Referência**: [Distributed Transactions – MS Docs](https://docs.microsoft.com/dotnet/framework/data/transactions/distributed-transactions)

## 8. Comparativo Final

| Estrat.                     | Consistência   | Latência | Complexidade Ops | Impacto no App   |
| --------------------------- | -------------- | -------- | ---------------- | ---------------- |
| Replicação Transacional     | Quase síncr.   | < 1 seg  | Alta             | Nenhum           |
| CDC + ETL                   | Eventual       | 1–5 min  | Média            | Nenhum           |
| Azure SQL Data Sync         | Eventual       | ≥ 5 min  | Baixa/Média      | Nenhum           |
| AG Distribuídos             | Síncrono/Async | < 1 seg  | Muito Alta       | Nenhum           |
| Event Sourcing Centralizado | Eventual       | < 1 seg  | Alta             | Reescrita do App |
| Dual‑Writes + DTC           | Síncrono       | < 1 seg  | Alta             | Usa DTC          |

## 9. Próximos Passos

* Definir **RPO máximo** (latência tolerável).
* Avaliar **latência** e **banda** da rede on‑prem ↔ Azure.
* Escolher entre solução **gerenciada** (Data Sync, CDC+ADF) ou **custom** (Outbox, Event Sourcing).

---

*Documento gerado com estudo de estratégias de sincronização de dados entre ambientes.*
