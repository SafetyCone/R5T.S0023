using System;
using System.Threading.Tasks;

using R5T.Magyar;


namespace R5T.S0023
{
    public class OverridableProcessStartTimeProvider : IProcessStartTimeProvider
    {
        #region Static

        public static Override<DateTime> ProcessStartTimeOverride { get; set; }


        public static void Override(string yyyymmdd_hhmmss)
        {
            OverridableProcessStartTimeProvider.ProcessStartTimeOverride = DateTimeHelper.FromYYYYMMDD_HHMMSS(yyyymmdd_hhmmss);
        }

        #endregion


        private ICurrentProcessStartTimeProvider CurrentProcessStartTimeProvider { get; }


        public OverridableProcessStartTimeProvider(
            ICurrentProcessStartTimeProvider currentProcessStartTimeProvider)
        {
            this.CurrentProcessStartTimeProvider = currentProcessStartTimeProvider;
        }

        public async Task<DateTime> GetProcessStartTime()
        {
            // If overridden, use the overridden value.
            if(OverridableProcessStartTimeProvider.ProcessStartTimeOverride)
            {
                return OverridableProcessStartTimeProvider.ProcessStartTimeOverride;
            }

            var output = await this.CurrentProcessStartTimeProvider.GetCurrentProcessStartTime();
            return output;
        }
    }
}
