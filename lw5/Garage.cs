using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace task5_1
{
    public class Garage : IDrawable
    {
        private static bool _texturesInitialized = false;
        private int _brickTexture = 0;
        private int _roofTexture = 0;
        private int _garageDoorTexture = 0;

        public void Draw()
        {
            InitializeTextures();

            GL.Enable(EnableCap.Texture2D);
            GL.PushMatrix();

            GL.Scale(3.5f, 3.5f, 3.5f);
            GL.Translate(0.0f, -0.1f, -0.8f);

            DrawWalls();
            DrawRoof();
            DrawAssociateBetweenWallsAndRoof();
            DrawDoor();

            GL.PopMatrix();
            GL.Disable(EnableCap.Texture2D);
        }

        private void DrawWalls()
        {
            GL.BindTexture(TextureTarget.Texture2D, _brickTexture);

            GL.Begin(PrimitiveType.QuadStrip);

            // Передняя стена
            GL.Normal3(0f, 0f, 1f);

            GL.TexCoord2(0.8f, 0.6f);
            GL.Vertex3(0.12f, -0.1f, 0.87f);

            GL.TexCoord2(0.8f, -0.6f);
            GL.Vertex3(0.12f, 0.07f, 0.87f);

            GL.TexCoord2(-0.8f, 0.6f);
            GL.Vertex3(0.32f, -0.1f, 0.87f);

            GL.TexCoord2(-0.8f, -0.6f);
            GL.Vertex3(0.32f, 0.07f, 0.87f);

            // Правая стена
            GL.Normal3(1f, 0f, 0f);

            GL.TexCoord2(2f, 0.6f);
            GL.Vertex3(0.32f, -0.1f, 0.62f);

            GL.TexCoord2(2f, -0.6f);
            GL.Vertex3(0.32f, 0.07f, 0.62f);

            // Задняя стена
            GL.Normal3(0f, 0f, -1f);

            GL.TexCoord2(-0.6f, 0.6f);
            GL.Vertex3(0.12f, -0.1f, 0.62f);

            GL.TexCoord2(-0.6f, -0.6f);
            GL.Vertex3(0.12f, 0.07f, 0.62f);

            GL.End();
        }

        private void DrawRoof()
        {
            GL.BindTexture(TextureTarget.Texture2D, _roofTexture);

            // Черепица крыши
            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0.3f, 1f, 0f);

            GL.TexCoord2(-0.5f, -0.5f);
            GL.Vertex3(0.35f, 0.061f, 0.94f);

            GL.TexCoord2(0.5f, -0.5f);
            GL.Vertex3(0.35f, 0.061f, 0.57f);

            GL.TexCoord2(0.5f, 0.5f);
            GL.Vertex3(0.12f, 0.13f, 0.57f);

            GL.TexCoord2(-0.5f, 0.5f);
            GL.Vertex3(0.12f, 0.13f, 0.94f);

            GL.End();
        }

        private void DrawAssociateBetweenWallsAndRoof()
        {
            GL.BindTexture(TextureTarget.Texture2D, _brickTexture);

            GL.Begin(PrimitiveType.Triangles);

            // Передняя
            GL.Normal3(0f, 0f, 1f);

            GL.TexCoord2(3f, 0f);
            GL.Vertex3(-0.12f, 0.07f, 0.87f);

            GL.TexCoord2(-2.5f, 0f);
            GL.Vertex3(0.32f, 0.07f, 0.87f);

            GL.TexCoord2(0f, 0.5f);
            GL.Vertex3(0.12f, 0.13f, 0.87f);

            // Задняя
            GL.Normal3(0f, 0f, -1f);

            GL.TexCoord2(3f, 0f);
            GL.Vertex3(-0.12f, 0.07f, 0.62f);

            GL.TexCoord2(-2.5f, 0f);
            GL.Vertex3(0.32f, 0.07f, 0.62f);

            GL.TexCoord2(0f, 0.5f);
            GL.Vertex3(0.12f, 0.13f, 0.62f);

            GL.End();
        }

        private void DrawDoor()
        {
            GL.BindTexture(TextureTarget.Texture2D, _garageDoorTexture);

            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0f, 0f, 1f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(0.14f, -0.1f, 0.871f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(0.3f, -0.1f, 0.871f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(0.3f, 0.065f, 0.871f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(0.14f, 0.065f, 0.871f);

            GL.End();
        }

        private void InitializeTextures()
        {
            if (!_texturesInitialized)
            {
                Texture texture = new();
                _brickTexture = texture.LoadTexture(
                    "textures/brick.jpg",
                    TextureMagFilter.LinearDetailSgis,
                    TextureMinFilter.Linear
                );
                _roofTexture = texture.LoadTexture(
                    "textures/roof.jpg",
                    TextureMagFilter.LinearDetailSgis,
                    TextureMinFilter.Linear
                );
                _garageDoorTexture = texture.LoadTexture(
                    "textures/garage-door.jpg",
                    TextureMagFilter.LinearDetailSgis,
                    TextureMinFilter.Linear
                );
                _texturesInitialized = true;
            }
        }
    }
}
