using System.Collections.Generic;
using PolSl.UrbanHealthPath.UserInterface.Initializers;

namespace PolSl.UrbanHealthPath.UserInterface.Views
{
    public class History
    {
        private struct HistoryElement
        {
            public ViewType Type { get; }
            public Initializer Data { get;  }

            public HistoryElement(ViewType type, Initializer data)
            {
                Type = type;
                Data = data;
            }
        }
        
        private Stack<HistoryElement> history;

        public void AddToHistory(ViewType type, Initializer data)
        {
            history.Push(new HistoryElement(type,data));
        }

        public (ViewType, Initializer) GetLastView()
        {
            var element = history.Pop();
            return (element.Type, element.Data);
        }
    }
}