using System;
using _Project.Scripts.GameElements;

namespace _Project.Scripts
{
    public class ZeroNode : CircleNode
    {
        public override void MarkAsVisited()
        {
            _isVisited = false;
        }

        public override void ResetVisit()
        {
            _isVisited = false;
        }
    }
}