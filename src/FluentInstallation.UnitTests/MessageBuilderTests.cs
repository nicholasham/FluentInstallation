using Xunit;

namespace FluentInstallation
{
    public class MessageBuilderTests
    {
        [Fact]
        public void Constructor_InitialisesAnEmptyMessage()
        {
            var builder = new MessageBuilder();
            var actual = builder.Build();
            Assert.Equal(string.Empty, actual);
        }

        [Fact]
        public void WriteLine_WithDefaultIndentWritesTheStandardIndent()
        {
            var builder = new MessageBuilder();
            builder.WriteLine("Some text");

            var actual = builder.Build();
            
            Assert.Equal("    Some text\r\n", actual);
        }

        [Fact]
        public void IncreaseIndent_IncreasesIndentByAFactorOf1()
        {
            var builder = new MessageBuilder();
            builder.IncreaseIndent();
            builder.WriteLine("Some text");
            var actual = builder.Build();

            Assert.Equal("        Some text\r\n", actual);
        }

        [Fact]
        public void DecreaseIndent_DecreasesIndentByAFactorOf1()
        {
            var builder = new MessageBuilder();
            builder.IncreaseIndent();
            builder.DecreaseIndent();

            builder.WriteLine("Some text");

            var actual = builder.Build();

            Assert.Equal("    Some text\r\n", actual);
        }
    }
}