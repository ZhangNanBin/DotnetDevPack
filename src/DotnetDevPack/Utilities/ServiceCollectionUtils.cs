namespace DotnetDevPack.Utilities
{
  using System;
  using System.Linq;
  using Microsoft.Extensions.DependencyInjection;

  /// <summary>
  /// ServiceCollection工具类
  /// </summary>
  public static class ServiceCollectionUtils
  {
    /// <summary>
    /// 获取单例注册服务对象
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="services">依赖注入服务容器</param>
    /// <returns>数据</returns>
    public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
    {
      ServiceDescriptor descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(T) && d.Lifetime == ServiceLifetime.Singleton);

      if (descriptor?.ImplementationInstance != null)
      {
        return (T)descriptor.ImplementationInstance;
      }

      if (descriptor?.ImplementationFactory != null)
      {
        return (T)descriptor.ImplementationFactory.Invoke(null);
      }

      return default;
    }

    /// <summary>
    /// 获取Scoped注册服务对象
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="services">依赖注入服务容器</param>
    /// <returns>数据</returns>
    public static T GetScopedInstanceOrNull<T>(this IServiceCollection services)
    {
      ServiceDescriptor descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(T) && d.Lifetime == ServiceLifetime.Scoped);

      if (descriptor?.ImplementationInstance != null)
      {
        return (T)descriptor.ImplementationInstance;
      }

      if (descriptor?.ImplementationFactory != null)
      {
        return (T)descriptor.ImplementationFactory.Invoke(null);
      }

      return default;
    }

    /// <summary>
    /// 获取Transient注册服务对象
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="services">依赖注入服务容器</param>
    /// <returns>数据</returns>
    public static T GetTransientInstanceOrNull<T>(this IServiceCollection services)
    {
      ServiceDescriptor descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(T) && d.Lifetime == ServiceLifetime.Transient);

      if (descriptor?.ImplementationInstance != null)
      {
        return (T)descriptor.ImplementationInstance;
      }

      if (descriptor?.ImplementationFactory != null)
      {
        return (T)descriptor.ImplementationFactory.Invoke(null);
      }

      return default;
    }

    /// <summary>
    /// 如果指定服务不存在，创建实例并添加
    /// </summary>
    /// <typeparam name="TServiceType">服务类型</typeparam>
    /// <param name="services">依赖注入服务容器</param>
    /// <param name="factory">返回类型Function</param>
    /// <returns>返回对应服务</returns>
    public static TServiceType GetOrAddSingletonInstance<TServiceType>(this IServiceCollection services, Func<TServiceType> factory)
        where TServiceType : class
    {
      if (services is null)
      {
        throw new ArgumentNullException(nameof(services));
      }

      if (factory is null)
      {
        throw new ArgumentNullException(nameof(factory));
      }

      TServiceType item = GetSingletonInstanceOrNull<TServiceType>(services);
      if (item == null)
      {
        item = factory();
        services.AddSingleton(item);
      }

      return item;
    }
  }
}
