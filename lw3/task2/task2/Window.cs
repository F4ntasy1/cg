using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Drawing;

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
        private const float MAX_COORD_VALUE = 0.9f;
        private const float MIN_COORD_VALUE = -0.9f;

        private float frame = 0;
        private int fps = 0;
        private readonly string title;

        private Point shaftCenter = new(0.0f, -0.6f);
        private Point crankShaftCenter = new(-0.22f, -0.52f);

        private Color grayColor = new(0.5f, 0.5f, 0.5f);
        private Color darkGrayColor = new(0.4f, 0.4f, 0.4f);
        private Color lightGrayColor = new(0.8f, 0.8f, 0.8f);
        private Color whiteColor = new(0.97f, 0.97f, 0.97f);

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
            DrawEngineBackground();
            DrawShaftBackground();

            DrawIntersectionBetweenShaftAndCrank();

            DrawShaft(shaftCenter);
            DrawEngineCylinder();
            DrawCrank();

            DrawInletValve();
            DrawOutletValve();
        }

        private void DrawEngineBackground()
        {
            // Обводка
            DrawPolygon([
                    new(-0.3f, 0.75f),
                    new(0.3f, 0.75f),
                    new(-0.3f, -0.5f),
                    new(0.3f, -0.5f)
                ], grayColor);
            DrawPolygon([
                    new(-0.29f, 0.74f),
                    new(0.29f, 0.74f),
                    new(-0.29f, -0.49f),
                    new(0.29f, -0.49f)
                ], whiteColor);

            // Шахта
            DrawPolygon([
                    new(-0.22f, 0.7f),
                    new(0.22f, 0.7f),
                    new(-0.22f, -0.35f),
                    new(0.22f, -0.35f)
                ], grayColor);
            DrawPolygon([
                    new(-0.21f, 0.69f),
                    new(0.21f, 0.69f),
                    new(-0.21f, -0.34f),
                    new(0.21f, -0.34f)
                ], lightGrayColor);
        }

        private void DrawShaftBackground()
        {
            // Большая часть
            DrawCircle(0.35f, shaftCenter, grayColor);
            DrawCircle(0.34f, shaftCenter, lightGrayColor);

            // Меньшая часть
            DrawCircle(0.25f, shaftCenter, grayColor);
            DrawCircle(0.24f, shaftCenter, new(0.88f, 0.88f, 0.88f));
        }

        private void DrawEngineCylinder()
        {
            // Цилиндр в шахте
            DrawPolygon([
                    new(-0.2f, 0.48f),
                    new(0.2f, 0.48f),
                    new(-0.2f, 0.05f),
                    new(0.2f, 0.05f)
                ], new(0.63f, 0.63f, 0.63f));
            DrawCircle(0.055f, new(0.0f, 0.22f), grayColor);
            DrawCircle(0.05f, new(0.0f, 0.22f), lightGrayColor);

            DrawCylinderIntersectionLines();
        }

        private void DrawCylinderIntersectionLines()
        {
            // Линии пересечения цилиндра
            GL.LineWidth(5);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(grayColor.m_r, grayColor.m_g, grayColor.m_b);

            GL.Vertex2(-0.21f, 0.45f);
            GL.Vertex2(0.21f, 0.45f);

            GL.Vertex2(-0.21f, 0.43f);
            GL.Vertex2(0.21f, 0.43f);

            GL.Vertex2(-0.21f, 0.41f);
            GL.Vertex2(0.21f, 0.41f);

            GL.End();
        }

        private void DrawCrank()
        {
            // Шатун, соединяющий цилиндр с валом
            DrawPolygon([
                    new(-0.02f, 0.05f),
                    new(0.02f, 0.05f),
                    new(-0.23f, -0.46f),
                    new(-0.18f, -0.48f)
                ], grayColor);
            DrawShaft(new(-0.22f, -0.52f));
        }

        private void DrawIntersectionBetweenShaftAndCrank()
        {
            // Линия пересечения между центром вала и шатуна
            GL.LineWidth(8);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(grayColor.m_r, grayColor.m_g, grayColor.m_b);

            GL.Vertex2(shaftCenter.m_x, shaftCenter.m_y);
            GL.Vertex2(crankShaftCenter.m_x, crankShaftCenter.m_y);

            GL.End();
        }

        private void DrawShaft(Point center)
        {
            // Вал
            DrawCircle(0.075f, center, grayColor);
            DrawCircle(0.07f, center, whiteColor);

            DrawCircle(0.035f, center, grayColor);
        }

        private void DrawInletValve()
        {
            // Входной клапан
            DrawPolygon([
                    new(-0.15f, 0.65f),
                    new(-0.075f, 0.65f),
                    new(-0.15f, 0.95f),
                    new(-0.075f, 0.95f)
                ], grayColor);
            DrawPolygon([
                    new(-0.15f, 0.9f),
                    new(-0.15f, 0.85f),
                    new(-0.25f, 0.9f),
                    new(-0.25f, 0.85f)
                ], grayColor);

            DrawPolygon([
                    new(-0.14f, 0.66f),
                    new(-0.085f, 0.66f),
                    new(-0.14f, 0.94f),
                    new(-0.085f, 0.94f)
                ], whiteColor);
            DrawPolygon([
                    new(-0.14f, 0.89f),
                    new(-0.14f, 0.86f),
                    new(-0.24f, 0.89f),
                    new(-0.24f, 0.86f)
                ], whiteColor);

            DrawPolygon([
                    new(-0.23f, 0.93f),
                    new(-0.255f, 0.93f),
                    new(-0.23f, 0.82f),
                    new(-0.255f, 0.82f)
                ], darkGrayColor);
        }

        private void DrawOutletValve()
        {
            // Выпускной клапан
            DrawPolygon([
                    new(0.15f, 0.65f),
                    new(0.075f, 0.65f),
                    new(0.15f, 0.95f),
                    new(0.075f, 0.95f)
                ], grayColor);
            DrawPolygon([
                    new(0.15f, 0.9f),
                    new(0.15f, 0.85f),
                    new(0.25f, 0.9f),
                    new(0.25f, 0.85f)
                ], grayColor);

            DrawPolygon([
                    new(0.14f, 0.66f),
                    new(0.085f, 0.66f),
                    new(0.14f, 0.94f),
                    new(0.085f, 0.94f)
                ], whiteColor);
            DrawPolygon([
                    new(0.14f, 0.89f),
                    new(0.14f, 0.86f),
                    new(0.24f, 0.89f),
                    new(0.24f, 0.86f)
                ], whiteColor);

            DrawPolygon([
                    new(0.23f, 0.93f),
                    new(0.255f, 0.93f),
                    new(0.23f, 0.82f),
                    new(0.255f, 0.82f)
                ], darkGrayColor);
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
