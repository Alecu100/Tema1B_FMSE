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
                if (symbolSyntaxNode.Id == " " || symbolSyntaxNode.Id == "and" || symbolSyntaxNode.Id == "or" ||
                    symbolSyntaxNode.Id == "imp" || symbolSyntaxNode.Id == "not")
                {
                    _isFinishedReading = true;
                }
                else
                {
                    Id += symbolSyntaxNode.Id;
                    EndIndex = symbolSyntaxNode.EndIndex;
                }
            }
        }
    }
}