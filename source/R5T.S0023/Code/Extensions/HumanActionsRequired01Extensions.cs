using System;


namespace R5T.S0023
{
    public static class HumanActionsRequired01Extensions
    {
        public static bool Any(this HumanActionsRequired01 humanActionsRequired)
        {
            var output = false
                || humanActionsRequired.ReviewDepartedProjects
                || humanActionsRequired.ReviewNewProjects
                ;

            return output;
        }

        public static bool AnyMandatory(this HumanActionsRequired01 humanActionsRequired)
        {
            // None are mandatory.
            return false;
        }

        public static void UnsetNonMandatory(this HumanActionsRequired01 humanActionsRequired)
        {
            humanActionsRequired.ReviewDepartedProjects = false;
            humanActionsRequired.ReviewNewProjects = false;
        }
    }
}
