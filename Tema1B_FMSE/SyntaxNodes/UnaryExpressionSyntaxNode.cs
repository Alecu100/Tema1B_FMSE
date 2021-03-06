﻿using System.Collections.Generic;

namespace Tema1B_FMSE.SyntaxNodes
{
    public class UnaryExpressionSyntaxNode : ValueSyntaxNode
    {
        public LiteralSyntaxNode Operator { get; set; }

        public EOperationKinds OperationKind { get; set; }

        public ValueSyntaxNode InnerValue { get; set; }
        public override void AssignChild(SyntaxNode child)
        {
            if (Operator == null)
            {
                var literalSyntaxNode = (LiteralSyntaxNode) child;

                OperationKind = OperatorsInformation.MappedUnaryOperationKinds[literalSyntaxNode.LiteralValue];

                Operator = literalSyntaxNode;
                literalSyntaxNode.Parent = this;
                StartIndex = literalSyntaxNode.StartIndex;
                EndIndex = literalSyntaxNode.EndIndex;

                return;
            }

            if (InnerValue == null)
            {
                var valueSyntaxNode = (ValueSyntaxNode) child;

                InnerValue = valueSyntaxNode;
                valueSyntaxNode.Parent = this;
                EndIndex = valueSyntaxNode.EndIndex;
                _isFinishedReading = true;

                AssignSymbolKindsToValues();
            }
        }

        private void AssignSymbolKindsToValues()
        {
            var symbolKind = OperatorsInformation.MappedOperationInputs[OperationKind];

            InnerValue.SymbolKind = symbolKind;
        }

        public override IEnumerable<SyntaxNode> Children
        {
            get
            {
                return new List<SyntaxNode> { Operator, InnerValue };
            }
        }
    }
}