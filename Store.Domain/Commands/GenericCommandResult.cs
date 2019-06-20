using System.Collections.Generic;
using System.Linq;
using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands
{
    public class GenericCommandResult : ICommandResult
    {
        private List<string> _messages;

        public GenericCommandResult()
        {
            _messages = new List<string>();
        }

        public bool Success => _messages.Any() == false;
        public IReadOnlyList<string> Messages => _messages;
        public object Data { get; set; }

        public void Fail(string message)
        {
            this._messages.Add(message);
        }

        public void Fail(IEnumerable<string> messages)
        {
            this._messages.AddRange(messages);
        }
    }
}
