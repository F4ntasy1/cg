using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task6_1
{
    public class ChessGame1 : IStrategy
    {
        private List<ChessPosition> positions = [
            new(Figure.Pawn5, Party.White, 4, 3),
            new(Figure.Pawn5, Party.Black, 4, 4),
            new(Figure.RightKnight, Party.White, 5, 2),
            new(Figure.LeftKnight, Party.Black, 2, 5),
            new(Figure.Pawn4, Party.White, 3, 3),
            new(Figure.Pawn5, Party.Black, 3, 3),
            new(Figure.RightKnight, Party.White, 3, 3),
            new(Figure.RightKnight, Party.Black, 4, 6),
            new(Figure.LeftKnight, Party.White, 2, 2),
            new(Figure.Pawn7, Party.Black, 6, 5),
            new(Figure.LeftBishop, Party.White, 6, 4),
            new(Figure.RightBishop, Party.Black, 6, 6),
            new(Figure.LeftKnight, Party.White, 3, 4),
            new(Figure.RightBishop, Party.Black, 3, 3),
            new(Figure.Queen, Party.White, 3, 3),
            new(Figure.LeftKnight, Party.Black, 3, 3),
            new(Figure.LeftKnight, Party.White, 5, 5),
            new(Figure.King, Party.Black, 5, 7),
            new(Figure.LeftBishop, Party.White, 7, 5),
        ];
        private int currIndex = 0;

        public bool HasNext()
        {
            return currIndex < positions.Count;
        }

        public ChessPosition Next()
        {
            return positions[currIndex++];
        }
    }
}
