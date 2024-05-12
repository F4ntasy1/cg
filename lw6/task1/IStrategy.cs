using task6_1;

namespace task6_1
{
    public interface IStrategy
    {
        ChessPosition Next();

        bool HasNext();
    }
}
