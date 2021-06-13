namespace DotnetDevPack.Exceptions
{
  using System;
  using System.Net;

  /// <summary>
  /// Http异常
  /// </summary>
  public class HttpGlobalException : Exception
  {
    /// <summary>
    /// 构造器
    /// </summary>
    public HttpGlobalException()
    {
    }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="message">消息</param>
    public HttpGlobalException(string message)
      : this(message, HttpStatusCode.InternalServerError)
    {
    }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="innerException">内部异常</param>
    public HttpGlobalException(string message, Exception innerException)
      : this(message, innerException, HttpStatusCode.InternalServerError)
    {
    }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="statusCode">状态码</param>
    public HttpGlobalException(int statusCode)
      : this((HttpStatusCode)statusCode)
    {
    }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="statusCode">状态码</param>
    public HttpGlobalException(HttpStatusCode statusCode)
      : this(null, statusCode)
    {
    }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="statusCode">状态码</param>
    public HttpGlobalException(string message, int statusCode)
      : this(message, (HttpStatusCode)statusCode)
    {
    }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="statusCode">状态码</param>
    public HttpGlobalException(string message, HttpStatusCode statusCode)
      : this(message, null, statusCode)
    {
    }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="innerException">内部异常</param>
    /// <param name="statusCode">状态码</param>
    public HttpGlobalException(string message, Exception innerException, int statusCode)
      : this(message, innerException, (HttpStatusCode)statusCode)
    {
    }

    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="innerException">内部异常</param>
    /// <param name="statusCode">状态码</param>
    public HttpGlobalException(string message, Exception innerException, HttpStatusCode statusCode)
      : base(message, innerException)
    {
      StatusCode = statusCode;
    }

    /// <summary>
    /// Http状态码
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }
  }
}
