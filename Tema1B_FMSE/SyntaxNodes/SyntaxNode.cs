namespace Tema1B_FMSE.SyntaxNodes
{
    public abstract class SyntaxNode
    {
        protected bool _isFinishedReading;

        public SyntaxNode Parent { get; set; }

        public bool IsFinishedReading => _isFinishedReading;

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public abstract void AssignChild(SyntaxNode child);
    }
}