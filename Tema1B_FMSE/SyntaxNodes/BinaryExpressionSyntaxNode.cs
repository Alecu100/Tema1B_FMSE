using System.Collections.Generic;
using System.Threading;

namespace Tema1B_FMSE.SyntaxNodes
{
    public class BinaryExpressionSyntaxNode : ValueSyntaxNode
    {
        public LiteralSyntaxNode Operator { get; set; }

        public ValueSyntaxNode LeftValue { get; set; }

        public ValueSyntaxNode RightValue { get; set; }

        public EOperationKinds OperationKind { get; set; }

        public override void AssignChild(SyntaxNode child)
        {
            if (LeftValue == null)
            {
                var valueSyntaxNode = (ValueSyntaxNode) child;
                LeftValue = valueSyntaxNode;
                valueSyntaxNode.Parent = this;

                StartIndex = valueSyntaxNode.StartIndex;
                EndIndex = valueSyntaxNode.EndIndex;

                return;
            }

            if (Operator == null)
            {
                var literalSyntaxNode = (LiteralSyntaxNode)child;

                OperationKind = OperatorsInformation.MappedOperationKinds[literalSyntaxNode.LiteralValue];
                Operator = literalSyntaxNode;
                literalSyntaxNode.Parent = this;
                EndIndex = literalSyntaxNode.EndIndex;
                AssignSymbolKindToThis();

                return;
            }

            if (RightValue == null)
            {
                var valueSyntaxNode = (ValueSyntaxNode)child;
                RightValue = valueSyntaxNode;
                valueSyntaxNode.Parent = this;
                EndIndex = valueSyntaxNode.EndIndex;
                _isFinishedReading = true;

                AssignSymbolKindsToValues();
            }
        }

        private void AssignSymbolKindToThis()
        {
            SymbolKind = OperatorsInformation.MappedOperationResults[OperationKind];
        }

        private void AssignSymbolKindsToValues()
        {
            var symbolKind = OperatorsInformation.MappedOperationInputs[OperationKind];

            LeftValue.SymbolKind = symbolKind;
            RightValue.SymbolKind = symbolKind;
        }

        public override IEnumerable<SyntaxNode> Children
        {
            get
            {
                var children = new List<SyntaxNode>();

                if (DomainValue != null)
                {
                    children.Add(DomainValue);
                }

                children.Add(LeftValue);
                children.Add(Operator);
                children.Add(RightValue);

                return children;
            }
        }
    }
}