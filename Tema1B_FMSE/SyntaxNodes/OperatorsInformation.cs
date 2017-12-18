using System.Collections.Generic;

namespace Tema1B_FMSE.SyntaxNodes
{
    public class OperatorsInformation
    {
        public static Dictionary<string, EOperationKinds> MappedOperationKinds = new Dictionary<string, EOperationKinds>
        {
            {"add", EOperationKinds.Add},
            {"mul", EOperationKinds.Multiply},
            {"and", EOperationKinds.And},
            {"imp", EOperationKinds.Implication},
            {"or", EOperationKinds.Or},
            {"not", EOperationKinds.Not},
            {"div", EOperationKinds.Divide},
            {"sub", EOperationKinds.Substract},
            {"ls", EOperationKinds.Less},
            {"lse", EOperationKinds.LessOrEqual},
            {"eq", EOperationKinds.Equal},
            {"gte", EOperationKinds.GreaterOrEqual},
            {"gt", EOperationKinds.Greater},
            {"min", EOperationKinds.Minus},
            {"any", EOperationKinds.Any },
            {"exists", EOperationKinds.Exists }
        };

        public static Dictionary<EOperationKinds, ESymbolKinds> MappedOperationResults =
            new Dictionary<EOperationKinds, ESymbolKinds>
            {
                {EOperationKinds.Add, ESymbolKinds.Integer},
                {EOperationKinds.Multiply, ESymbolKinds.Integer},
                {EOperationKinds.And, ESymbolKinds.Boolean},
                {EOperationKinds.Implication, ESymbolKinds.Boolean},
                {EOperationKinds.Or, ESymbolKinds.Boolean},
                {EOperationKinds.Not, ESymbolKinds.Boolean},
                {EOperationKinds.Divide, ESymbolKinds.Integer},
                {EOperationKinds.Substract, ESymbolKinds.Integer},
                {EOperationKinds.Less, ESymbolKinds.Boolean},
                {EOperationKinds.LessOrEqual, ESymbolKinds.Boolean},
                {EOperationKinds.Equal, ESymbolKinds.Boolean},
                {EOperationKinds.GreaterOrEqual, ESymbolKinds.Boolean},
                {EOperationKinds.Greater, ESymbolKinds.Boolean},
                {EOperationKinds.Minus, ESymbolKinds.Integer},
                {EOperationKinds.Any, ESymbolKinds.Any },
                {EOperationKinds.Exists, ESymbolKinds.Any }
            };

        public static Dictionary<EOperationKinds, ESymbolKinds> MappedOperationInputs =
            new Dictionary<EOperationKinds, ESymbolKinds>
            {
                {EOperationKinds.Add, ESymbolKinds.Integer},
                {EOperationKinds.Multiply, ESymbolKinds.Integer},
                {EOperationKinds.And, ESymbolKinds.Boolean},
                {EOperationKinds.Implication, ESymbolKinds.Boolean},
                {EOperationKinds.Or, ESymbolKinds.Boolean},
                {EOperationKinds.Not, ESymbolKinds.Boolean},
                {EOperationKinds.Divide, ESymbolKinds.Integer},
                {EOperationKinds.Substract, ESymbolKinds.Integer},
                {EOperationKinds.Less, ESymbolKinds.Integer},
                {EOperationKinds.LessOrEqual, ESymbolKinds.Integer},
                {EOperationKinds.Equal, ESymbolKinds.Integer},
                {EOperationKinds.GreaterOrEqual, ESymbolKinds.Integer},
                {EOperationKinds.Greater, ESymbolKinds.Integer},
                {EOperationKinds.Minus, ESymbolKinds.Integer},
                {EOperationKinds.Any, ESymbolKinds.Any },
                {EOperationKinds.Exists, ESymbolKinds.Any }
            };

        public static List<EOperationKinds> UnaryOperations = new List<EOperationKinds>
        {
            EOperationKinds.Minus,
            EOperationKinds.Not,
            EOperationKinds.Any,
            EOperationKinds.Exists
        };

        public static Dictionary<string, EOperationKinds> MappedBinaryOperationKinds = new Dictionary<string, EOperationKinds>
        {
            {"add", EOperationKinds.Add},
            {"mul", EOperationKinds.Multiply},
            {"and", EOperationKinds.And},
            {"imp", EOperationKinds.Implication},
            {"or", EOperationKinds.Or},
            {"div", EOperationKinds.Divide},
            {"sub", EOperationKinds.Substract},
            {"ls", EOperationKinds.Less},
            {"lse", EOperationKinds.LessOrEqual},
            {"eq", EOperationKinds.Equal},
            {"gte", EOperationKinds.GreaterOrEqual},
            {"gt", EOperationKinds.Greater},
        };

        public static Dictionary<string, EOperationKinds> MappedUnaryOperationKinds = new Dictionary<string, EOperationKinds>
        {
            {"not", EOperationKinds.Not},
            {"min", EOperationKinds.Minus},
            {"any", EOperationKinds.Any},
            {"exists", EOperationKinds.Exists}
        };
    }
}
