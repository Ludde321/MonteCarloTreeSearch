using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Mcts
{
    public interface INode
    {
        long NumVisits {get;set;}
        long Score {get;set;}

        INode[] Children {get;set;}

        double ComputeUtc(INode child);

        INode SelectPromisingNode();
    }

}