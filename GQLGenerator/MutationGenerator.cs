//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using System.Collections.Immutable;

//namespace GQLGenerator;

//[Generator]
//public class MutationGenerator : IIncrementalGenerator
//{
//    public void Initialize(IncrementalGeneratorInitializationContext context)
//    {

//        //context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
//        //  "GenerateServiceAttribute.g.cs",
//        //  SourceText.From(SourceGenerationHelper.Attribute, Encoding.UTF8)));

//        var provider = context.SyntaxProvider.CreateSyntaxProvider(
//            predicate: static (node, _) => node is ClassDeclarationSyntax,
//            transform: static (ctx, _) => (ClassDeclarationSyntax)ctx.Node
//            ).Where(m => m is not null);

//        var compilation = context.CompilationProvider.Combine(provider.Collect());
//        context.RegisterSourceOutput(compilation, Execute);
//    }

//    private void Execute(SourceProductionContext context, (Compilation left, ImmutableArray<ClassDeclarationSyntax> Right) tuple)
//    {
//        //Debugger.Launch();
//        var (compilation, list) = tuple;

//        var nameList = new List<string>();

//        foreach (var syntax in list)
//        {

//            var symbol = compilation
//                .GetSemanticModel(syntax.SyntaxTree)
//                .GetDeclaredSymbol(syntax) as INamedTypeSymbol;

//            nameList.Add($"\"{symbol.ToDisplayString()}\"");
//        }

//        var names = String.Join(",\n    ", nameList);


//        var theCode = $$"""

//            namespace GQLGeneratorCode;

//            public class MutationGeneratedClass{
//             public static List<string> Names = new(){
//             "Hello",
//             "Rafiul",
//             "Islam",
//             "Shohan",
//             };
//            }
//            """;
//        context.AddSource("MutationGeneratedClass.g.cs", theCode);
//    }
//}
