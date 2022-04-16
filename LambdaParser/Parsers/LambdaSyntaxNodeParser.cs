using LambdaParser.LambdaSyntaxNodes;
using Microsoft.Extensions.Primitives;

namespace LambdaParser.Parsers;

public class LambdaSyntaxNodeParser
{
    public static LambdaSyntaxNode Parse(StringSegment expression)
    {
        LambdaSyntaxNode result = null;
        var index = 0;

        switch (expression[index])
        {
            case Constants.Lambda:
                throw new NotImplementedException();
            case '(':
                LambdaSyntaxNode innerStatement = Parse(expression.Subsegment(index + 1));
                index = innerStatement.Location.End + 1;

                //TODO:rework
                result = innerStatement;
                break;

            case ')':
                if (expression.Length == index)
                    return result;

                throw new NotImplementedException("Do not currently support brackets inside term");
        }

        throw new NotImplementedException();
    }
}