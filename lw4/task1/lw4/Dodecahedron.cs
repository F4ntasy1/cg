using OpenTK.Graphics.OpenGL;

namespace lw4
{
    public class Dodecahedron: IDrawable
    {
        public void Draw()
        {
            GL.Begin(PrimitiveType.TriangleStrip);
            GL.Color3(0.0f, 0.0f, 0.0f);

            GL.Vertex3(0.0f, 0.0f, 0.0f);
            GL.Vertex3(0.5f, 0.0f, 0.0f);
            GL.Vertex3(0.25f, 0.5f, 0.0f);

            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(0.25f, 0.0f, 0.5f);

            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(0.0f, 0.0f, 0.0f);



            GL.End();
        }
    }
}
