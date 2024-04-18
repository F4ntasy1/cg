using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace task2
{
    public class Torus: IDrawable
    {
        private readonly float R = 2f;
        private readonly float r = 0.5f;
        private readonly float step = MathF.PI / 30;

        private void SetVertexByAngles(float a, float b)
        {
            Vector3 p = new(
                (R + r * MathF.Cos(a)) * MathF.Cos(b),
                (R + r * MathF.Cos(a)) * MathF.Sin(b),
                r * MathF.Sin(a)
            );
            GL.Color3(MathF.Sin(a) / 1.5, MathF.Sin(b) / 2, MathF.Cos(a) / 1.5);
            GL.Vertex3(p);
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Quads); // QuadStrip + вырожденные quad / TriangleStrip

            for (float b = 0; b < 2 * MathF.PI; b += step)
            {
                // b - описывает окружность тора
                for (float a = 0; a < 2 * MathF.PI; a += step)
                {
                    // a - описывает объем тора
                    SetVertexByAngles(a, b);
                    SetVertexByAngles(a + step, b);
                    SetVertexByAngles(a + step, b + step);
                    SetVertexByAngles(a, b + step);
                }
            }

            GL.End();
        }
    }
}
