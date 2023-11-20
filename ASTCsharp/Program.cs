using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

string code = @"
class Program
{
    static void Main()
    {
        Console.WriteLine(\'Hello, Roslyn!\');
    }

    int test()
    {
        int i = 1+1;
        return i;
    }
}
";

// コードを構文ツリーに変換
SyntaxTree tree = CSharpSyntaxTree.ParseText(code);

// ルートノードの取得
var root = (CompilationUnitSyntax)tree.GetRoot();

// 新しいメソッド呼び出しノードを作成
var newStatement = SyntaxFactory.ExpressionStatement(
    SyntaxFactory.InvocationExpression(
        SyntaxFactory.MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            SyntaxFactory.IdentifierName("Console"),
            SyntaxFactory.IdentifierName("WriteLine")),
        SyntaxFactory.ArgumentList(
            SyntaxFactory.SingletonSeparatedList(
                SyntaxFactory.Argument(
                    SyntaxFactory.LiteralExpression(
                        SyntaxKind.StringLiteralExpression,
                        SyntaxFactory.Literal("Modified code")))))));


// 既存のステートメントを新しいステートメントで置き換える
var newRoot = root.ReplaceNode(root.DescendantNodes().OfType<ExpressionStatementSyntax>().First(), newStatement);

// 変更を適用したコードを取得
var newCode = newRoot.ToFullString();
Console.WriteLine(newCode);