using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace task2
{
    public class Torus: IDrawable
    {
        private readonly float R = 2f;
        private readonly float r = 0.5f;
        private readonly float step = MathF.PI / 30;

        private void SetVertex(float a, float b)
        {
            Vector3 p = new(
                (R + r * MathF.Cos(a)) * MathF.Cos(b),
                (R + r * MathF.Cos(a)) * MathF.Sin(b),
                r * MathF.Sin(a)
            );

            GL.Color3(p.X, p.Y, p.Z);
            GL.Vertex3(p);
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Quads);
            for (float b = 0; b < 2 * MathF.PI; b += step)
            {
                // b - описывает окружность
                for (float a = 0; a < 2 * MathF.PI; a += step)
                {
                    // a - описывает объем
                    SetVertex(a, b);
                    SetVertex(a + step, b);
                    SetVertex(a + step, b + step);
                    SetVertex(a, b + step);
                }
            }
            GL.End();
        }
    }
}
