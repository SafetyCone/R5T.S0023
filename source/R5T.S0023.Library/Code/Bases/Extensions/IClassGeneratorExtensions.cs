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
            var indentation = Instances.Indentation.Method();

            var methods = projects
                .Select(xProject => Instances.MethodGenerator.GetProjectPathExtension(xProject)
                    .IndentBlock(indentation))
                .ToArray();

            var output = _.GetPublicStaticClass(
                Instances.TypeName.IProjectPathExtensions())
                .AddMembersWithLineSpacing(methods)
                ;

            return output;
        }
    }
}
