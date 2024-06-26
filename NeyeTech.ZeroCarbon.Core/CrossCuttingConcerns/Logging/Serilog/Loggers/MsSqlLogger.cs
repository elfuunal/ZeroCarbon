﻿using NeyeTech.ZeroCarbon.Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using NeyeTech.ZeroCarbon.Core.Utilities.IoC;
using NeyeTech.ZeroCarbon.Core.Utilities.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Data;

namespace NeyeTech.ZeroCarbon.Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class MsSqlLogger : LoggerServiceBase
    {
        public MsSqlLogger()
        {
            var configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            var logConfig = configuration.GetSection("SeriLogConfigurations:MsSqlConfiguration")
                                .Get<MsSqlConfiguration>() ??
                            throw new Exception(SerilogMessages.NullOptionsMessage);
            var sinkOpts = new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true };

            var columnOpts = new ColumnOptions();

            SqlColumn userId = new SqlColumn { DataType = SqlDbType.BigInt, ColumnName = "UserId", AllowNull = true };
            SqlColumn methodName = new SqlColumn { DataType = SqlDbType.NVarChar, ColumnName = "Method", AllowNull = true };

            columnOpts.AdditionalColumns = new List<SqlColumn> 
            {
                userId,
                methodName,
            };

            columnOpts.Store.Remove(StandardColumn.MessageTemplate);
            columnOpts.Store.Remove(StandardColumn.Properties);
            columnOpts.Store.Remove(StandardColumn.Level);

            var seriLogConfig = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(connectionString: logConfig.ConnectionString, sinkOptions: sinkOpts, columnOptions: columnOpts)
                .CreateLogger();

            Logger = seriLogConfig;
        }
    }
}
