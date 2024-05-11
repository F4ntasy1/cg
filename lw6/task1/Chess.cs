using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace task6_1
{
    public class Chess : IDrawable
    {
        public Model rook = new("models/Rook.obj");
        public Model knight = new("models/Knight.obj");
        public Model bishop = new("models/Bishop.obj");
        public Model queen = new("models/Queen.obj");
        public Model king = new("models/King.obj");
        public Model pawn = new("models/Pawn.obj");
        public Model board = new("models/ChessBoard.obj");

        private Color4 blackColor = new(0.2f, 0.2f, 0.2f, 1f);
        private Color4 whiteColor = Color4.White;

        private float quadSize = 40f;

        public void Draw()
        {
            GL.PushMatrix();
            GL.Scale(0.008f, 0.008f, 0.008f);

            DrawBoard();

            GL.PushMatrix();
            GL.Translate(0f, 20f, 0f);
            GL.Rotate(-90f, 1f, 0f, 0f);

            DrawPawns();
            DrawRooks();
            DrawKhights();
            DrawBishops();
            DrawKings();
            DrawQueens();

            GL.PopMatrix();
            GL.PopMatrix();
        }

        private void DrawBoard()
        {
            GL.PushMatrix();
            GL.Scale(90f, 90f, 90f);
            GL.Color4(whiteColor);
            board.RenderModel();
            GL.PopMatrix();
        }

        private void DrawKings()
        {
            float x = quadSize / 2;
            float y = quadSize * 3 + quadSize / 2;

            DrawModel(king, whiteColor, new(x, -y, 0f));
            DrawModel(king, blackColor, new(x, y, 0f));
        }

        private void DrawQueens()
        {
            float x = -quadSize / 2;
            float y = quadSize * 3 + quadSize / 2;

            DrawModel(queen, whiteColor, new(x, -y, 0f));
            DrawModel(queen, blackColor, new(x, y, 0f));
        }

        private void DrawRooks()
        {
            float x = quadSize * 3 + quadSize / 2;
            float y = quadSize * 3 + quadSize / 2;

            DrawModel(rook, blackColor, new(x, y, 0f));
            DrawModel(rook, blackColor, new(-x, y, 0f));

            DrawModel(rook, whiteColor, new(-x, -y, 0f));
            DrawModel(rook, whiteColor, new(x, -y, 0f));
        }

        private void DrawKhights()
        {
            float x = quadSize * 2 + quadSize / 2;
            float y = quadSize * 3 + quadSize / 2;

            DrawModel(knight, blackColor, new(x, y, 0f));
            DrawModel(knight, blackColor, new(-x, y, 0f));

            DrawModel(knight, whiteColor, new(-x, -y, 0f), 180f);
            DrawModel(knight, whiteColor, new(x, -y, 0f), 180f);
        }

        private void DrawBishops()
        {
            float x = quadSize + quadSize / 2;
            float y = quadSize * 3 + quadSize / 2;

            DrawModel(bishop, blackColor, new(x, y, 0f));
            DrawModel(bishop, blackColor, new(-x, y, 0f));

            DrawModel(bishop, whiteColor, new(-x, -y, 0f));
            DrawModel(bishop, whiteColor, new(x, -y, 0f));
        }

        private void DrawPawns()
        {
            for (int i = -4; i < 4; i++)
            {
                float x = i * quadSize + quadSize / 2;
                float y = quadSize * 2 + quadSize / 2;

                DrawModel(pawn, blackColor, new(x, y, 0f));
                DrawModel(pawn, whiteColor, new(x, -y, 0f));
            }
        }

        private void DrawModel(Model m, Color4 color, Vector3 translateTo, float rotateAngle = 0f)
        {
            GL.PushMatrix();
            GL.Translate(translateTo);
            GL.Rotate(rotateAngle, 0f, 0f, 1f);
            GL.Color4(color);
            m.RenderModel();
            GL.PopMatrix();
        }
    }
}
