namespace Tema1B_FMSE.SyntaxNodes
{
    public class UnaryExpressionSyntaxNode : ValueSyntaxNode
    {
        public LiteralSyntaxNode Operator { get; set; }

        public EOperationKinds OperationKind { get; set; }

        public ValueSyntaxNode Value { get; set; }
    }
}