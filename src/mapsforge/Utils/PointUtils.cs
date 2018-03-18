namespace Mapsforge.Utils
{
    public sealed class PointUtils
    {
        private const double CONVERSION_FACTOR = 1000000.0;

        public static double MicrodegreesToDegrees(int coordinate)
        {
            return coordinate / CONVERSION_FACTOR;
        }
    }
}