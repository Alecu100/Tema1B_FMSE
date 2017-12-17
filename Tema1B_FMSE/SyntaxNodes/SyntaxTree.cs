namespace Tema1B_FMSE.SyntaxNodes
{
    public class SyntaxTree : SyntaxNode
    {
        public SyntaxNode RootValue { get; set; }

        public override void AssignChild(SyntaxNode child)
        {
            RootValue = child;
        }
    }
}