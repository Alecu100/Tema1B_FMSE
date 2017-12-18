using System.Collections.Generic;

namespace Tema1B_FMSE.SyntaxNodes
{
    public abstract class SyntaxNode
    {
        protected bool _isFinishedReading;

        public SyntaxNode Parent { get; set; }

        public bool IsFinishedReading
        {
            get { return _isFinishedReading; }
            set { _isFinishedReading = value; }
        }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public abstract void AssignChild(SyntaxNode child);

        public abstract IEnumerable<SyntaxNode> Children { get; }

        public string Name
        {
            get { return GetType().Name; }
        }
    }
}