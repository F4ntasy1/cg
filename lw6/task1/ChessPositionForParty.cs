using OpenTK.Mathematics;

namespace task6_1
{
    public class ChessPositionForParty(
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
}
