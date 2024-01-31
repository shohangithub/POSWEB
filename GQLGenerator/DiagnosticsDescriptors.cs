using Microsoft.CodeAnalysis;

namespace GQLGenerator;

public static class DiagnosticsDescriptors
{
    public static readonly DiagnosticDescriptor ClassWithWrongNameMessage
        = new("ERR001",                                        // id
            "Worng name",                                      // title
            "The class '{0}' must be contains 'Model' prefix", // message
            "Generator",                                       // category
            DiagnosticSeverity.Error,
            true);
}
