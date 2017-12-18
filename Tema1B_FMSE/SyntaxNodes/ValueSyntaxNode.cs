namespace Tema1B_FMSE.SyntaxNodes
{
    public abstract class ValueSyntaxNode : SyntaxNode
    {
        public ESymbolKinds SymbolKind { get; set; } 
    }
}