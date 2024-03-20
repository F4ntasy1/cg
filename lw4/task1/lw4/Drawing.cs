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

            DrawLines();
        }

        protected abstract PrimitiveType GetPrimiviteType();

        private void DrawLines()
        {
            GL.Begin(PrimitiveType.Lines);
            GL.LineWidth(1);
            GL.Color3(0.0f, 0.0f, 0.0f);

            for (int i = 0; i < m_points.Length; i++)
            {
                Point p1 = m_points[i];
                for (int j = i; j < m_points.Length; j++)
                {
                    Point p2 = m_points[j];
                    if ((p1.m_x == p2.m_x && p1.m_y == p2.m_y && p1.m_z != p2.m_z) ||
                        (p1.m_x == p2.m_x && p1.m_z == p2.m_z && p1.m_y != p2.m_y) ||
                        (p1.m_y == p2.m_y && p1.m_z == p2.m_z && p1.m_x != p2.m_x) ||
                        (m_points.Length <= 3))
                    {
                        GL.Vertex3(p1.m_x, p1.m_y, p1.m_z);
                        GL.Vertex3(p2.m_x, p2.m_y, p2.m_z);
                    }
                }
            }

            GL.End();
        }
    }
}
