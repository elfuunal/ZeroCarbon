﻿using NeyeTech.ZeroCarbon.Core.Utilities.Interceptors;
using Castle.DynamicProxy;
using System.Transactions;

namespace NeyeTech.ZeroCarbon.Core.Aspects.Autofac.Transaction
{
    /// <summary>
    /// TransactionScopeAspect
    /// </summary>
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (var transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (System.Exception ex)
                {
                    ex.ToString();
                    throw;
                }
            }
        }
    }
}
