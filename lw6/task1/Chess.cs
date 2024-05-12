using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Runtime.InteropServices;

namespace task6_1
{
    public class MoveFigureInfo(Figure figure, Party party, Vector2 startPos, Vector2 moveTo)
    {
        public Figure figure = figure;
        public Party party = party;
        public Vector2 startPos = startPos;
        public Vector2 moveTo = moveTo;
    }

    public class Chess : IDrawable
    {
        public List<List<Vector2>> positions = [];
        public ChessPositionForParty whitePosition;
        public ChessPositionForParty blackPosition;

        private IStrategy gameStrategy;

        private Model rook = new("models/Rook.obj");
        private Model knight = new("models/Knight.obj");
        private Model bishop = new("models/Bishop.obj");
        private Model queen = new("models/Queen.obj");
        private Model king = new("models/King.obj");
        private Model pawn = new("models/Pawn.obj");
        private Model board = new("models/ChessBoard.obj");

        private Color4 blackColor = new(0.2f, 0.2f, 0.2f, 1f);
        private Color4 whiteColor = Color4.White;

        private MoveFigureInfo? moveFigureInfo = null;
        private bool gameIsOn = false;

        private static float secondsBetweenMoves = 1f;
        private static float animationStep = 80f * secondsBetweenMoves;

        private float maxFigureHeight = 60f;

        public Chess()
        {
            FillAllChessPositions();
            FillWhiteAndBlackDefaultPositions();
        }

        public void SetGameStrategy(IStrategy strategy)
        {
            gameStrategy = strategy;
        }

        public async void Play()
        {
            if (gameIsOn)
            {
                throw new Exception("Game already on!");
            }

            gameIsOn = true;
            while (gameStrategy.HasNext())
            {
                var oneSecond = Task.Delay(TimeSpan.FromSeconds(secondsBetweenMoves));
                                                                     
                ChessPosition chessPosition = gameStrategy.Next();
                MoveFigure(chessPosition.figure, chessPosition.party, 
                    positions[chessPosition.verticalIndex][chessPosition.horizontalIndex]);

                await oneSecond;
            }
            gameIsOn = false;
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

            AnimateMovement();
        }

        public void MoveFigure(Figure figure, Party party, Vector2 pos)
        {
            moveFigureInfo = new(figure, party, GetFigurePosition(figure, party), pos);

            Party anotherParty = party == Party.White ? Party.Black : Party.White;

            if (FigureBeatsAnother(anotherParty, pos))
            {
                ref Vector2 otherPartyPosition = ref FindFigurePosition(anotherParty, pos);
                otherPartyPosition.X = 1000;
                otherPartyPosition.Y = 1000;
            }
        }

        private void AnimateMovement()
        {
            if (moveFigureInfo == null) return;

            ref Vector2 figurePos = ref GetFigurePosition(moveFigureInfo.figure, moveFigureInfo.party);

            float moveByX = moveFigureInfo.moveTo.X - moveFigureInfo.startPos.X;
            float moveByY = moveFigureInfo.moveTo.Y - moveFigureInfo.startPos.Y;

            float stepByX = moveByX / animationStep;
            float stepByY = moveByY / animationStep;

            if (moveFigureInfo.figure != Figure.LeftKnight && moveFigureInfo.figure != Figure.RightKnight)
            {
                // Обычные фигуры
                figurePos.X += stepByX;
                figurePos.Y += stepByY;
            }
            else
            {
                // Кони
                float positiveMoveByX = moveByX > 0 ? moveByX : -moveByX;
                float positiveMoveByY = moveByY > 0 ? moveByY : -moveByY;

                if (positiveMoveByX > positiveMoveByY)
                {
                    // Сначала движение по X
                    if (figurePos.X != moveFigureInfo.moveTo.X)
                        figurePos.X += stepByX;
                    else
                        figurePos.Y += stepByY;
                }
                else
                {
                    // Сначала движение по Y
                    if (figurePos.Y != moveFigureInfo.moveTo.Y)
                        figurePos.Y += stepByY;
                    else
                        figurePos.X += stepByX;
                }
            }

            // Проверка на границы
            if ((moveByX > 0 && figurePos.X + stepByX > moveFigureInfo.moveTo.X) ||
                (moveByX < 0 && figurePos.X + stepByX < moveFigureInfo.moveTo.X))
            {
                figurePos.X = moveFigureInfo.moveTo.X;
            }
            if ((moveByY > 0 && figurePos.Y + stepByY > moveFigureInfo.moveTo.Y) ||
                (moveByY < 0 && figurePos.Y + stepByY < moveFigureInfo.moveTo.Y))
            {
                figurePos.Y = moveFigureInfo.moveTo.Y;
            }

            // Если закончилось движение, то установить `null`
            if (figurePos.Y == moveFigureInfo.moveTo.Y && figurePos.X == moveFigureInfo.moveTo.X)
            {
                moveFigureInfo = null;
            }
        }

        private ref Vector2 FindFigurePosition(Party party, Vector2 pos)
        {
            if (party == Party.White)
            {
                if (whitePosition.king == pos) return ref whitePosition.king;
                if (whitePosition.queen == pos) return ref whitePosition.queen;
                if (whitePosition.leftRook == pos) return ref whitePosition.leftRook;
                if (whitePosition.rightRook == pos) return ref whitePosition.rightRook;
                if (whitePosition.leftBishop == pos) return ref whitePosition.leftBishop;
                if (whitePosition.rightBishop == pos) return ref whitePosition.rightBishop;
                if (whitePosition.leftKnight == pos) return ref whitePosition.leftKnight;
                if (whitePosition.rightKnight == pos) return ref whitePosition.rightKnight;
                for (int i = 0; i < whitePosition.pawns.Count; i++)
                {
                    if (whitePosition.pawns[i] == pos) return ref CollectionsMarshal.AsSpan(whitePosition.pawns)[i];
                }
            }

            if (blackPosition.queen == pos) return ref blackPosition.queen;
            if (blackPosition.leftRook == pos) return ref blackPosition.leftRook;
            if (blackPosition.rightRook == pos) return ref blackPosition.rightRook;
            if (blackPosition.leftBishop == pos) return ref blackPosition.leftBishop;
            if (blackPosition.rightBishop == pos) return ref blackPosition.rightBishop;
            if (blackPosition.leftKnight == pos) return ref blackPosition.leftKnight;
            if (blackPosition.rightKnight == pos) return ref blackPosition.rightKnight;
            for (int i = 0; i < blackPosition.pawns.Count; i++)
            {
                if (blackPosition.pawns[i] == pos) return ref CollectionsMarshal.AsSpan(blackPosition.pawns)[i];
            }
            return ref blackPosition.king;
        }

        private bool FigureBeatsAnother(Party anotherParty, Vector2 figurePos)
        {
            if (anotherParty == Party.White)
            {
                return whitePosition.queen == figurePos || whitePosition.pawns.Exists(val => val == figurePos) ||
                    whitePosition.leftRook == figurePos || whitePosition.rightRook == figurePos ||
                    whitePosition.leftBishop == figurePos || whitePosition.rightBishop == figurePos ||
                    whitePosition.leftKnight == figurePos || whitePosition.rightKnight == figurePos;
            }
            return blackPosition.queen == figurePos || blackPosition.pawns.Exists(val => val == figurePos) ||
                    blackPosition.leftRook == figurePos || blackPosition.rightRook == figurePos ||
                    blackPosition.leftBishop == figurePos || blackPosition.rightBishop == figurePos ||
                    blackPosition.leftKnight == figurePos || blackPosition.rightKnight == figurePos;
        }

        private ref Vector2 GetFigurePosition(Figure figure, Party party)
        {
            if (party == Party.White)
            {
                switch (figure)
                {
                    case Figure.King: return ref whitePosition.king;
                    case Figure.Queen: return ref whitePosition.queen;
                    case Figure.LeftRook: return ref whitePosition.leftRook;
                    case Figure.RightRook: return ref whitePosition.rightRook;
                    case Figure.LeftBishop: return ref whitePosition.leftBishop;
                    case Figure.RightBishop: return ref whitePosition.rightBishop;
                    case Figure.LeftKnight: return ref whitePosition.leftKnight;
                    case Figure.RightKnight: return ref whitePosition.rightKnight;
                    case Figure.Pawn1: return ref CollectionsMarshal.AsSpan(whitePosition.pawns)[0];
                    case Figure.Pawn2: return ref CollectionsMarshal.AsSpan(whitePosition.pawns)[1];
                    case Figure.Pawn3: return ref CollectionsMarshal.AsSpan(whitePosition.pawns)[2];
                    case Figure.Pawn4: return ref CollectionsMarshal.AsSpan(whitePosition.pawns)[3];
                    case Figure.Pawn5: return ref CollectionsMarshal.AsSpan(whitePosition.pawns)[4];
                    case Figure.Pawn6: return ref CollectionsMarshal.AsSpan(whitePosition.pawns)[5];
                    case Figure.Pawn7: return ref CollectionsMarshal.AsSpan(whitePosition.pawns)[6];
                    default: return ref CollectionsMarshal.AsSpan(whitePosition.pawns)[7];
                }
            }

            switch (figure)
            {
                case Figure.King: return ref blackPosition.king;
                case Figure.Queen: return ref blackPosition.queen;
                case Figure.LeftRook: return ref blackPosition.leftRook;
                case Figure.RightRook: return ref blackPosition.rightRook;
                case Figure.LeftBishop: return ref blackPosition.leftBishop;
                case Figure.RightBishop: return ref blackPosition.rightBishop;
                case Figure.LeftKnight: return ref blackPosition.leftKnight;
                case Figure.RightKnight: return ref blackPosition.rightKnight;
                case Figure.Pawn1: return ref CollectionsMarshal.AsSpan(blackPosition.pawns)[0];
                case Figure.Pawn2: return ref CollectionsMarshal.AsSpan(blackPosition.pawns)[1];
                case Figure.Pawn3: return ref CollectionsMarshal.AsSpan(blackPosition.pawns)[2];
                case Figure.Pawn4: return ref CollectionsMarshal.AsSpan(blackPosition.pawns)[3];
                case Figure.Pawn5: return ref CollectionsMarshal.AsSpan(blackPosition.pawns)[4];
                case Figure.Pawn6: return ref CollectionsMarshal.AsSpan(blackPosition.pawns)[5];
                case Figure.Pawn7: return ref CollectionsMarshal.AsSpan(blackPosition.pawns)[6];
                default: return ref CollectionsMarshal.AsSpan(blackPosition.pawns)[7];
            }
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
            float zWhiteLeftKnight = 0f;
            float zWhiteRightKnight = 0f;
            float zBlackLeftKnight = 0f;
            float zBlackRightKnight = 0f;
            if (moveFigureInfo?.figure == Figure.LeftKnight && moveFigureInfo?.party == Party.White)
                zWhiteLeftKnight = maxFigureHeight;
            if (moveFigureInfo?.figure == Figure.RightKnight && moveFigureInfo?.party == Party.White)
                zWhiteRightKnight = maxFigureHeight;
            if (moveFigureInfo?.figure == Figure.LeftKnight && moveFigureInfo?.party == Party.Black)
                zBlackLeftKnight = maxFigureHeight;
            if (moveFigureInfo?.figure == Figure.RightKnight && moveFigureInfo?.party == Party.Black)
                zBlackRightKnight = maxFigureHeight;
            DrawModel(knight, whiteColor, new(whitePosition.leftKnight.X, whitePosition.leftKnight.Y, zWhiteLeftKnight), 180f);
            DrawModel(knight, whiteColor, new(whitePosition.rightKnight.X, whitePosition.rightKnight.Y, zWhiteRightKnight), 180f);
            DrawModel(knight, blackColor, new(blackPosition.leftKnight.X, blackPosition.leftKnight.Y, zBlackLeftKnight));
            DrawModel(knight, blackColor, new(blackPosition.rightKnight.X, blackPosition.rightKnight.Y, zBlackRightKnight));
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
            if (translateTo.X == 1000 && translateTo.Y == 1000) return;

            GL.PushMatrix();
            GL.Translate(translateTo);
            GL.Rotate(rotateAngle, 0f, 0f, 1f);
            GL.Color4(color);
            m.RenderModel();
            GL.PopMatrix();
        }
    }
}
