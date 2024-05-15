using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace task5_1
{
    public class LightObject : IDrawable
    {
        private float _t = 0.0f;

        public void Draw()
        {
            GL.PushMatrix();

            //GL.Rotate(_t, 0, 0, 1f);
            GL.Translate(2f, 0, 1.5f);
            GL.Scale(0.4f, 0.4f, 0.4f);

            GL.Light(LightName.Light0, LightParameter.Position, Color4.White);

            DrawLightObject();

            GL.PopMatrix();

            _t += 0.1f;
        }

        private void DrawLightObject()
        {
            GL.Color4(Color4.Yellow);
            GL.Begin(PrimitiveType.QuadStrip);

            GL.Vertex3(-0.1f, -0.1f, 0.1f);
            GL.Vertex3(-0.1f, 0.1f, 0.1f);
            GL.Vertex3(0.1f, -0.1f, 0.1f);
            GL.Vertex3(0.1f, 0.1f, 0.1f);

            GL.Color4(Color4.White);

            GL.Vertex3(0.1f, -0.1f, -0.1f);
            GL.Vertex3(0.1f, 0.1f, -0.1f);

            GL.Vertex3(-0.1f, -0.1f, -0.1f);
            GL.Vertex3(-0.1f, 0.1f, -0.1f);

            GL.Vertex3(-0.1f, -0.1f, 0.1f);
            GL.Vertex3(-0.1f, 0.1f, 0.1f);

            GL.End();

            GL.Begin(PrimitiveType.Quads);

            GL.Vertex3(-0.1f, 0.1f, 0.1f);
            GL.Vertex3(0.1f, 0.1f, 0.1f);
            GL.Vertex3(0.1f, 0.1f, -0.1f);
            GL.Vertex3(-0.1f, 0.1f, -0.1f);

            GL.Vertex3(-0.1f, -0.1f, 0.1f);
            GL.Vertex3(0.1f, -0.1f, 0.1f);
            GL.Vertex3(0.1f, -0.1f, -0.1f);
            GL.Vertex3(-0.1f, -0.1f, -0.1f);

            GL.End();
        }
    }
}
