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

        private List<IDrawable> m_drawables = [];

        private Color m_color;
        private Color m_colorSecond;
        private Color m_colorThird;

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
            m_colorSecond = new(m_color.m_r + 1.0f, m_color.m_g, m_color.m_b - 0.3f, m_color.m_a);
            m_colorThird = new(m_color.m_r, m_color.m_g - 0.3f, m_color.m_b, m_color.m_a);

            InitializeFrontPrimitives();
            InitializeSidePrimitives();
            InitializeBackPrimitives();
        }

        public void SetColor(Color color)
        {
            m_color = color;
            m_colorSecond = new(color.m_r + 0.3f, color.m_g, color.m_b - 0.3f, color.m_a);
            m_colorThird = new(color.m_r, color.m_g - 0.3f, color.m_b, color.m_a);
        }

        public void Draw()
        {
            foreach (IDrawable drawable in m_drawables)
            {
                drawable.Draw();
            }
        }

        public void DrawLines()
        {
            foreach (IDrawable drawable in m_drawables)
            {
                drawable.DrawLines();
            }
        }

        private void InitializeSidePrimitives()
        {
            // Top
            m_drawables.Add(new Polygon([
                m_topSquare.m_rightTop, m_topSquare.m_bottomRight,
                m_topSquare.m_leftTop, m_topSquare.m_bottomLeft,
            ], m_colorSecond));
            // Top Left
            m_drawables.Add(new Polygon([
                m_leftSquare.m_leftTop, m_leftSquare.m_rightTop, m_topSquare.m_bottomLeft,
                m_topSquare.m_leftTop
            ], m_colorSecond));
            // Top Right
            m_drawables.Add(new Polygon([
                m_rightSquare.m_rightTop, m_rightSquare.m_leftTop, m_topSquare.m_rightTop,
                m_topSquare.m_bottomRight,
            ], m_colorSecond));
            // Bottom
            m_drawables.Add(new Polygon([
                m_bottomSquare.m_bottomLeft, m_bottomSquare.m_bottomRight,
                m_bottomSquare.m_leftTop, m_bottomSquare.m_rightTop
            ], m_colorSecond));
            // Bottom Left
            m_drawables.Add(new Polygon([
                m_leftSquare.m_bottomRight, m_leftSquare.m_bottomLeft,
                m_bottomSquare.m_leftTop, m_bottomSquare.m_bottomLeft,
            ], m_colorSecond));
            // Bottom Right
            m_drawables.Add(new Polygon([
                m_rightSquare.m_bottomLeft, m_rightSquare.m_bottomRight,
                m_bottomSquare.m_bottomRight, m_bottomSquare.m_rightTop
            ], m_colorSecond));
            // Left Side
            m_drawables.Add(new Polygon([
                m_leftSquare.m_bottomLeft, m_leftSquare.m_bottomRight,
                m_leftSquare.m_leftTop, m_leftSquare.m_rightTop
            ], m_colorSecond));
            // Right Side
            m_drawables.Add(new Polygon([
                m_rightSquare.m_bottomRight, m_rightSquare.m_bottomLeft,
                m_rightSquare.m_rightTop, m_rightSquare.m_leftTop,
            ], m_colorSecond));
        }

        private void InitializeBackPrimitives()
        {
            // Back Top
            m_drawables.Add(new Polygon([
                m_topSquare.m_bottomRight, m_backSquare.m_rightTop,
                m_topSquare.m_bottomLeft, m_backSquare.m_leftTop,
            ], m_color));
            // Top Back left
            m_drawables.Add(new Triangle([
                m_backSquare.m_leftTop, m_leftSquare.m_leftTop, m_topSquare.m_bottomLeft
            ], m_color));
            // Top Back right
            m_drawables.Add(new Triangle([
                m_backSquare.m_rightTop, m_topSquare.m_bottomRight, m_rightSquare.m_leftTop,
            ], m_color));
            // Bottom Back left
            m_drawables.Add(new Triangle([
                m_backSquare.m_bottomLeft, m_bottomSquare.m_bottomLeft, m_leftSquare.m_bottomLeft,
            ], m_color));
            // Bottom Back right
            m_drawables.Add(new Triangle([
                m_backSquare.m_bottomRight, m_rightSquare.m_bottomLeft, m_bottomSquare.m_bottomRight
            ], m_color));
            // Bottom Back
            m_drawables.Add(new Polygon([
                m_backSquare.m_bottomLeft, m_backSquare.m_bottomRight,
                m_bottomSquare.m_bottomLeft, m_bottomSquare.m_bottomRight
            ], m_color));
            // Back Side
            m_drawables.Add(new Polygon([
                m_backSquare.m_bottomRight, m_backSquare.m_bottomLeft,
                m_backSquare.m_rightTop, m_backSquare.m_leftTop,
            ], m_color));
            // Back Left
            m_drawables.Add(new Polygon([
                m_backSquare.m_bottomLeft, m_leftSquare.m_bottomLeft,
                m_backSquare.m_leftTop, m_leftSquare.m_leftTop,
            ], m_color));
            // Back Right
            m_drawables.Add(new Polygon([
                m_rightSquare.m_bottomLeft, m_backSquare.m_bottomRight,
                m_rightSquare.m_leftTop, m_backSquare.m_rightTop
            ], m_color));
        }

        private void InitializeFrontPrimitives()
        {
            // Front top
            m_drawables.Add(new Polygon([
                m_frontSquare.m_leftTop, m_frontSquare.m_rightTop, 
                m_topSquare.m_leftTop, m_topSquare.m_rightTop,
            ], m_colorThird));
            // Front left top
            m_drawables.Add(new Triangle([
                m_frontSquare.m_leftTop, m_topSquare.m_leftTop, m_leftSquare.m_rightTop,
            ], m_colorThird));
            // Front right top
            m_drawables.Add(new Triangle([
                m_frontSquare.m_rightTop, m_rightSquare.m_rightTop, m_topSquare.m_rightTop
            ], m_colorThird));
            // Front bottom
            m_drawables.Add(new Polygon([
                m_frontSquare.m_bottomRight, m_frontSquare.m_bottomLeft,
                m_bottomSquare.m_rightTop, m_bottomSquare.m_leftTop,
            ], m_colorThird));
            // Front left bottom
            m_drawables.Add(new Triangle([
                m_frontSquare.m_bottomLeft, m_leftSquare.m_bottomRight, m_bottomSquare.m_leftTop
            ], m_colorThird));
            // Front right bottom
            m_drawables.Add(new Triangle([
                m_frontSquare.m_bottomRight, m_bottomSquare.m_rightTop, m_rightSquare.m_bottomRight,
            ], m_colorThird));
            // Front Side
            m_drawables.Add(new Polygon([
                m_frontSquare.m_bottomLeft, m_frontSquare.m_bottomRight,
                m_frontSquare.m_leftTop, m_frontSquare.m_rightTop
            ], m_colorThird));
            // Front Left side
            m_drawables.Add(new Polygon([
                m_leftSquare.m_bottomRight, m_frontSquare.m_bottomLeft,
                m_leftSquare.m_rightTop, m_frontSquare.m_leftTop
            ], m_colorThird));
            // Front Right side
            m_drawables.Add(new Polygon([
                m_frontSquare.m_bottomRight, m_rightSquare.m_bottomRight,
                m_frontSquare.m_rightTop, m_rightSquare.m_rightTop,
            ], m_colorThird));
        }
    }
}
