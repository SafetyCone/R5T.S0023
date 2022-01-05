using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0045;
using R5T.T0097;


namespace R5T.S0023.Library
{
    public static class ICompilationUnitGeneratorExtensions
    {
        public static CompilationUnitSyntax GetIProjectPathExtensions(this ICompilationUnitGenerator _,
            IEnumerable<Project> projects,
            string namespaceName)
        {
            var compilationUnit = _.InNewNamespace(
                namespaceName,
                (xNamespace, xNamespaceNames) =>
                {
                    var iProjectPathsNamespacedTypeName = Instances.NamespacedTypeName.IProjectPath();
                    var iProjectPathsNamespaceName = Instances.NamespacedTypeName.GetNamespaceName(iProjectPathsNamespacedTypeName);

                    xNamespaceNames.AddRange(
                        iProjectPathsNamespaceName);

                    var iProjectPathExtensions = Instances.ClassGenerator.GetIProjectPathExtensions(projects);

                    var outputNamespace = xNamespace.AddClass(iProjectPathExtensions);
                    return outputNamespace;
                });

            return compilationUnit;
        }
    }
}
