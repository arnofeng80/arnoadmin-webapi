using AspectCore.DynamicProxy;
using System;
using System.Threading.Tasks;
using System.Transactions;

namespace ArnoAdminCore.Transaction
{
    public class TransactionalAttribute : AbstractInterceptorAttribute
    {
        public int Timeout { get; set; } = 60;
        public TransactionScopeOption ScopeOption { get; set; } = TransactionScopeOption.Required;
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;
        public TransactionalAttribute() { }

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            TransactionOptions transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = IsolationLevel;
            if(Timeout != 0)
            {
                transactionOptions.Timeout = new TimeSpan(0, 0, Timeout);
            }
            
            using (TransactionScope scope = new TransactionScope(ScopeOption, transactionOptions))
            {
                await next(context);
                scope.Complete();
            }
        }
    }
}

