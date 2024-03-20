using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Drawing;

namespace task1
{
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
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);
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

            GL.Rotate(0.3, 1.0f, 0.0f, 0.0f);
            GL.Rotate(0.15, 0.0f, 0.0f, 1.0f);
            GL.Rotate(0.4, 0.0f, 1.0f, 0.0f);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

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
