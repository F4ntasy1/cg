using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace task7_1
{
    public class Window : GameWindow
    {
        private float m_frame = 0;
        private int m_fps = 0;
        private readonly string m_title;

        private bool m_leftButtonPressed = false;
        private float m_mouseX = 0;
        private float m_mouseY = 0;

        private ShaderProgram shaderProgram;

        public Window(NativeWindowSettings nativeWindowSettings)
            : base(GameWindowSettings.Default, nativeWindowSettings)
        {
            VSync = VSyncMode.On;
            m_title = nativeWindowSettings.Title;

            shaderProgram = new();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color4.Gray);

            GL.Translate(0f, 0f, -20f);

            return;

            Shader vertexShader = new(ShaderType.VertexShader, "../../../vertex_shader.vsh");
            vertexShader.CheckStatus();

            shaderProgram.AttachShader(vertexShader);

            shaderProgram.Compile();

            vertexShader.Delete();
            shaderProgram.DetachShaders();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //int xLocation = shaderProgram.GetAttributeLocation("x");

            float step = 1;

            GL.Begin(PrimitiveType.Quads);
            for (float x = -10; x <= 10; x += step)
            {
                for (float y = -10; y <= 10; y += step)
                {
                    GL.Vertex3(x, y, 0.0f);
                    GL.Vertex3(x + step, y, 0.0f);
                    GL.Vertex3(x + step, y + step, 0.0f);
                    GL.Vertex3(x, y + step, 0.0f);
                }
            }
            GL.End();

            SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            int width = e.Width;
            int height = e.Height;

            GL.Viewport(0, 0, width, height);

            SetupProjectionMatrix(width, height);

            GL.MatrixMode(MatrixMode.Modelview);
            base.OnResize(e);

            OnRenderFrame(new());
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
            UpdateFramesCount(args.Time);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            shaderProgram.DeleteProgram();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                m_leftButtonPressed = true;
                m_mouseX = MousePosition.X;
                m_mouseY = MousePosition.Y;
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (!m_leftButtonPressed) return;

            float dx = e.X - m_mouseX;
            float dy = e.Y - m_mouseY;

            // Поворот по Х = смещение мыши по Y и наоборот для Y
            float rotateX = dy * 180 / this.Size.X;
            float rotateY = dx * 180 / this.Size.Y;
            RotateCamera(rotateX, rotateY);

            m_mouseX = e.X;
            m_mouseY = e.Y;

            base.OnMouseMove(e);

            OnRenderFrame(new FrameEventArgs());
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            m_leftButtonPressed = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave()
        {
            m_leftButtonPressed = false;
            base.OnMouseLeave();
        }

        private void SetupProjectionMatrix(double width, double height)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            double frustumSize = 0.5;

            double aspectRatio = height != 0 ? (width / height) : 0;
            double frustumHeight = frustumSize;
            double frustumWidth = frustumHeight * aspectRatio;

            if (frustumWidth < frustumSize && aspectRatio != 0)
            {
                frustumWidth = frustumSize;
                frustumHeight = frustumWidth / aspectRatio;
            }

            GL.Frustum(
                -frustumWidth * 0.5, frustumWidth * 0.5, // left, right
                -frustumHeight * 0.5, frustumHeight * 0.5, // top, bottom
                frustumSize * 0.5, frustumSize * 500 // znear, zfar
            );
        }

        private void RotateCamera(float x, float y)
        {
            GL.MatrixMode(MatrixMode.Modelview);

            GL.GetFloat(GetPName.ModelviewMatrix, out Matrix4 modelView);

            Vector3 xAxis = new(modelView[0, 0], modelView[1, 0], modelView[2, 0]);
            Vector3 yAxis = new(modelView[0, 1], modelView[1, 1], modelView[2, 1]);

            GL.Rotate(x, xAxis);
            GL.Rotate(y, yAxis);
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
