using System;

namespace Tema1B_FMSE.SyntaxNodes
{
    public class SymbolSyntaxNode : ValueSyntaxNode
    {
        public string Id { get; set; }
        public override void AssignChild(SyntaxNode child)
        {

            if (!(child is SymbolSyntaxNode))
            {
                throw new SyntaxTreeParserException("Invalid character for symbol", StartIndex);
            }

            var symbolSyntaxNode = (SymbolSyntaxNode) child;

            if (!_isFinishedReading)
            {
                if (symbolSyntaxNode.Id == " ")
                {
                    this._isFinishedReading = true;
                }
                else
                {
                    this.Id += symbolSyntaxNode.Id;
                }
            }
        }
    }
}