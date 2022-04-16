// See https://aka.ms/new-console-template for more information

const char Lambda = 'λ';

Console.WriteLine("Hello, World!");

var λnΛfΛxFNFX = "λn.λf.λx.f (n f x)";
var λfΛxFFXSqr = "(((λf . (λx . (f (f x)))) sqr) 3)";


LambdaSyntaxNode Parse(Span<char> expression)
{
    LambdaSyntaxNode result = null;
    var index = 0;

    switch (expression[index])
    {
        case Lambda:
            throw new NotImplementedException();
        case '(':
            LambdaSyntaxNode innerStatement = Parse(expression.Slice(index + 1));
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

//AbstractionLambdaSyntaxNode ParseAbstractionLambdaSyntaxNode(Span<char> expression)
//{
//    if (expression.Length == 0 || expression[0] != Lambda)
//        throw new Exception($"Unexpected char at {expression.}")
//}

public class LambdaSyntaxTree
{
    public LambdaSyntaxNode Root { get; }

    public LambdaSyntaxTree(LambdaSyntaxNode root)
    {
        Root = root;
        
    }
}