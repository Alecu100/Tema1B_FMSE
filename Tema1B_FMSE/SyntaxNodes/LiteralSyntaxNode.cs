namespace Tema1B_FMSE.SyntaxNodes
{
    public class LiteralSyntaxNode : SyntaxNode
    {
        public override void AssignChild(SyntaxNode child)
        {
            
        }

        public string LiteralValue { get; set; }
    }
}