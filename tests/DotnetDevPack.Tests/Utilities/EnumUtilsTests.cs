namespace DotnetDevPack.Tests
{
  using System.ComponentModel;
  using DotnetDevPack.Utilities;
  using Xunit;

  public class EnumUtilsTests
  {
    [Theory(DisplayName = "获取Enum类型的Description")]
    [InlineData(TestEnum.Number, "数字")]
    [InlineData(TestEnum.String, "字符串")]
    [InlineData(TestEnum.Bool, "布尔")]
    [InlineData(TestEnum.Other, "DotnetDevPack.Tests.TestEnum.Other")]
    public void GetEnumDescription_Success(TestEnum testEnum, string description) => Assert.Equal(description, testEnum.GetDescription());
  }

  public enum TestEnum
  {
    [Description("数字")]
    Number = 0,

    [Description("字符串")]
    String = 1,

    [Description("布尔")]
    Bool = 2,

    Other = 3
  }
}
