using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace task5_1
{
    public class Plot : IDrawable
    {
        private static bool _texturesInitialized = false;
        private int _grassTexture = 0;
        private int _roadSectionTexture = 0;
        private int _pedestrianSectionTexture = 0;
        private int _brickTexture = 0;
        private int _fenceTexture = 0;
        private int _fenceSecondTexture = 0;

        public void Draw()
        {
            InitializeTextures();

            GL.Enable(EnableCap.Texture2D);
            GL.PushMatrix();

            GL.Scale(3.5f, 3.5f, 3.5f);
            GL.Translate(0.0f, -0.2f, 0f);

            DrawGrass();
            DrawRoad();
            DrawPedestrian();
            DrawFrontFence();
            DrawOtherFence();

            GL.PopMatrix();
            GL.Disable(EnableCap.Texture2D);
        }

        private void DrawGrass()
        {
            GL.BindTexture(TextureTarget.Texture2D, _grassTexture);

            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0f, 1f, 0f);

            GL.TexCoord2(0f, 6f);
            GL.Vertex3(-0.5f, 0, 0.5f);

            GL.TexCoord2(6f, 6f);
            GL.Vertex3(0.5f, 0, 0.5f);

            GL.TexCoord2(6f, 0f);
            GL.Vertex3(0.5f, 0, -0.5f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(-0.5f, 0, -0.5f);

            GL.End();
        }

        private void DrawRoad()
        {
            GL.BindTexture(TextureTarget.Texture2D, _roadSectionTexture);

            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0f, 1f, 0f);

            GL.TexCoord2(0f, 6f);
            GL.Vertex3(0.13f, 0.001f, 0.5f);

            GL.TexCoord2(6f, 6f);
            GL.Vertex3(0.31f, 0.001f, 0.5f);

            GL.TexCoord2(6f, 0f);
            GL.Vertex3(0.31f, 0.001f, 0.05f);

            GL.TexCoord2(0.1f, 0f);
            GL.Vertex3(0.13f, 0.001f, 0.05f);

            GL.End();
        }

        private void DrawPedestrian()
        {
            GL.BindTexture(TextureTarget.Texture2D, _pedestrianSectionTexture);

            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0f, 1f, 0f);

            GL.TexCoord2(0f, 1.5f);
            GL.Vertex3(0.02f, 0.001f, 0.5f);

            GL.TexCoord2(0.5f, 1.5f);
            GL.Vertex3(0.1f, 0.001f, 0.5f);

            GL.TexCoord2(0.5f, 0f);
            GL.Vertex3(0.1f, 0.001f, 0.1f);

            GL.TexCoord2(0.1f, 0f);
            GL.Vertex3(0.02f, 0.001f, 0.1f);

            GL.End();
        }

        private void DrawFrontFence()
        {
            // Столбы
            DrawPillar(-0.445f);
            DrawPillar(-0.335f);
            DrawPillar(-0.225f);
            DrawPillar(-0.115f);
            DrawPillar(-0.005f);
            DrawPillar(0.105f);
            DrawPillar(0.315f);
            DrawPillar(0.425f);

            // Забор
            DrawOneFence(-0.427f);
            DrawOneFence(-0.317f);
            DrawOneFence(-0.207f);
            DrawOneFence(-0.097f);
            DrawOneFence(0.333f);
        }

        private void DrawOtherFence()
        {
            GL.BindTexture(TextureTarget.Texture2D, _fenceSecondTexture);

            GL.Begin(PrimitiveType.QuadStrip);

            // Правая сторона
            GL.Normal3(1f, 0f, 0f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(0.44f, 0.1f, 0.5f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(0.44f, 0.001f, 0.5f);

            GL.TexCoord2(11f, 1f);
            GL.Vertex3(0.44f, 0.1f, -0.45f);

            GL.TexCoord2(11f, 0f);
            GL.Vertex3(0.44f, 0.001f, -0.45f);

            // Задняя сторона
            GL.Normal3(0f, 0f, -1f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(-0.443f, 0.1f, -0.45f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(-0.443f, 0.001f, -0.45f);

            // Левая сторона
            GL.Normal3(-1f, 0f, 0f);

            GL.TexCoord2(11f, 1f);
            GL.Vertex3(-0.443f, 0.1f, 0.5f);

            GL.TexCoord2(11f, 0f);
            GL.Vertex3(-0.443f, 0.001f, 0.5f);

            GL.End();
        }

        private void DrawOneFence(float x)
        {
            GL.BindTexture(TextureTarget.Texture2D, _fenceTexture);

            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0f, 0f, 1f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(x, 0.001f, 0.49f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(x + 0.092f, 0.001f, 0.49f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(x + 0.092f, 0.1f, 0.49f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(x, 0.1f, 0.49f);

            GL.End();
        }

        private void DrawPillar(float x)
        {
            float size = 0.018f;
            float z = 0.5f;

            GL.BindTexture(TextureTarget.Texture2D, _brickTexture);

            GL.Begin(PrimitiveType.QuadStrip);

            // Передняя сторона
            GL.Normal3(0f, 0f, 1f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(x, 0.001f, z);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(x, 0.12f, z);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(x + size, 0.001f, z);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(x + size, 0.12f, z);

            // Правая сторона
            GL.Normal3(1f, 0f, 0f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(x + size, 0.001f, z - size);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(x + size, 0.12f, z - size);

            // Задняя сторона
            GL.Normal3(0f, 0f, -1f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(x, 0.001f, z - size);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(x, 0.12f, z - size);

            // Левая сторона
            GL.Normal3(-1f, 0f, 0f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(x, 0.001f, z);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(x, 0.12f, z);

            GL.End();

            //
            // Верхняя часть
            //

            GL.Begin(PrimitiveType.QuadStrip);

            // Передняя сторона
            GL.Normal3(0f, 0f, 1f);

            GL.TexCoord2(0f, 0.07f);
            GL.Vertex3(x - 0.004f, 0.12f, z + 0.004f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(x - 0.004f, 0.13f, z + 0.004f);

            GL.TexCoord2(1.4f, 0.07f);
            GL.Vertex3(x + size + 0.004f, 0.12f, z + 0.004f);

            GL.TexCoord2(1.4f, 0f);
            GL.Vertex3(x + size + 0.004f, 0.13f, z + 0.004f);

            // Правая сторона
            GL.Normal3(1f, 0f, 0f);

            GL.TexCoord2(0f, 0.3f);
            GL.Vertex3(x + size + 0.004f, 0.12f, z - size - 0.004f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(x + size + 0.004f, 0.13f, z - size - 0.004f);

            // Задняя сторона
            GL.Normal3(0f, 0f, -1f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(x - 0.004f, 0.12f, z - size - 0.004f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(x - 0.004f, 0.13f, z - size - 0.004f);

            // Левая сторона
            GL.Normal3(-1f, 0f, 0f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(x - 0.004f, 0.12f, z + 0.004f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(x - 0.004f, 0.13f, z + 0.004f);

            GL.End();

            GL.Begin(PrimitiveType.Quads);

            // Верхушка
            GL.Normal3(0f, 1f, 0f);

            GL.TexCoord2(0f, 0.5f);
            GL.Vertex3(x - 0.004f, 0.13f, z + 0.004f);

            GL.TexCoord2(0.5f, 0.5f);
            GL.Vertex3(x + size + 0.004f, 0.13f, z + 0.004f);

            GL.TexCoord2(0.5f, 0f);
            GL.Vertex3(x + size + 0.004f, 0.13f, z - size - 0.004f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(x - 0.004f, 0.13f, z - size - 0.004f);

            // Днище
            GL.Normal3(0f, -1f, 0f);

            GL.TexCoord2(0f, 0.5f);
            GL.Vertex3(x - 0.004f, 0.12f, z + 0.004f);

            GL.TexCoord2(0.5f, 0.5f);
            GL.Vertex3(x + size + 0.004f, 0.12f, z + 0.004f);

            GL.TexCoord2(0.5f, 0f);
            GL.Vertex3(x + size + 0.004f, 0.12f, z - size - 0.004f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(x - 0.004f, 0.12f, z - size - 0.004f);

            GL.End();
        }

        private void InitializeTextures()
        {
            if (!_texturesInitialized)
            {
                Texture texture = new();
                _grassTexture = texture.LoadTexture(
                    "textures/grass.jpg",
                    TextureMagFilter.Nearest,
                    TextureMinFilter.Nearest
                );
                _roadSectionTexture = texture.LoadTexture(
                    "textures/road-section.jpg",
                    TextureMagFilter.LinearDetailSgis,
                    TextureMinFilter.Linear
                );
                _pedestrianSectionTexture = texture.LoadTexture(
                    "textures/pedestrian-section.jpg",
                    TextureMagFilter.LinearDetailSgis,
                    TextureMinFilter.Linear
                );
                _brickTexture = texture.LoadTexture(
                   "textures/brick2.jpg",
                   TextureMagFilter.LinearDetailSgis,
                   TextureMinFilter.Linear
                );
                _fenceTexture = texture.LoadTexture(
                   "textures/fence.png",
                   TextureMagFilter.LinearDetailSgis,
                   TextureMinFilter.Linear
                );
                _fenceSecondTexture = texture.LoadTexture(
                   "textures/fence2.jpg",
                   TextureMagFilter.LinearDetailSgis,
                   TextureMinFilter.Linear
                );
                _texturesInitialized = true;
            }
        }
    }
}
