using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0045;
using R5T.T0097;


namespace R5T.S0023.Library
{
    public static class IMethodGeneratorExtensions
    {
        public static MethodDeclarationSyntax GetProjectPathExtension_WithoutMethodIndentation(this IMethodGenerator _,
            Project project)
        {
            var projectMethodName = Instances.ProjectNameOperator.GetMethodNameForProjectName(project.Name);

            var text = $@"
public static string {projectMethodName}(this {Instances.TypeName.IProjectPath()} _)
{{
    return ""{project.Identity}"";
}}
";

            var method = _.GetMethodDeclarationFromText_TrimOnly(text);
            return method;
        }

        public static MethodDeclarationSyntax GetProjectPathExtension(this IMethodGenerator _,
            Project project,
            bool prependNewLineToFirstToken = false)
        {
            var output = _.GetProjectPathExtension_WithoutMethodIndentation(project)
                .IndentBlock_Old(
                    Instances.Indentation.Method(),
                    prependNewLineToFirstToken);

            return output;
        }
    }
}
