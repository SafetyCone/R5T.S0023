﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using R5T.T0045;
using R5T.T0097;


namespace R5T.S0023.Library
{
    public static class ICodeFileGeneratorExtensions
    {
        public static async Task CreateIProjectPathExtensions(this ICodeFileGenerator _,
            IEnumerable<Project> projects,
            string namespaceName,
            string filePath)
        {
            var compilationUnit = Instances.CompilationUnitGenerator.GetIProjectPathExtensions(
                projects,
                namespaceName);

            await compilationUnit.WriteTo(filePath);
        }
    }
}
