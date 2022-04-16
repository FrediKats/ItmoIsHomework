﻿using System.Collections.Immutable;
using LambdaParser.Syntax.Nodes;

namespace LambdaParser.Semantic.Nodes;

public abstract class ExpressionLambdaSemanticNode
{
    public abstract ExpressionLambdaSyntaxNode GetSyntax();

    public virtual ImmutableArray<ExpressionLambdaSemanticNode> GetChildren()
    {
        return ImmutableArray<ExpressionLambdaSemanticNode>.Empty;
    }
}