using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

using System.Collections.Immutable;
using System.Text;

namespace GQLGenerator;

[Generator]
public sealed class ServiceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //Debugger.Launch();
        //context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
        //    "GenerateServiceAttribute.g.cs",
        //    SourceText.From(SourceGenerationHelper.Attribute, Encoding.UTF8)));

        IncrementalValuesProvider<ClassDeclarationSyntax> enumDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                transform: static (ctx, _) => GetTargetForGeneration(ctx));

        IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax>)> compilationAndEnums
                = context.CompilationProvider.Combine(enumDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndEnums,
            (spc, source) => Execute(source.Item1, source.Item2, spc));
    }

    public static bool IsSyntaxTargetForGeneration(SyntaxNode syntaxNode)
    {
        return syntaxNode is ClassDeclarationSyntax classDeclarationSyntax &&
            classDeclarationSyntax.AttributeLists.Count > 0 &&
            classDeclarationSyntax.AttributeLists
                .Any(al => al.Attributes
                    .Any(a => a.Name.ToString() == "GraphQLEntity"));
    }
    public static ClassDeclarationSyntax GetTargetForGeneration(GeneratorSyntaxContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

        return classDeclarationSyntax;
    }

    public void Execute(Compilation compilation,
    ImmutableArray<ClassDeclarationSyntax> classes,
    SourceProductionContext context)
    {
        //Debugger.Launch();
        foreach (var classSyntax in classes)
        {
            // Converting the class to a semantic model to access much more meaningful data.
            var model = compilation.GetSemanticModel(classSyntax.SyntaxTree);

            // Parse to declared symbol, so you can access each part of code separately,
            // such as interfaces, methods, members, contructor parameters etc.
            var symbol = model.GetDeclaredSymbol(classSyntax);

            var className = symbol.Name;

            if (!className.Contains("Model"))
            {
                var error = Diagnostic.Create(DiagnosticsDescriptors.ClassWithWrongNameMessage,
                    classSyntax.Identifier.GetLocation(),
                    className);

                context.ReportDiagnostic(error);

                return;
            }

            var classNamespace = symbol.ContainingNamespace?.ToDisplayString();

            var classAssembly = symbol.ContainingAssembly?.Name;
            // Get the template string
            var text = $$"""

                using System.Linq;          
                using System.Collections.Generic;
                

                namespace GQLGeneratorCode
                {
                    public partial class {{className}}Service
                    {
                        private static readonly List<{{className}}> _list = new();

                        public virtual List<{{className}}> All()
                        {
                            return new();
                        }

                    //    public virtual void Add({{className}} item)
                    //    {
                    //        _list.Add(item);
                    //    }

                    ////    public virtual void Update({{className}} item)
                    ////    {
                    ////        var existing = _list.Single(x => x.Id == item.Id);

                    ////        _list.Remove(existing);
                    ////        _list.Add(item);
                    ////    }

                    ////    public virtual void Delete(int id)
                    ////    {
                    ////        var existing = _list.Single(x => x.Id == id);

                    ////        _list.Remove(existing);
                    ////    }
                   }
                }

                """;

            //var template = Template.Parse(text);

            //var sourceCode = template.Render(new
            //{
            //    ClassName = className,
            //    ClassNamespace = classNamespace,
            //    ClassAssembly = classAssembly
            //});

            context.AddSource(
                $"{className}{"Service"}.g.cs",
                SourceText.From(text, Encoding.UTF8)
            );
        }
    }
}