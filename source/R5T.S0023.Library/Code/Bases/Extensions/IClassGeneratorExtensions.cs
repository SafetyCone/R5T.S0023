using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0045;
using R5T.T0097;


namespace R5T.S0023.Library
{
    public static class IClassGeneratorExtensions
    {
        public static ClassDeclarationSyntax GetIProjectPathExtensions(this IClassGenerator _,
            IEnumerable<Project> projects)
        {
            var methods = projects
                .Select(xProject => Instances.MethodGenerator.GetProjectPathExtension(xProject))
                .ToArray();

            var output = _.GetPublicStaticClass(
                Instances.TypeName.IProjectPathExtensions())
                .AddMembersWithBlankLineSeparation(methods)
                ;

            return output;
        }
    }
}
