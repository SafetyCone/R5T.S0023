using System;


namespace R5T.S0023
{
    public static class HumanActionsRequired03Extensions
    {
        public static bool Any(this HumanActionsRequired03 humanActionsRequired)
        {
            var output = false
                || humanActionsRequired.ReviewDepartedSelectedNames
                || humanActionsRequired.ReviewNewSelectedNames
                ;

            return output;
        }

        public static bool AnyMandatory(this HumanActionsRequired03 humanActionsRequired)
        {
            // None are mandatory.
            return false;
        }

        public static void UnsetNonMandatory(this HumanActionsRequired03 humanActionsRequired)
        {
            humanActionsRequired.ReviewDepartedSelectedNames = false;
            humanActionsRequired.ReviewNewSelectedNames = false;
        }
    }
}
