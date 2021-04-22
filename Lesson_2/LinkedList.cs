using System;

namespace Lesson_2
{
    public class BlankListException : Exception
    {
        public BlankListException()
        {
            HResult = (int)ExceptionHResult.BlankListException;
        }
    }
    public class UnexistentNodeException : Exception
    {
        public UnexistentNodeException()
        {
            HResult = (int)ExceptionHResult.UnexistentNodeException;
        }
    }
    class LinkedList : ILinkedList
    {
        private Node _startNode;
        private Node _endNode;
        private int _count;
        public LinkedList()
        {
            _startNode = null;
            _endNode = null;
            _count = 0;
        }
        public void AddNode(int value)
        {
            var newNode = new Node(value, _endNode, null);
            if (_startNode == null)
            {
                _startNode = newNode;
            }
            if (_endNode != null)
            {
                newNode.PrevNode = _endNode;
                _endNode.NextNode = newNode;
            }
            _endNode = newNode;
            _count++;
        }

        public void AddNodeAfter(Node node, int value)
        {
            CheckBlankList();

            Node searchNode = _startNode;
            do
            {
                if (searchNode == node)
                {
                    var newNode = new Node(value, searchNode, searchNode.NextNode);
                    newNode.NextNode = searchNode.NextNode;
                    newNode.PrevNode = searchNode;
                    searchNode.NextNode = newNode;
                    if (searchNode == _endNode)
                    {
                        _endNode = newNode;
                    }
                    _count++;
                    return;
                }
                searchNode = searchNode.NextNode;
            } while (searchNode != null);

            throw new UnexistentNodeException();
        }

        public Node FindNode(int searchValue)
        {
            CheckBlankList();

            Node searchNode = _startNode;
            do
            {
                if (searchNode.Value == searchValue)
                {
                    return searchNode;
                }
                searchNode = searchNode.NextNode;
            } while (searchNode != null);

            throw new UnexistentNodeException();
        }
        public Node FindNodeFromIndex(int index)
        {
            CheckBlankList();
            if (index > _count - 1)
            {
                throw new IndexOutOfRangeException();
            }

            Node searchNode = _startNode;
            for (int i = 0; i < index; i++)
            {
                searchNode = searchNode.NextNode;
            }
            return searchNode;
        }

        public int GetCount()
        {
            return _count;
        }
        public Node GetStartNode()
        {
            return _startNode;
        }
        public Node GetEndNode()
        {
            return _endNode;
        }
        public void RemoveNode(int index)
        {
            CheckBlankList();
            if (index > _count - 1)
            {
                throw new IndexOutOfRangeException();
            }

            Node searchNode = _startNode;
            for (int i = 0; i < index; i++)
            {
                searchNode = searchNode.NextNode;
            }
            if (searchNode == _startNode)
            {
                _startNode = searchNode.NextNode;
            }
            else
            {
                searchNode.PrevNode.NextNode = searchNode.NextNode;
            }
            if (searchNode == _endNode)
            {
                _endNode = searchNode.PrevNode;
            }
            else
            {
                searchNode.NextNode.PrevNode = searchNode.PrevNode;
            }
            searchNode.PrevNode = null;
            searchNode.NextNode = null;
            _count--;
        }

        public void RemoveNode(Node node)
        {
            CheckBlankList();

            Node searchNode = _startNode;
            do
            {
                if (searchNode == node)
                {
                    if (searchNode == _startNode)
                    {
                        _startNode = searchNode.NextNode;
                    }
                    else
                    {
                        searchNode.PrevNode.NextNode = searchNode.NextNode;
                    }
                    if (searchNode == _endNode)
                    {
                        _endNode = searchNode.PrevNode;
                    }
                    else
                    {
                        searchNode.NextNode.PrevNode = searchNode.PrevNode;
                    }
                    searchNode.PrevNode = null;
                    searchNode.NextNode = null;
                    _count--;
                    return;
                }
                searchNode = searchNode.NextNode;
            } while (searchNode != null);

            throw new UnexistentNodeException();
        }
        private void CheckBlankList()
        {
            if (_startNode == null)
            {
                throw new BlankListException();
            }
        }
    }
}
