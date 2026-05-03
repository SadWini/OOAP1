using System;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

namespace OOAP
{
    public class BoundedStackTests
    {
        [Fact]
        public void Constructor_ValidCapacity_CreatedStack()
        {
            int cap = 10;
            BoundedStack<int> stack = new BoundedStack<int>(cap);
            Assert.Equal(cap, stack.capacity());
            Assert.Equal(BoundedStackATD<int>.PEEK_NIL, stack.get_peek_status());
            Assert.Equal(BoundedStackATD<int>.POP_NIL, stack.get_pop_status());
            Assert.Equal(BoundedStackATD<int>.PUSH_NIL, stack.get_push_status());
        }
        
        [Fact]
        public void Constructor_IllegalCapacity_ThrowsArgumentOutOfRangeException()
        {
            int invalid_cap = 0;
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                BoundedStack<int> stack = new BoundedStack<int>(invalid_cap);
            });
        }

        [Fact]
        public void Push_ValidCase_ElementInserted()
        {
            BoundedStack<int> stack = new BoundedStack<int>();
            int val = 20;
            stack.push(val);
            Assert.Equal(BoundedStackATD<int>.PUSH_OK, stack.get_push_status());
            Assert.Equal(val, stack.peek());
        }

        [Fact]
        public void Push_FullStack_FailedInsert()
        {
            BoundedStack<int> stack = new BoundedStack<int>();
            for (int i = 0; i < stack.capacity(); i++)
                stack.push(i);
            stack.push(20);
            Assert.Equal(BoundedStackATD<int>.PUSH_ERR, stack.get_push_status());
        }

        [Fact]
        public void Pop_NotEmptyStack_RemovedElement()
        {
            int value = 10;
            BoundedStack<int> stack = new BoundedStack<int>();
            stack.push(value);
            stack.pop();
            Assert.Equal(BoundedStack<int>.POP_OK, stack.get_pop_status());
            Assert.Equal(0, stack.size());
        }

        [Fact]
        public void Pop_EmptyStack_FailedToRemoveElement()
        {
            BoundedStack<int> stack = new BoundedStack<int>();
            stack.pop();
            Assert.Equal(BoundedStack<int>.POP_ERR, stack.get_pop_status());
        }

        [Fact]
        public void Peek_NotEmptyStack_ReturnedElement()
        {
            int value = 10;
            BoundedStack<int> stack = new BoundedStack<int>();
            stack.push(value);
            Assert.Equal(value, stack.peek());
            Assert.Equal(BoundedStack<int>.PEEK_OK, stack.get_peek_status());
        }

        [Fact]
        public void Peek_EmptyStack_FailedToFind()
        {
            BoundedStack<int> stack = new BoundedStack<int>();
            stack.peek();
            Assert.Equal(BoundedStack<int>.PEEK_ERR, stack.get_peek_status());
        }
    }
}