using System;
using System.Collections.Generic;
using System.Text;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;


namespace Core.Aspects.Autofac.Caching
{
    /// <summary>
    /// <para>Tr : Veri ekeleme güncelleme gibi durumlarda cache deki veri güncelliğini kaybedeceği için remove yapılması gerekmektedir ki önce db dena alıp sonra yeniden cachelesin</para>
    /// <para>En : This aspects must work on data new data added or current updated, because ; cache must be up to date with data </para>
    /// </summary>
    public class CacheRemoveAspect : MethodInterception
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}
