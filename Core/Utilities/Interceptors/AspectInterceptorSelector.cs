using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Castle.DynamicProxy;
using Core.Aspects.Autofac.Performance;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);

            // todo : aşağıdaki tüm metotların performansının ölçülmesi olayını .config den yönetilebilir hale getirebilirsin
            //classAttributes.Add(new PerformanceAspect(5));// tüm metotların performans takibi yapılamk istenirse bu satır açılabilir


            return classAttributes.OrderBy(x => x.Priority).ToArray();
        }
    }
}
