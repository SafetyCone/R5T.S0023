using System;

using R5T.T0037;

using Instances = R5T.S0023.Library.Instances;


namespace System
{
    public static class CodeFileNameExtensions
    {
        public static string IProjectPathExtensions(this ICodeFileName _)
        {
            var output = _.GetCSharpFileNameForTypeName(
                Instances.TypeName.IProjectPathExtensions());

            return output;
        }
    }
}
