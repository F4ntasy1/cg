using Drawing;
using Primitives;
using OpenTK.Graphics.OpenGL;

namespace Objects
{
    public class Rhombocuboctahedron : IDrawable
    {
        private readonly Square m_backSquare;
        private readonly Square m_frontSquare;
        private readonly Square m_leftSquare;
        private readonly Square m_rightSquare;
        private readonly Square m_topSquare;
        private readonly Square m_bottomSquare;

        private List<Polygon> m_polygons = [];
        private List<Triangle> m_triangles = [];

        private Color m_color;

        public Rhombocuboctahedron(float size, Color ?color = null)
        {
            float squareLength = size / 1.8f;
            float centerToSquareDistance = size / 1.5f;

            m_backSquare = new(new(0.0f, 0.0f, -centerToSquareDistance), squareLength, Axis.X, Axis.Y);
            m_frontSquare = new(new(0.0f, 0.0f, centerToSquareDistance), squareLength, Axis.X, Axis.Y);

            m_leftSquare = new(new(-centerToSquareDistance, 0.0f, 0.0f), squareLength, Axis.Z, Axis.Y);
            m_rightSquare = new(new(centerToSquareDistance, 0.0f, 0.0f), squareLength, Axis.Z, Axis.Y);

            m_topSquare = new(new(0.0f, centerToSquareDistance, 0.0f), squareLength, Axis.Z, Axis.X);
            m_bottomSquare = new(new(0.0f, -centerToSquareDistance, 0.0f), squareLength, Axis.Z, Axis.X);

            m_color = color ?? new(0.0f, 0.0f, 0.0f, 0.5f);

            InitializeBackPrimitives();
            InitializeFrontPrimitives();
        }

        public void SetColor(Color color)
        {
            m_color = color;
        }

        public void Draw()
        {
            List<Color> colors = [
                new(m_color.m_r, m_color.m_g, m_color.m_b, m_color.m_a),
                new(m_color.m_r, m_color.m_g, m_color.m_b - 0.2f, m_color.m_a + 0.05f),
                new(m_color.m_r, m_color.m_g, m_color.m_b + 0.05f, m_color.m_a + 0.15f)
            ];

            int colorIndex = 0;

            foreach (Polygon drawable in m_polygons)
            {
                drawable.SetColor(colors[colorIndex]);
                drawable.Draw();

                colorIndex++;
                if (colorIndex >= colors.Count) colorIndex = 0;
            }
            foreach (IDrawable drawable in m_triangles)
            {
                drawable.SetColor(colors[colorIndex]);
                drawable.Draw();

                colorIndex++;
                if (colorIndex >= colors.Count) colorIndex = 0;
            }
        }

        private void InitializeBackPrimitives()
        {
            // Back Top
            m_polygons.Add(new Polygon([
                m_backSquare.m_leftTop, m_backSquare.m_rightTop, 
                m_topSquare.m_bottomLeft, m_topSquare.m_bottomRight
            ]));
            return;
            m_polygons.Add(new Polygon([
                m_topSquare.m_bottomLeft, m_topSquare.m_bottomRight, m_topSquare.m_leftTop, m_topSquare.m_rightTop
            ]));
            // Top Back left
            m_triangles.Add(new Triangle([
                m_backSquare.m_leftTop, m_leftSquare.m_leftTop, m_topSquare.m_bottomLeft
            ]));
            // Top Back right
            m_triangles.Add(new Triangle([
                m_backSquare.m_rightTop, m_rightSquare.m_leftTop, m_topSquare.m_bottomRight
            ]));
            // Left
            m_polygons.Add(new Polygon([
                m_leftSquare.m_leftTop, m_leftSquare.m_rightTop, m_topSquare.m_bottomLeft, m_topSquare.m_leftTop
            ]));
            // Right
            m_polygons.Add(new Polygon([
                m_rightSquare.m_leftTop, m_rightSquare.m_rightTop, m_topSquare.m_bottomRight, m_topSquare.m_rightTop
            ]));
            // Bottom Back left
            m_triangles.Add(new Triangle([
                m_backSquare.m_bottomLeft, m_leftSquare.m_bottomLeft, m_bottomSquare.m_bottomLeft
            ]));
            // Bottom Back right
            m_triangles.Add(new Triangle([
                m_backSquare.m_bottomRight, m_rightSquare.m_bottomLeft, m_bottomSquare.m_bottomRight
            ]));
            // Bottom
            m_polygons.Add(new Polygon([
                m_bottomSquare.m_bottomLeft, m_bottomSquare.m_bottomRight,
                m_bottomSquare.m_leftTop, m_bottomSquare.m_rightTop
            ]));
            // Bottom Left
            m_polygons.Add(new Polygon([
                m_leftSquare.m_bottomLeft, m_leftSquare.m_bottomRight,
                m_bottomSquare.m_bottomLeft, m_bottomSquare.m_leftTop
            ]));
            // Bottom Right
            m_polygons.Add(new Polygon([
                m_rightSquare.m_bottomLeft, m_rightSquare.m_bottomRight,
                m_bottomSquare.m_bottomRight, m_bottomSquare.m_rightTop
            ]));
            // Bottom Back
            m_polygons.Add(new Polygon([
                m_backSquare.m_bottomLeft, m_backSquare.m_bottomRight,
                m_bottomSquare.m_bottomLeft, m_bottomSquare.m_bottomRight
            ]));
            // Back Side
            m_polygons.Add(new Polygon([
                m_backSquare.m_bottomLeft, m_backSquare.m_bottomRight,
                m_backSquare.m_leftTop, m_backSquare.m_rightTop
            ]));
            // Left Side
            m_polygons.Add(new Polygon([
                m_leftSquare.m_bottomLeft, m_leftSquare.m_bottomRight,
                m_leftSquare.m_leftTop, m_leftSquare.m_rightTop
            ]));
            // Right Side
            m_polygons.Add(new Polygon([
                m_rightSquare.m_bottomLeft, m_rightSquare.m_bottomRight,
                m_rightSquare.m_leftTop, m_rightSquare.m_rightTop
            ]));
            // Back Left
            m_polygons.Add(new Polygon([
                m_leftSquare.m_bottomLeft, m_backSquare.m_bottomLeft,
                m_leftSquare.m_leftTop, m_backSquare.m_leftTop
            ]));
            // Back Right
            m_polygons.Add(new Polygon([
                m_rightSquare.m_bottomLeft, m_backSquare.m_bottomRight,
                m_rightSquare.m_leftTop, m_backSquare.m_rightTop
            ]));
        }

        private void InitializeFrontPrimitives()
        {
            // Front top
            m_polygons.Add(new Polygon([
                m_frontSquare.m_rightTop, m_frontSquare.m_leftTop, m_topSquare.m_rightTop, m_topSquare.m_leftTop
            ]));
            // TODO: Сделать по часовой стрелке
            return;
            // Front left top
            m_triangles.Add(new Triangle([
                m_frontSquare.m_leftTop, m_leftSquare.m_rightTop, m_topSquare.m_leftTop
            ]));
            // Front right top
            m_triangles.Add(new Triangle([
                m_frontSquare.m_rightTop, m_rightSquare.m_rightTop, m_topSquare.m_rightTop
            ]));
            // Front bottom
            m_polygons.Add(new Polygon([
                m_frontSquare.m_bottomLeft, m_frontSquare.m_bottomRight,
                m_bottomSquare.m_leftTop, m_bottomSquare.m_rightTop
            ]));
            // Front left bottom
            m_triangles.Add(new Triangle([
                m_frontSquare.m_bottomLeft, m_leftSquare.m_bottomRight, m_bottomSquare.m_leftTop
            ]));
            // Front right bottom
            m_triangles.Add(new Triangle([
                m_frontSquare.m_bottomRight, m_rightSquare.m_bottomRight, m_bottomSquare.m_rightTop
            ]));

            // Front Side
            m_polygons.Add(new Polygon([
                m_frontSquare.m_bottomLeft, m_frontSquare.m_bottomRight,
                m_frontSquare.m_leftTop, m_frontSquare.m_rightTop
            ]));

            // Front Left side
            m_polygons.Add(new Polygon([
                m_leftSquare.m_bottomRight, m_frontSquare.m_bottomLeft,
                m_leftSquare.m_rightTop, m_frontSquare.m_leftTop
            ]));
            // Front Right side
            m_polygons.Add(new Polygon([
                m_rightSquare.m_bottomRight, m_frontSquare.m_bottomRight,
                m_rightSquare.m_rightTop, m_frontSquare.m_rightTop
            ]));
        }
    }
}
