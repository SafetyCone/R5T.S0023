using System;

using R5T.T0034;
using R5T.T0036;
using R5T.T0045;
using R5T.T0090;
using R5T.T0112;


namespace R5T.S0023.Library
{
    public static class Instances
    {
        public static IClassGenerator ClassGenerator { get; } = T0045.ClassGenerator.Instance;
        public static ICompilationUnitGenerator CompilationUnitGenerator { get; } = T0045.CompilationUnitGenerator.Instance;
        public static IIndentation Indentation { get; } = T0036.Indentation.Instance;
        public static IJsonKey JsonKey { get; } = T0090.JsonKey.Instance;
        public static IMethodGenerator MethodGenerator { get; } = T0045.MethodGenerator.Instance;
        public static INamespacedTypeName NamespacedTypeName { get; } = T0034.NamespacedTypeName.Instance;
        public static IProjectNameOperator ProjectNameOperator { get; } = T0112.ProjectNameOperator.Instance;
        public static ITypeName TypeName { get; } = T0034.TypeName.Instance;
    }
}
