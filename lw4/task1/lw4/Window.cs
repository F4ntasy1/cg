using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using lw4;

namespace task1
{
    public class Color
    {
        public float m_r; 
        public float m_g; 
        public float m_b;

        public Color(float r, float g, float b)
        {
            m_r = r;
            m_g = g;
            m_b = b;
        }
    }

    public class Point
    {
        public float m_x;
        public float m_y;

        public Point(float x, float y)
        {
            m_x = x; 
            m_y = y;
        }
    }

    public class Window : GameWindow
    {
        private float m_frame = 0;
        private int m_fps = 0;
        private readonly string m_title;

        private readonly IDrawable[] m_drawables;

        public Window(NativeWindowSettings nativeWindowSettings, IDrawable[] drawables)
            : base(GameWindowSettings.Default, nativeWindowSettings)
        {
            m_drawables = drawables;
            VSync = VSyncMode.On;
            m_title = nativeWindowSettings.Title;
            //GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.Blend);
            //GL.LoadIdentity();
            GL.MatrixMode(MatrixMode.Modelview);
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            // Цвет фона
            GL.ClearColor(Color4.White);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            int size = ClientSize.X < ClientSize.Y ? ClientSize.X : ClientSize.Y;
            GL.Viewport(0, 0, size, size);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            UpdateFramesCount(args.Time);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Rotate(0.7, 0.0f, 1.0f, 0.0f);
            GL.Rotate(0.5, 0.0f, 1.0f, 1.0f);

            foreach (IDrawable drawable in m_drawables)
            {
                drawable.Draw();
            }

            SwapBuffers(); // двойная буферизация
            base.OnRenderFrame(args);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }

        private void DrawPolygon(Point[] points, Color color)
        {
            GL.Begin(PrimitiveType.TriangleStrip);
            GL.Color3(color.m_r, color.m_g, color.m_b);

            foreach (Point p in points)
            {
                GL.Vertex2(p.m_x, p.m_y);
            }

            GL.End();
        }

        private void DrawCircle(float radius, Point center, Color color)
        {
            GL.Begin(PrimitiveType.TriangleFan);
            GL.Color3(color.m_r, color.m_g, color.m_b);

            GL.Vertex2(center.m_x, center.m_y);
            for (int angle = 0; angle < 360; angle++)
            {
                GL.Vertex2(center.m_x + Math.Cos(angle) * radius, 
                    center.m_y + Math.Sin(angle) * radius);
            }

            GL.End();
        }

        private void UpdateFramesCount(double time)
        {
            m_frame += (float)time;
            m_fps++;
            if (m_frame >= 1.0f)
            {
                Title = m_title + $" FPS - {m_fps}";
                m_fps = 0;
                m_frame = 0.0f;
            }
        }
    }
}
