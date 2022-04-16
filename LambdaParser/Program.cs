// See https://aka.ms/new-console-template for more information

using System.Collections.Immutable;

const char Lambda = 'λ';

Console.WriteLine("Hello, World!");

var λnΛfΛxFNFX = "λn.λf.λx.f (n f x)";
var λfΛxFFXSqr = "(((λf . (λx . (f (f x)))) sqr) 3)";


LambdaSyntaxNode Parse(Span<char> expression)
{
    switch (expression[0])
    {
        case Lambda:
            throw new NotImplementedException();
        case '(':
            throw new NotImplementedException();
        case ')':
            throw new NotImplementedException();
    }

    throw new NotImplementedException();
}

public class NodeLocation
{
    public int Start { get; }
    public int End { get; }
    public int Length => End - Start + 1;

    public NodeLocation(int start, int end)
    {
        if (start < end)
            throw new ArgumentException($"Location end is lower that start. [{start}..{end}]");

        Start = start;
        End = end;
    }
}

public abstract class LambdaSyntaxNode
{
    public NodeLocation Location { get; }

    protected LambdaSyntaxNode(NodeLocation location)
    {
        Location = location;
    }
}

public abstract class ExpressionLambdaSyntaxNode : LambdaSyntaxNode
{
    protected ExpressionLambdaSyntaxNode(NodeLocation location) : base(location)
    {
    }
}

public class LetterLambdaSyntaxNode : ExpressionLambdaSyntaxNode
{
    public string Value { get; }

    public LetterLambdaSyntaxNode(NodeLocation location, string value) : base(location)
    {
        Value = value;
    }
}

public class FuncInvokeSyntaxNode : ExpressionLambdaSyntaxNode
{
    public LetterLambdaSyntaxNode FunctionName { get; }
    public ImmutableArray<ExpressionLambdaSyntaxNode> Arguments { get; }

    public FuncInvokeSyntaxNode(NodeLocation location, LetterLambdaSyntaxNode functionName, ImmutableArray<ExpressionLambdaSyntaxNode> arguments) : base(location)
    {
        FunctionName = functionName;
        Arguments = arguments;
    }
}

public class AbstractionLambdaSyntaxNode : LambdaSyntaxNode
{
    public LetterLambdaSyntaxNode Argument { get; }
    public LambdaSyntaxNode Body { get; }

    public AbstractionLambdaSyntaxNode(NodeLocation location, LetterLambdaSyntaxNode argument, LambdaSyntaxNode body) : base(location)
    {
        Argument = argument;
        Body = body;
    }
}

public class LambdaSyntaxTree
{
    public LambdaSyntaxNode Root { get; }

    public LambdaSyntaxTree(LambdaSyntaxNode root)
    {
        Root = root;
    }
}