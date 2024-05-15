using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

// устранить мeльтишение текстур
namespace task5_1
{
    public class House : IDrawable
    {
        private static bool _texturesInitialized = false;
        private int _brickTexture = 0;
        private int _roofTexture = 0;
        private int _windowTexture = 0;
        private int _ventilationTexture = 0;
        private int _doorTexture = 0;

        public void Draw()
        {
            InitializeTextures();

            GL.Enable(EnableCap.Texture2D);
            GL.PushMatrix();

            GL.Scale(3.5f, 3.5f, 3.5f);
            GL.Translate(0.0f, -0.1f, -0.8f);

            DrawHouseWalls();
            DrawHouseRoof();
            DrawAssociateBetweenHouseWallsAndRoof();
            DrawHouseWindows();
            DrawVentilation();
            DrawDoor();

            GL.PopMatrix();
            GL.Disable(EnableCap.Texture2D);
        }

        private void DrawHouseWalls()
        {
            GL.BindTexture(TextureTarget.Texture2D, _brickTexture);

            GL.Begin(PrimitiveType.QuadStrip);

            // Передняя ближняя стена
            GL.Normal3(0f, 0f, 1f);

            GL.TexCoord2(-1f, 1f);
            GL.Vertex3(-0.12f, -0.1f, 0.9f);

            GL.TexCoord2(-1f, -1f);
            GL.Vertex3(-0.12f, 0.3f, 0.9f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(0.12f, -0.1f, 0.9f);

            GL.TexCoord2(1f, -1f);
            GL.Vertex3(0.12f, 0.3f, 0.9f);

            // Правая стена
            GL.Normal3(1f, 0f, 0f);

            GL.TexCoord2(-1f, 1f);
            GL.Vertex3(0.12f, -0.1f, 0.6f);

            GL.TexCoord2(-1f, -1f);
            GL.Vertex3(0.12f, 0.3f, 0.6f);

            // Задняя стена
            GL.Normal3(0f, 0f, -1f);

            GL.TexCoord2(2.5f, 1f);
            GL.Vertex3(-0.25f, -0.1f, 0.6f);

            GL.TexCoord2(2.5f, -1f);
            GL.Vertex3(-0.25f, 0.3f, 0.6f);

            // Левая дальняя стена
            GL.Normal3(-1f, 0f, 0f);

            GL.TexCoord2(-1f, 1f);
            GL.Vertex3(-0.25f, -0.1f, 0.75f);

            GL.TexCoord2(-1f, -1f);
            GL.Vertex3(-0.25f, 0.3f, 0.75f);

            // Передняя дальняя стена
            GL.Normal3(-1f, 0f, 1f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(-0.12f, -0.1f, 0.75f);

            GL.TexCoord2(1f, -1f);
            GL.Vertex3(-0.12f, 0.3f, 0.75f);

            // Левая ближняя стена
            GL.Normal3(-1f, 0f, 0f);

            GL.TexCoord2(-1f, 1f);
            GL.Vertex3(-0.12f, -0.1f, 0.9f);

            GL.TexCoord2(-1f, -1f);
            GL.Vertex3(-0.12f, 0.3f, 0.9f);

            GL.End();
        }

        private void DrawHouseRoof()
        {
            GL.BindTexture(TextureTarget.Texture2D, _roofTexture);

            GL.Begin(PrimitiveType.QuadStrip);

            // Передняя большая часть
            GL.Normal3(0f, 1f, 0.3f);

            GL.TexCoord2(-0.5f, 0.5f);
            GL.Vertex3(-0.14f, 0.25f, 0.95f);

            GL.TexCoord2(-0.5f, -0.5f);
            GL.Vertex3(0.14f, 0.25f, 0.95f);

            GL.TexCoord2(0.5f, 0.5f);
            GL.Vertex3(-0.14f, 0.45f, 0.75f);

            GL.TexCoord2(0.5f, -0.5f);
            GL.Vertex3(0.14f, 0.45f, 0.75f);

            // Задняя большая часть
            GL.Normal3(0f, 1f, -0.3f);

            GL.TexCoord2(-0.5f, 0.5f);
            GL.Vertex3(-0.14f, 0.25f, 0.55f);

            GL.TexCoord2(-0.5f, -0.5f);
            GL.Vertex3(0.14f, 0.25f, 0.55f);

            GL.End();

            GL.Begin(PrimitiveType.QuadStrip);

            // Передняя меньшая часть
            GL.Normal3(0f, 1f, 0.3f);

            GL.TexCoord2(-0.27f, 0.27f);
            GL.Vertex3(-0.27f, 0.25f, 0.8f);

            GL.TexCoord2(-0.27f, -0.27f);
            GL.Vertex3(-0.12f, 0.25f, 0.8f);

            GL.TexCoord2(0.27f, 0.27f);
            GL.Vertex3(-0.27f, 0.382f, 0.675f);

            GL.TexCoord2(0.27f, -0.27f);
            GL.Vertex3(-0.12f, 0.382f, 0.675f);

            // Задняя меньшая часть
            GL.Normal3(0f, 1f, -0.3f);

            GL.TexCoord2(-0.27f, 0.27f);
            GL.Vertex3(-0.27f, 0.25f, 0.55f);

            GL.TexCoord2(-0.27f, -0.27f);
            GL.Vertex3(-0.12f, 0.25f, 0.55f);

            GL.End();
        }

        private void DrawAssociateBetweenHouseWallsAndRoof()
        {
            GL.BindTexture(TextureTarget.Texture2D, _brickTexture);

            GL.Begin(PrimitiveType.Triangles);

            // Правая
            GL.Normal3(1f, 0f, 0f);

            GL.TexCoord2(1f, -0.6f);
            GL.Vertex3(0.12f, 0.3f, 0.9f);

            GL.TexCoord2(-1f, -0.6f);
            GL.Vertex3(0.12f, 0.3f, 0.6f);

            GL.TexCoord2(0f, 0.3f);
            GL.Vertex3(0.12f, 0.45f, 0.75f);

            // Левая большая
            GL.Normal3(-1f, 0f, 0f);

            GL.TexCoord2(1.5f, -0.6f);
            GL.Vertex3(-0.12f, 0.3f, 0.9f);

            GL.TexCoord2(-1.5f, -0.6f);
            GL.Vertex3(-0.12f, 0.3f, 0.6f);

            GL.TexCoord2(0f, 0.3f);
            GL.Vertex3(-0.12f, 0.45f, 0.75f);

            // Левая малая
            GL.Normal3(-1f, 0f, 0f);

            GL.TexCoord2(1.3f, -0.3f);
            GL.Vertex3(-0.25f, 0.3f, 0.75f);

            GL.TexCoord2(-1.3f, -0.3f);
            GL.Vertex3(-0.25f, 0.3f, 0.6f);

            GL.TexCoord2(0f, 0.1f);
            GL.Vertex3(-0.25f, 0.382f, 0.675f);

            GL.End();
        }

        private void DrawHouseWindows()
        {
            GL.BindTexture(TextureTarget.Texture2D, _windowTexture);

            GL.Begin(PrimitiveType.Quads);

            // Ближняя передняя стена
            // Правое верхнее
            GL.Normal3(0f, 0f, 1f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(0.02f, 0.15f, 0.901f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(0.1f, 0.15f, 0.901f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(0.1f, 0.26f, 0.901f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(0.02f, 0.26f, 0.901f);

            // Левое верхнее
            GL.Normal3(0f, 0f, 1f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(-0.1f, 0.15f, 0.901f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(-0.02f, 0.15f, 0.901f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(-0.02f, 0.26f, 0.901f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(-0.1f, 0.26f, 0.901f);

            // Левое нижнее
            GL.Normal3(0f, 0f, 1f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(-0.1f, -0.03f, 0.901f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(-0.02f, -0.03f, 0.901f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(-0.02f, 0.08f, 0.901f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(-0.1f, 0.08f, 0.901f);

            // Левая стена
            // Верхнее
            GL.Normal3(-1f, 0f, 0f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(-0.251f, 0.15f, 0.635f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(-0.251f, 0.15f, 0.715f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(-0.251f, 0.26f, 0.715f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(-0.251f, 0.26f, 0.635f);

            // Нижнее
            GL.Normal3(-1f, 0f, 0f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(-0.251f, -0.03f, 0.635f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(-0.251f, -0.03f, 0.715f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(-0.251f, 0.08f, 0.715f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(-0.251f, 0.08f, 0.635f);

            // Задняя стена
            // Нижнее левое
            GL.Normal3(0f, 0f, -1f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(-0.23f, -0.03f, 0.599f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(-0.23f, 0.08f, 0.599f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(-0.15f, 0.08f, 0.599f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(-0.15f, -0.03f, 0.599f);

            // Нижнее центральное
            GL.Normal3(0f, 0f, -1f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(-0.11f, -0.03f, 0.599f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(-0.11f, 0.08f, 0.599f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(-0.03f, 0.08f, 0.599f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(-0.03f, -0.03f, 0.599f);

            // Верхнее левое
            GL.Normal3(0f, 0f, -1f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(-0.23f, 0.15f, 0.599f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(-0.23f, 0.26f, 0.599f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(-0.15f, 0.26f, 0.599f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(-0.15f, 0.15f, 0.599f);

            // Верхнее центральное
            GL.Normal3(0f, 0f, -1f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(-0.11f, 0.15f, 0.599f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(-0.11f, 0.26f, 0.599f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(-0.03f, 0.26f, 0.599f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(-0.03f, 0.15f, 0.599f);

            GL.End();
        }

        private void DrawVentilation()
        {
            GL.BindTexture(TextureTarget.Texture2D, _ventilationTexture);

            GL.Begin(PrimitiveType.Quads);

            // Правая на крыше
            GL.Normal3(1f, 0f, 0f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(0.121f, 0.35f, 0.73f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(0.121f, 0.35f, 0.77f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(0.121f, 0.4f, 0.77f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(0.121f, 0.4f, 0.73f);

            // Левая на маленькой крыше
            GL.Normal3(-1f, 0f, 0f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(-0.251f, 0.31f, 0.66f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(-0.251f, 0.31f, 0.69f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(-0.251f, 0.35f, 0.69f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(-0.251f, 0.35f, 0.66f);

            GL.End();
        }

        private void DrawDoor()
        {
            GL.BindTexture(TextureTarget.Texture2D, _doorTexture);

            GL.Begin(PrimitiveType.Quads);

            GL.Normal3(0f, 0f, 1f);

            GL.TexCoord2(0f, 1f);
            GL.Vertex3(0.035f, -0.1f, 0.901f);

            GL.TexCoord2(1f, 1f);
            GL.Vertex3(0.085f, -0.1f, 0.901f);

            GL.TexCoord2(1f, 0f);
            GL.Vertex3(0.085f, 0.06f, 0.901f);

            GL.TexCoord2(0f, 0f);
            GL.Vertex3(0.035f, 0.06f, 0.901f);

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
                _windowTexture = texture.LoadTexture(
                    "textures/home-window-open.png",
                    TextureMagFilter.LinearDetailSgis,
                    TextureMinFilter.Linear
                );
                _ventilationTexture = texture.LoadTexture(
                    "textures/ventilation.jpg",
                    TextureMagFilter.LinearDetailSgis,
                    TextureMinFilter.Linear
                );
                _doorTexture = texture.LoadTexture(
                    "textures/door.jpg",
                    TextureMagFilter.LinearDetailSgis,
                    TextureMinFilter.Linear
                );
                _texturesInitialized = true;
            }
        }
    }
}
