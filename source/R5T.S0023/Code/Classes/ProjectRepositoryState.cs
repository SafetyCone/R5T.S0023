using System;
using System.Collections.Generic;

using R5T.T0097;


namespace R5T.S0023
{
    public class ProjectRepositoryState
    {
        public List<Project> Projects { get; }  = new();
        public List<string> IgnoredNames { get; } = new();
        public List<ProjectNameSelection> DuplicateNameSelections { get; } = new();
        public List<ProjectNameSelection> NameSelections { get; } = new();
    }
}
