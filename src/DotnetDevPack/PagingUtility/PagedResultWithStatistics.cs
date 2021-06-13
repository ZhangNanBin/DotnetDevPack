namespace DotnetDevPack.PagingUtility
{
  using System.Collections.Generic;

  /// <summary>
  /// 分页后返回的结果，可附带更多信息
  /// </summary>
  /// <typeparam name="TOfResult">返回结果的类型</typeparam>
  /// <typeparam name="TOfStatistics">更多信息的类型</typeparam>
  public class PagedResult<TOfResult, TOfStatistics> : PagedResult<TOfResult>
  {
    /// <summary>
    /// 构造器
    /// </summary>
    /// <param name="pagedResult">已有的分页结果</param>
    /// <param name="stats">更多信息</param>
    public PagedResult(PagedResult<TOfResult> pagedResult, TOfStatistics stats)
      : this(pagedResult?.PagingInfo, pagedResult?.Result, stats)
    {
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="pagingInfo">分页信息</param>
    /// <param name="result">结果</param>
    /// <param name="stats">更多信息</param>
    public PagedResult(PagingInfo pagingInfo, List<TOfResult> result, TOfStatistics stats)
      : base(pagingInfo, result)
    {
      Statistics = stats;
    }

    /// <summary>
    /// 更多信息
    /// </summary>
    /// <value>设置或获取更多信息</value>
    public TOfStatistics Statistics { get; set; }
  }
}
