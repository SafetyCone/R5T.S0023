using System;
using System.Collections.Generic;

using R5T.T0045;
using R5T.T0097;


namespace R5T.S0023.Library
{
    public static class ICodeFileGeneratorExtensions
    {
        public static void CreateIProjectPathExtensions(this ICodeFileGenerator _,
            IEnumerable<Project> projects,
            string namespaceName,
            string filePath)
        {
            var class1CompilationUnit = Instances.CompilationUnitGenerator.GetIProjectPathExtensions(
                projects,
                namespaceName);

            class1CompilationUnit.WriteTo(filePath);
        }
    }
}
