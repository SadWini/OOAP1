using System;
using Xunit;

namespace OOAP
{
    public class DequeTests
    {
        [Fact]
        public void AddFront_ValidCase()
        {
            Deque<int> deque = new Deque<int>();
            
            deque.addFront(10);
            deque.addFront(20); 
            
            Assert.Equal(10, deque.getTail());
            Assert.Equal(Deque<int>.GET_TAIL_OK, deque.get_get_tail_status());
            Assert.Equal(20, deque.getFront());
            Assert.Equal(Deque<int>.GET_OK, deque.get_get_status());
        }

        [Fact] 
        public void RemoveTail_NotEmptyDeque_ValidCase()
        {
            Deque<int> deque = new Deque<int>();
            deque.addFront(10);
            deque.removeTail();

            Assert.Equal(Deque<int>.REMOVE_TAIL_OK, deque.get_remove_tail_status());
            Assert.Equal(0, deque.size());
        }

        [Fact]
        public void RemoveTail_EmptyDeque_ReturnsError()
        {
            Deque<int> deque = new Deque<int>();

            deque.removeTail();

            Assert.Equal(Deque<int>.REMOVE_TAIL_ERR, deque.get_remove_tail_status());
            Assert.Equal(0, deque.size());
        }

        [Fact]
        public void GetTail_EmptyDeque_ReturnsError()
        {
            Deque<int> deque = new Deque<int>();

            int val = deque.getTail();

            Assert.Equal(Deque<int>.GET_TAIL_ERR, deque.get_get_tail_status());
            Assert.Equal(default(int), val);
        }

        [Fact]
        public void Clear_ResetsAllDequeAndQueueStatuses()
        {
            Deque<int> deque = new Deque<int>();
            deque.addFront(10);
            deque.removeTail(); 
            
            deque.clear();

            Assert.Equal(0, deque.size());
            Assert.Equal(Deque<int>.REMOVE_TAIL_NIL, deque.get_remove_tail_status());
            Assert.Equal(Deque<int>.GET_TAIL_NIL, deque.get_get_tail_status());            
            Assert.Equal(ParentQueueATD<int>.DEQUEUE_NIL, deque.get_dequeue_status());
        }

        [Fact]
        public void Aliases_WorkAccordingToDequeSemantics_ValidCase()
        {
            Deque<int> deque = new Deque<int>();
            
            deque.addTail(10);   
            deque.addFront(20);  
            deque.addTail(30);   

            Assert.Equal(20, deque.getFront());
            Assert.Equal(30, deque.getTail());
            
            deque.removeFront(); 
            Assert.Equal(10, deque.getFront());
            
            deque.removeTail(); 
            Assert.Equal(10, deque.getTail());
            Assert.Equal(1, deque.size());
        }
    }
}