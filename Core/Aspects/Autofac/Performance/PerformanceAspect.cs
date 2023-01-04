using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    /// <summary>
    /// <para>TR : Metodun nekaar süreceğini kontrol etmekte kullanılır</para>
    /// <para>En : Use for calculate execution time of metot</para>
    /// </summary>
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopwatch;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval">
        /// <para>Tr: uyarı veirlmesi için aşılması beklenilen zaman (saniye)</para>
        /// <para>En : the max time for metot execution time which give warning</para>
        /// </param>
        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }


        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                // todo : eğer metotun işlteim zamanı interval değerini aşarsa istenilen kod burada çalıitırlabilir, buraya func geç ki istenilen kodlar core da tutulmamış olsun

                // ör :
                Debug.WriteLine($"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}");
            }
            _stopwatch.Reset();
        }
    }
}
