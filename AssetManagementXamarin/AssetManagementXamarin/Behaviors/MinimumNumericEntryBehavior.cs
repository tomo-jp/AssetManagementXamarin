namespace AssetManagementXamarin.Behaviors
{
    internal class MinimumNumericEntryBehavior : PlainNumericEntryBehavior
    {
        public double MinimumValue { get; set; } = 0;

        public MinimumNumericEntryBehavior()
        {
            AdditionalCheck = ValidateMinimum;
        }

        private bool ValidateMinimum(double input)
        {
            if (input < MinimumValue)
                return false;

            return true;
        }
    }
}
