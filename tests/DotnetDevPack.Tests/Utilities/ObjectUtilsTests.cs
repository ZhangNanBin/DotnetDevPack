namespace DotnetDevPack.Tests.Utilities
{
  using System;
  using DotnetDevPack.Utilities;
  using Xunit;

  public class ObjectUtilsTests
  {
    [Theory(DisplayName = "Object转Int")]
    [InlineData(null, 0)]
    [InlineData(TestEnum.Bool, 2)]
    [InlineData("Zhang", 0)]
    [InlineData("10", 10)]
    public void ToInt_Success(object value, int revalValue)
    {
      Assert.Equal(value.ToInt(), revalValue);
    }

    [Theory(DisplayName = "Object转Int，带默认值")]
    [InlineData(null, 0)]
    [InlineData(TestEnum.Number, 0)]
    [InlineData("Zhang", 10)]
    [InlineData("10", 10)]
    public void ToInt_Default_Success(object value, int defaultValue)
    {
      Assert.Equal(value.ToInt(defaultValue), defaultValue);
    }

    [Theory(DisplayName = "Object转Double")]
    [InlineData(null, 0d)]
    [InlineData(TestEnum.String, 1d)]
    [InlineData("Zhang", 0d)]
    [InlineData("10", 10d)]
    public void ToDouble_Success(object value, int revalValue)
    {
      Assert.Equal(value.ToDouble(), revalValue);
    }

    [Theory(DisplayName = "Object转Double，带默认值")]
    [InlineData(null, 6.6d)]
    [InlineData(TestEnum.Other, 3d)]
    [InlineData("Zhang", 8.8d)]
    [InlineData("10", 10d)]
    public void ToDouble_Default_Success(object value, int defaultValue)
    {
      Assert.Equal(value.ToDouble(defaultValue), defaultValue);
    }


    [Theory(DisplayName = "Object转Decimal")]
    [InlineData(null, 0)]
    [InlineData(TestEnum.String, 1)]
    [InlineData("Zhang", 0)]
    [InlineData("10", 10)]
    public void ToDecimal_Success(object value, int revalValue)
    {
      Assert.Equal(value.ToDecimal(), revalValue);
    }

    [Theory(DisplayName = "Object转Decimal，带默认值")]
    [InlineData(null, 6.6d)]
    [InlineData(TestEnum.Other, 3d)]
    [InlineData("Zhang", 8.8d)]
    [InlineData("10", 10d)]
    public void ToDecimal_Default_Success(object value, int defaultValue)
    {
      Assert.Equal(value.ToDecimal(defaultValue), defaultValue);
    }

    [Theory(DisplayName = "Object转DateTime")]
    [InlineData(null, "0001-01-01 00:00:00")]
    [InlineData("2021-06-11", "2021-06-11")]
    public void ToDateTime_Success(object value, DateTime revalValue)
    {
      Assert.Equal(value.ToDateTime(), revalValue);
    }

    [Theory(DisplayName = "Object转DateTime，带默认值")]
    [InlineData(null, "2021-06-11")]
    [InlineData("2021-06-11", "2021-06-11")]
    public void ToDateTime_Default_Success(object value, DateTime defaultValue)
    {
      Assert.Equal(value.ToDateTime(defaultValue), defaultValue);
    }

    [Theory(DisplayName = "Object转Double")]
    [InlineData("false", false)]
    [InlineData("true", true)]
    [InlineData("Zhang", false)]
    public void ToBool_Success(object value, bool revalValue)
    {
      Assert.Equal(value.ToBool(), revalValue);
    }
  }
}
