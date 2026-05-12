using System;
using Xunit;

namespace OOAP
{
    public class QueueTests
    { 
        [Fact]
        public void Dequeue_NotEmptyQueue_ValidCase()
        {
            Queue<int> queue = new Queue<int>();
            queue.enqueue(10);
            queue.enqueue(20);
            queue.enqueue(30);
            queue.dequeue();

            Assert.Equal(QueueATD<int>.DEQUEUE_OK, queue.get_dequeue_status());
            Assert.Equal(2, queue.size());    
        }

        [Fact]
        public void Dequeue_EmptyQueue_ReturnsError()
        {
            Queue<int> queue = new Queue<int>();
            queue.dequeue();

            Assert.Equal(QueueATD<int>.DEQUEUE_ERR, queue.get_dequeue_status());
            Assert.Equal(0, queue.size());
        }

        [Fact]
        public void Get_NotEmptyQueue_ValidCase()
        {
            Queue<int> queue = new Queue<int>();
            queue.enqueue(10);
            queue.enqueue(20);
            queue.get();

            Assert.Equal(QueueATD<int>.GET_OK, queue.get_get_status());
            Assert.Equal(10, queue.get());
        } 

        [Fact]
        public void Get_EmptyQueue_ReturnsError()
        {
            Queue<int> queue = new Queue<int>();
            queue.get();

            Assert.Equal(QueueATD<int>.GET_ERR, queue.get_get_status());
        }

        [Fact]
        public void Clear_ResetsSizeAndStatuses_ValidCase()
        {
            Queue<int> queue = new Queue<int>();
            queue.enqueue(10);
            queue.dequeue();

            queue.clear();

            Assert.Equal(0, queue.size());
            Assert.Equal(QueueATD<int>.DEQUEUE_NIL, queue.get_dequeue_status());
            Assert.Equal(QueueATD<int>.GET_NIL, queue.get_get_status());
        }
    }
}