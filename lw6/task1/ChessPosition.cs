using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task6_1
{
    public class ChessPosition(Figure figure, Party party, int horizontalIndex, int verticalIndex)
    {
        public Figure figure = figure;
        public Party party = party;
        public int horizontalIndex = horizontalIndex;
        public int verticalIndex = verticalIndex;
    }
}
