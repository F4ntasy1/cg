using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing;

namespace task1
{
    public class Window : GameWindow
    {
        private const float MAX_COORD_VALUE = 0.9f;
        private const float MIN_COORD_VALUE = -0.9f;

        private float frame = 0;
        private int fps = 0;
        private readonly string title;

        public Window(NativeWindowSettings nativeWindowSettings)
            : base(GameWindowSettings.Default, nativeWindowSettings)
        {
            Console.WriteLine(GL.GetString(StringName.Version));
            VSync = VSyncMode.On;
            title = nativeWindowSettings.Title;
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
            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            UpdateFramesCount(args.Time);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Draw();

            SwapBuffers(); // двойная буферизация
            base.OnRenderFrame(args);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }

        private void Draw()
        {
            DrawAxisLines();
            DrawAxisDivisions();
            DrawGraphic();
        }

        private void DrawAxisLines()
        {
            GL.LineWidth(3);

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(0.0f, 0.0f, 0.0f);

            // Ось X
            GL.Vertex2(MIN_COORD_VALUE, 0.0f);
            GL.Vertex2(MAX_COORD_VALUE, 0.0f);

            GL.Vertex2(MAX_COORD_VALUE, 0.0f);
            GL.Vertex2(0.86f, 0.02f);

            GL.Vertex2(MAX_COORD_VALUE, 0.0f);
            GL.Vertex2(0.86f, -0.02f);

            // Ось Y
            GL.Vertex2(0.0f, MIN_COORD_VALUE);
            GL.Vertex2(0.0f, MAX_COORD_VALUE);

            GL.Vertex2(0.0f, MAX_COORD_VALUE);
            GL.Vertex2(0.02f, 0.86f);

            GL.Vertex2(0.0f, MAX_COORD_VALUE);
            GL.Vertex2(-0.02f, 0.86f);

            GL.End();
        }

        private void DrawAxisDivisions()
        {
            GL.LineWidth(3);

            GL.Begin(PrimitiveType.Lines);
            GL.Color3(0.0f, 0.0f, 0.0f);

            for (float coord = MIN_COORD_VALUE + 0.1f; coord < MAX_COORD_VALUE; coord += 0.1f)
            {
                // Деления по оси Х
                GL.Vertex2(coord, 0.02f);
                GL.Vertex2(coord, -0.02f);

                // Деления по оси Y
                GL.Vertex2(0.02f, coord);
                GL.Vertex2(-0.02f, coord);
            }

            GL.End();
        }

        private void DrawGraphic()
        {
            GL.LineWidth(3);

            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(1.0f, 0.0f, 0.0f);

            for (float x = -2.0f; x <= 3.0f; x += 0.001f)
            {
                // Парабола
                float y = 2 * x * x - 3 * x - 8;
                GL.Vertex2(x / 10, y / 10);
            }

            GL.End();
        }

        private void UpdateFramesCount(double time)
        {
            frame += (float)time;
            fps++;
            if (frame >= 1.0f)
            {
                Title = title + $" FPS - {fps}";
                fps = 0;
                frame = 0.0f;
            }
        }
    }
}
