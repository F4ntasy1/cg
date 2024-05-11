using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace task6_1
{
    public class ChessPosition(
        Vector2 leftRook, 
        Vector2 rightRook, 
        Vector2 leftKnight, 
        Vector2 rightKnight,
        Vector2 leftBishop,
        Vector2 rightBishop,
        Vector2 queen,
        Vector2 king,
        List<Vector2> pawns
    )
    {
        public Vector2 leftRook = leftRook; 
        public Vector2 rightRook = rightRook;
        public Vector2 leftKnight = leftKnight;
        public Vector2 rightKnight = rightKnight;
        public Vector2 leftBishop = leftBishop;
        public Vector2 rightBishop = rightBishop;
        public Vector2 queen = queen;
        public Vector2 king = king;
        public List<Vector2> pawns = pawns;
    }

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

        private List<List<Vector2>> positions = [];
        private ChessPosition whitePosition;
        private ChessPosition blackPosition;

        public Chess()
        {
            FillAllChessPositions();
            FillWhiteAndBlackDefaultPositions();
        }

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

        /// <summary>
        /// Заполняет позиции на шахматной доске с 1 по 8 горизонтали.
        /// Позиции на горизонтале заполняются в порядке от "a" до "h".
        /// </summary>
        private void FillAllChessPositions()
        {
            int step = 40;
            for (int y = -140; y <= 140; y += step)
            {
                List<Vector2> row = [];
                for (int x = -140; x <= 140; x += step)
                {
                    row.Add(new(x, y));
                }
                positions.Add(row);
            }
        }

        private void FillWhiteAndBlackDefaultPositions()
        {
            List<Vector2> whitePawns = [];
            List<Vector2> blackPawns = [];
            for (int i = 0; i <= 7; i++)
            {
                whitePawns.Add(positions[1][i]);
                blackPawns.Add(positions[6][i]);
            }

            whitePosition = new(
                positions[0][0],
                positions[0][7],
                positions[0][1],
                positions[0][6],
                positions[0][2],
                positions[0][5],
                positions[0][3],
                positions[0][4],
                whitePawns
            );
            blackPosition = new(
                positions[7][0],
                positions[7][7],
                positions[7][1],
                positions[7][6],
                positions[7][2],
                positions[7][5],
                positions[7][3],
                positions[7][4],
                blackPawns
            );
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
            DrawModel(king, whiteColor, new(whitePosition.king.X, whitePosition.king.Y, 0f));
            DrawModel(king, blackColor, new(blackPosition.king.X, blackPosition.king.Y, 0f));
        }

        private void DrawQueens()
        {
            DrawModel(queen, whiteColor, new(whitePosition.queen.X, whitePosition.queen.Y, 0f));
            DrawModel(queen, blackColor, new(blackPosition.queen.X, blackPosition.queen.Y, 0f));
        }

        private void DrawRooks()
        {
            DrawModel(rook, whiteColor, new(whitePosition.leftRook.X, whitePosition.leftRook.Y, 0f), 180f);
            DrawModel(rook, whiteColor, new(whitePosition.rightRook.X, whitePosition.rightRook.Y, 0f), 180f);
            DrawModel(rook, blackColor, new(blackPosition.leftRook.X, blackPosition.leftRook.Y, 0f));
            DrawModel(rook, blackColor, new(blackPosition.rightRook.X, blackPosition.rightRook.Y, 0f));
        }

        private void DrawKhights()
        {
            DrawModel(knight, whiteColor, new(whitePosition.leftKnight.X, whitePosition.leftKnight.Y, 0f), 180f);
            DrawModel(knight, whiteColor, new(whitePosition.rightKnight.X, whitePosition.rightKnight.Y, 0f), 180f);
            DrawModel(knight, blackColor, new(blackPosition.leftKnight.X, blackPosition.leftKnight.Y, 0f));
            DrawModel(knight, blackColor, new(blackPosition.rightKnight.X, blackPosition.rightKnight.Y, 0f));
        }

        private void DrawBishops()
        {
            DrawModel(bishop, whiteColor, new(whitePosition.leftBishop.X, whitePosition.leftBishop.Y, 0f));
            DrawModel(bishop, whiteColor, new(whitePosition.rightBishop.X, whitePosition.rightBishop.Y, 0f));
            DrawModel(bishop, blackColor, new(blackPosition.leftBishop.X, blackPosition.leftBishop.Y, 0f));
            DrawModel(bishop, blackColor, new(blackPosition.rightBishop.X, blackPosition.rightBishop.Y, 0f));
        }

        private void DrawPawns()
        {
            foreach (var position in whitePosition.pawns)
            {
                DrawModel(pawn, whiteColor, new(position.X, position.Y, 0f));
            }
            foreach (var position in blackPosition.pawns)
            {
                DrawModel(pawn, blackColor, new(position.X, position.Y, 0f));
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
