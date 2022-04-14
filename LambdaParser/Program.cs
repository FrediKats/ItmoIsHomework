// See https://aka.ms/new-console-template for more information
const char Lambda = 'λ';

Console.WriteLine("Hello, World!");

var λnΛfΛxFNFX = "λn.λf.λx.f (n f x)";



void Parse(Span<char> expression)
{
    switch (expression[0])
    {
        case Lambda:
            throw new NotImplementedException();
        case '(':
            throw new NotImplementedException();
    }
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

public class LambdaSyntaxTree
{
    public LambdaSyntaxNode Root { get; }

    public LambdaSyntaxTree(LambdaSyntaxNode root)
    {
        Root = root;
    }
}