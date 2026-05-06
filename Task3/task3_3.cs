using System;
using Xunit;

namespace OOAP
{
    public class TwoWayListTests
    {
        [Fact] 
        public void AddToEmpty_EmptyList_ValidCase()
        {
            TwoWayList<int> list = new TwoWayList<int>();
            list.add_to_empty(10);
            Assert.Equal(TwoWayList<int>.ADD_TO_EMPTY_OK, list.get_add_to_empty_status());
            Assert.Equal(1, list.size());    
        }

        [Fact] 
        public void AddToEmpty_NotEmptyList_ReturnsError()
        {
            TwoWayList<int> list = new TwoWayList<int>();
            list.add_to_empty(10);
            list.add_to_empty(10);
            Assert.Equal(TwoWayList<int>.ADD_TO_EMPTY_ERR, list.get_add_to_empty_status());
            Assert.Equal(1, list.size());    
        }

        [Fact] 
        public void PutLeft_NotEmptyList_ValidCase()
        {
            TwoWayList<int> list = new TwoWayList<int>();
            list.add_to_empty(10);
            list.put_left(20);
            Assert.Equal(TwoWayList<int>.PUT_LEFT_OK, list.get_put_left_status());
            list.left();
            Assert.Equal(20, list.get());
        }

        [Fact]
        public void PutLeft_EmptyList_ReturnsError()
        {
            TwoWayList<int> list = new TwoWayList<int>();
            list.put_left(10);
            Assert.Equal(TwoWayList<int>.PUT_LEFT_ERR, list.get_put_left_status());
            Assert.Equal(0, list.size());
        }
        
        [Fact]
        public void Right_OutOfBounds_ReturnError()
        {
            TwoWayList<int> list = new TwoWayList<int>();
            list.add_tail(1);
            list.add_tail(2);

            list.tail();
            list.right();
            Assert.Equal(LinkedListATD<int>.RIGHT_ERR_NOT_RIGHTER_ELEMENT, list.get_right_status());
            Assert.True(list.is_tail());
        }

                [Fact]
        public void Left_OutOfBounds_ReturnError()
        {
            TwoWayList<int> list = new TwoWayList<int>();
            list.add_tail(1);
            list.add_tail(2);

            list.head();
            list.left();
            Assert.Equal(TwoWayList<int>.LEFT_ERR_NOT_LEFTER, list.get_left_status());
            Assert.True(list.is_head());
        }

        [Fact]
        public void Remove_OnlyElement_CursorInvalidListEmpty()
        {
            TwoWayList<int> list = new TwoWayList<int>();
            list.add_to_empty(10);
            list.remove();
            Assert.Equal(LinkedListATD<int>.REMOVE_OK, list.get_remove_status());
            Assert.Equal(0, list.size());
            Assert.False(list.is_value());
        }

        [Fact] 
        public void Find_MultipleElements_MovesToNextMatch()
        {
            TwoWayList<int> list = new TwoWayList<int>();
            list.add_tail(1);
            list.add_tail(2);
            list.add_tail(1);

            list.head();
            list.find(1);

            Assert.Equal(LinkedListATD<int>.FIND_OK, list.get_find_status());
            Assert.True(list.is_tail());
            Assert.Equal(1, list.get());    
        }

        [Fact] 
        public void Find_ElementNotExist_CursorStaysReturnError()
        {
            TwoWayList<int> list = new TwoWayList<int>();
            list.add_tail(1);
            list.add_tail(2);

            list.head();
            list.find(0);
            Assert.Equal(LinkedListATD<int>.FIND_ERR_NOT_FOUND, list.get_find_status());
            Assert.Equal(1, list.get());
        }

        [Fact]
        public void RemoveAll_ElementUnderCursor_CursorStaysValid()
        {
            TwoWayList<int> list = new TwoWayList<int>();
            list.add_tail(10);
            list.add_tail(10);
            list.add_tail(20);
            list.add_tail(30);
            list.head();

            list.remove_all(10);
            Assert.Equal(2, list.size());
            Assert.Equal(20, list.get());
        }
    }
}