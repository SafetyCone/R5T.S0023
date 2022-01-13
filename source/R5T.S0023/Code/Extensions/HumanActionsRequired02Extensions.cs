using System;


namespace R5T.S0023
{
    public static class HumanActionsRequired02Extensions
    {
        public static bool Any(this HumanActionsRequired02 humanActionsRequired)
        {
            var output = false
                || humanActionsRequired.ReviewDuplicateProjectNames
                ;

            return output;
        }

        public static bool AnyMandatory(this HumanActionsRequired02 humanActionsRequired)
        {
            var output = false
                || humanActionsRequired.ReviewDuplicateProjectNames
                ;

            return output;
        }

        public static void UnsetNonMandatory(this HumanActionsRequired02 _)
        {
            // Do nothing since all actions are mandatory.
        }
    }
}
