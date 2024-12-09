using System.Collections.Generic;


namespace _Project.Scripts.GameElements
{
    public class LevelManager 
    {
        private readonly List<CircleNode> _levelElements;
        private readonly CircleNode _startElement;
        private readonly CircleNode _endElement;

        public LevelManager(CircleNode startElement, CircleNode endElement, List<CircleNode> levelElements)
        {
            _startElement = startElement;
            _endElement = endElement;
            _levelElements = levelElements;
        }

        public void VisitElement(CircleNode element)
        {
            if (!_levelElements.Contains(element)) return;
            element.MarkAsVisited();
        }

        public bool IsLevelCompleted()
        {
            // Проверяем, посещены ли все элементы и конечная точка
            foreach (var element in _levelElements)
            {
                if (!element.IsVisited) return false;
            }

            return _endElement.IsVisited;
        }

        public void ResetLevel()
        {
            foreach (var element in _levelElements)
            {
                element.ResetVisit();
            }

            _startElement.ResetVisit();
            _endElement.ResetVisit();
        }
    }
}