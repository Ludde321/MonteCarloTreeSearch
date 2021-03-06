using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Mcts;

namespace TicTacToe
{
    public class Node : INode
    {
        public long NumVisits {get;set;}

        public long Score {get;set;}

        public INode[] Children {get;set;}

        public byte StoneTo;

        public const double C = 1.4142135623730950488016887242097;

        public double ComputeUtc(INode child)
        {
            return child.Score / (double)child.NumVisits + C * Math.Sqrt(Math.Log(NumVisits) / child.NumVisits);
        }

        public INode SelectPromisingNode()
        {
            Node bestChild = null;
            double bestUtc = double.MinValue;

            foreach(Node child in Children)
            {
                if(child.NumVisits == 0)
                    return child;
                    
                double utc = ComputeUtc(child);
                if(utc > bestUtc)
                {
                    bestUtc = utc;
                    bestChild = child;
                }
            }

            return bestChild;
        }

    }

}