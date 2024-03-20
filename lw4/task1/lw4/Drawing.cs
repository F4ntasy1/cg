using OpenTK.Graphics.OpenGL;
using Primitives;

namespace Drawing
{
    public class Color
    {
        public float m_r;
        public float m_g;
        public float m_b;
        public float m_a;

        public Color(float r, float g, float b, float a = 1.0f)
        {
            m_r = r;
            m_g = g;
            m_b = b;
            m_a = a;
        }
    }

    public interface IDrawable
    {
        public void Draw();

        public void SetColor(Color color);
    }

    public abstract class Drawable : IDrawable
    {
        public readonly Point[] m_points;
        protected Color m_color;

        public Drawable(Point[] points, Color? color = null)
        {
            /* Порядок points важен! */
            m_points = points;
            m_color = color ?? new(0.0f, 0.0f, 0.0f);
        }

        public void SetColor(Color color)
        {
            m_color = color;
        }

        public void Draw()
        {
            GL.Begin(GetPrimiviteType());
            GL.Color4(m_color.m_r, m_color.m_g, m_color.m_b, m_color.m_a);

            foreach (Point p in m_points)
            {
                GL.Vertex3(p.m_x, p.m_y, p.m_z);
            }

            GL.End();
        }

        protected abstract PrimitiveType GetPrimiviteType();
    }
}
