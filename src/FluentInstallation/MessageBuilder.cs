using System.Text;

namespace FluentInstallation
{
    public class MessageBuilder : IMessageBuilder
    {
        private const int IndentSize = 4;
        private readonly StringBuilder _builder = new StringBuilder();
        private int _indentLevel = 1;
        

        public IMessageBuilder Indent()
        {
            return this;
        }

        public IMessageBuilder DecreaseIndent()
        {
            return this;
        }

        public IMessageBuilder WriteLine(string text, params object[] args)
        {
            var indent = new string(' ', _indentLevel * IndentSize);
            _builder.AppendFormat(indent + text, args);
            _builder.AppendLine();
            return this;
        }

        public string Build()
        {
            return _builder.ToString();
        }
    }
}