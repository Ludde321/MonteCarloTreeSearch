using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe.Mcts
{
    public interface INode<TNode> where TNode : INode<TNode>
    {
        long NumVisits {get;set;}
        long Score {get;set;}

        TNode[] Children {get;set;}

        double ComputeUtc(TNode child);

        TNode SelectPromisingNode();
    }

}