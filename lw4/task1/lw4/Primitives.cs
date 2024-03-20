using Drawing;
using OpenTK.Graphics.OpenGL;

namespace Primitives
{
    public enum Axis
    {
        X, Y, Z
    }

    public class Point
    {
        public float m_x;
        public float m_y;
        public float m_z;

        public Point(float x, float y, float z)
        {
            m_x = x;
            m_y = y;
            m_z = z;
        }
    }

    public class Square
    {
        public Point m_leftTop = new(0.0f, 0.0f, 0.0f);
        public Point m_rightTop = new(0.0f, 0.0f, 0.0f);
        public Point m_bottomLeft = new(0.0f, 0.0f, 0.0f);
        public Point m_bottomRight = new(0.0f, 0.0f, 0.0f);

        public Square(Point center, float squareLength, Axis axisByWidth, Axis axisByHeight)
        {
            float centreToEdgeDistance = squareLength / 2;
            if (axisByWidth == Axis.X && axisByHeight == Axis.Y)
            {
                InitializePointsForXYPlane(center, centreToEdgeDistance);
            }
            else if (axisByWidth == Axis.Z && axisByHeight == Axis.Y)
            {
                InitializePointsForZYPlane(center, centreToEdgeDistance);
            }
            else if (axisByWidth == Axis.Z && axisByHeight == Axis.X)
            {
                InitializePointsForZXPlane(center, centreToEdgeDistance);
            }
            else
            {
                throw new ArgumentException("Incorrect axis combination");
            }
        }

        private void InitializePointsForXYPlane(Point center, float centreToEdgeDistance)
        {
            // для X, Y: leftTop.x = c.x - len, leftTop.y = c.y + len
            // для X, Y: rightTop.x = c.x + len, rightTop.y = c.y + len
            // для X, Y: bottomLeft.x = c.x - len, bottomLeft.y = c.y - len
            // для X, Y: bottomRight.x = c.x + len, bottomRight.y = c.y - len
            m_leftTop.m_z = center.m_z;
            m_leftTop.m_x = center.m_x - centreToEdgeDistance;
            m_leftTop.m_y = center.m_y + centreToEdgeDistance;

            m_rightTop.m_z = center.m_z;
            m_rightTop.m_x = center.m_x + centreToEdgeDistance;
            m_rightTop.m_y = center.m_y + centreToEdgeDistance;

            m_bottomLeft.m_z = center.m_z;
            m_bottomLeft.m_x = center.m_x - centreToEdgeDistance;
            m_bottomLeft.m_y = center.m_y - centreToEdgeDistance;

            m_bottomRight.m_z = center.m_z;
            m_bottomRight.m_x = center.m_x + centreToEdgeDistance;
            m_bottomRight.m_y = center.m_y - centreToEdgeDistance;
        }

        private void InitializePointsForZYPlane(Point center, float centreToEdgeDistance)
        {
            // для Z, Y: leftTop.z = c.z - len, leftTop.y = c.y + len
            // для Z, Y: rightTop.z = c.z + len, rightTop.y = c.y + len
            // для Z, Y: bottomLeft.z = c.z - len, bottomLeft.y = c.y - len
            // для Z, Y: bottomRight.z = c.z + len, bottomRight.y = c.y - len
            m_leftTop.m_x = center.m_x;
            m_leftTop.m_z = center.m_z - centreToEdgeDistance;
            m_leftTop.m_y = center.m_y + centreToEdgeDistance;

            m_rightTop.m_x = center.m_x;
            m_rightTop.m_z = center.m_z + centreToEdgeDistance;
            m_rightTop.m_y = center.m_y + centreToEdgeDistance;

            m_bottomLeft.m_x = center.m_x;
            m_bottomLeft.m_z = center.m_z - centreToEdgeDistance;
            m_bottomLeft.m_y = center.m_y - centreToEdgeDistance;

            m_bottomRight.m_x = center.m_x;
            m_bottomRight.m_z = center.m_z + centreToEdgeDistance;
            m_bottomRight.m_y = center.m_y - centreToEdgeDistance;
        }

        private void InitializePointsForZXPlane(Point center, float centreToEdgeDistance)
        {
            // для Z, X: leftTop.z = c.z + len, leftTop.x = c.x - len
            // для Z, X: rightTop.z = c.z + len, rightTop.x = c.x + len
            // для Z, X: bottomLeft.z = c.z - len, bottomLeft.x = c.x - len
            // для Z, X: bottomRight.z = c.z - len, bottomRight.x = c.x + len
            m_leftTop.m_y = center.m_y;
            m_leftTop.m_z = center.m_z + centreToEdgeDistance;
            m_leftTop.m_x = center.m_x - centreToEdgeDistance;

            m_rightTop.m_y = center.m_y;
            m_rightTop.m_z = center.m_z + centreToEdgeDistance;
            m_rightTop.m_x = center.m_x + centreToEdgeDistance;

            m_bottomLeft.m_y = center.m_y;
            m_bottomLeft.m_z = center.m_z - centreToEdgeDistance;
            m_bottomLeft.m_x = center.m_x - centreToEdgeDistance;

            m_bottomRight.m_y = center.m_y;
            m_bottomRight.m_z = center.m_z - centreToEdgeDistance;
            m_bottomRight.m_x = center.m_x + centreToEdgeDistance;
        }
    }

    public class Polygon : Drawable
    {
        public Polygon(Point[] points, Color? color = null)
            : base(points, color)
        {
        }

        protected override PrimitiveType GetPrimiviteType()
        {
            return PrimitiveType.TriangleStrip;
        }
    }

    public class Triangle : Drawable
    {
        public Triangle(Point[] points, Color? color = null)
           : base(points, color)
        {
            if (points.Length != 3)
            {
                throw new ArgumentException("Triangle must contain 3 points");
            }
        }

        protected override PrimitiveType GetPrimiviteType()
        {
            return PrimitiveType.Triangles;
        }
    }
}
