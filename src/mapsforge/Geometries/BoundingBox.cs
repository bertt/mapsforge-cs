
namespace Mapsforge.Geometries
{
    public class BoundingBox
    {
        private double minX, minY, maxX, maxY;
        public BoundingBox(double minX, double minY, double maxX, double maxY)
        {
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
        }

        public double MinX => minX;

        public double MinY => minY;

        public double MaxX => maxX;

        public double MaxY => maxY;

    }
}
