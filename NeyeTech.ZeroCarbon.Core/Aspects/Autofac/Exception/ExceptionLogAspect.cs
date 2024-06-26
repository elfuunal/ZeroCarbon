﻿using NeyeTech.ZeroCarbon.Core.CrossCuttingConcerns.Logging;
using NeyeTech.ZeroCarbon.Core.CrossCuttingConcerns.Logging.Serilog;
using NeyeTech.ZeroCarbon.Core.Utilities.Interceptors;
using NeyeTech.ZeroCarbon.Core.Utilities.IoC;
using NeyeTech.ZeroCarbon.Core.Utilities.Messages;
using Castle.DynamicProxy;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace NeyeTech.ZeroCarbon.Core.Aspects.Autofac.Exception
{
    /// <summary>
    /// ExceptionLogAspect
    /// </summary>
    public class ExceptionLogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExceptionLogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new ArgumentException(AspectMessages.WrongLoggerType);
            }
            _loggerServiceBase = (LoggerServiceBase)ServiceTool.ServiceProvider.GetService(loggerService);
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            var logDetailWithException = GetLogDetail(invocation);

            if (e is not ValidationException)
            {
                logDetailWithException.ExceptionMessage = e is System.Exception
                 ? string.Join(Environment.NewLine, (e as AggregateException).InnerExceptions.Select(x => x.Message))
                 : e.Message;
                _loggerServiceBase.Error(JsonConvert.SerializeObject(logDetailWithException));
            }
        }

        private LogDetailWithException GetLogDetail(IInvocation invocation)
        {
            var logParameters = invocation.Arguments.Select((t, i) => new LogParameter
            {
                Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                Value = t,
                Type = t.GetType().Name
            })
                .ToList();
            var logDetailWithException = new LogDetailWithException
            {
                MethodName = invocation.Method.Name,
                Parameters = logParameters,
                User = (_httpContextAccessor.HttpContext == null ||
                        _httpContextAccessor.HttpContext.User.Identity.Name == null)
                    ? "?"
                    : _httpContextAccessor.HttpContext.User.Identity.Name
            };
            return logDetailWithException;
        }
    }
}
