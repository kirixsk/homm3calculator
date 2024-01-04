namespace Homm3.Domain.Calculator
{
    public static class ProtectionIndexHelper
    {
        public static void GetValueCoefficients(
            int protectionIndex,
            out int minimalValue1,
            out int minimalValue2,
            out float coefficient1,
            out float coefficient2)
        {
            switch (protectionIndex)
            {
                case 1:
                    minimalValue1 = 2500;
                    coefficient1 = 0.5f;
                    minimalValue2 = 7500;
                    coefficient2 = 0.5f;
                    break;

                case 2:
                    minimalValue1 = 1500;
                    coefficient1 = 0.75f;
                    minimalValue2 = 7500;
                    coefficient2 = 0.75f;
                    break;

                case 3:
                    minimalValue1 = 1000;
                    coefficient1 = 1f;
                    minimalValue2 = 7500;
                    coefficient2 = 1f;
                    break;

                case 4:
                    minimalValue1 = 500;
                    coefficient1 = 1.5f;
                    minimalValue2 = 5000;
                    coefficient2 = 1f;
                    break;

                case 5:
                    minimalValue1 = 0;
                    coefficient1 = 1.5f;
                    minimalValue2 = 5000;
                    coefficient2 = 1.5f;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(protectionIndex));
            }
        }
    }
}
