using System;


namespace R5T.S0023
{
    public static class HumanActionsRequiredExtensions
    {
        public static bool Any(this HumanActionsRequired humanActionsRequired)
        {
            var output = false
                || humanActionsRequired.ReviewDepartedProjects
                || humanActionsRequired.ReviewNewDuplicateProjectNames
                || humanActionsRequired.ReviewNewProjects
                ;

            return output;
        }
    }
}
