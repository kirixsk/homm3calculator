namespace Homm3.Domain.Calculator
{
    public static class AdjustedValueCalculator
    {
        public static int CalculateAdjustedValue(int rawValue, int protectionIndex)
        {
            ProtectionIndexHelper.GetValueCoefficients(
                protectionIndex,
                out int minimalValue1,
                out int minimalValue2,
                out float coefficient1,
                out float coefficient2);
            var adjustedValue = (int)(Math.Max(rawValue - minimalValue1, 0) * coefficient1 + Math.Max(rawValue - minimalValue2, 0) * coefficient2);
            return adjustedValue;
        }
    }
}
