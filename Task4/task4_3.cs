using System;
using Xunit;

namespace OOAP
{
    public class DynArrayTests
    {
        [Fact]
        public void Insert_AtEnd_ValidCase()
        {
            DynArray<int> arr = new DynArray<int>();
            arr.insert(0, 2);
            Assert.Equal(DynArrayATD<int>.INSERT_OK, arr.get_insert_status());
            Assert.Equal(1, arr.size());
            Assert.Equal(2, arr.get(0));
        }

        [Fact]
        public void Insert_OutOfBounds_ReturnsError()
        {
            DynArray<int> arr = new DynArray<int>();
            arr.append(10);
            arr.insert(3, 30);
            
            Assert.Equal(DynArrayATD<int>.INSERT_ERR, arr.get_insert_status());
            Assert.Equal(1, arr.size()); 
        }

        [Fact]
        public void Append_OverCapacity_DoublesBuffer()
        {
            DynArray<int> arr = new DynArray<int>(); 
            
            for (int i = 0; i < 16; i++)
            {
                arr.append(i);
            }
            Assert.Equal(16, arr.capacity());

            arr.append(17);
            
            Assert.Equal(17, arr.size());
            Assert.Equal(32, arr.capacity());
            Assert.Equal(17, arr.get(16));
        }

        [Fact]
        public void Remove_Below50Percent_ShrinksBufferByOnePointFive()
        {
            DynArray<int> arr = new DynArray<int>();
            
            for (int i = 0; i < 17; i++) arr.append(i); 
            Assert.Equal(32, arr.capacity());

            arr.remove(16);
            Assert.Equal(32, arr.capacity()); 

            arr.remove(15);
            Assert.Equal(21, arr.capacity());
        }

        [Fact]
        public void Remove_Below50Percent_BufferDoesNotGoBelow16()
        {
            DynArray<int> arr = new DynArray<int>(); 
            
            for (int i = 0; i < 10; i++) arr.append(i);
            
            arr.remove(0);
            arr.remove(0);
            arr.remove(0);

            Assert.Equal(7, arr.size());
            Assert.Equal(16, arr.capacity()); 
        }

        [Fact]
        public void Remove_OutOfBounds_ReturnsError()
        {
            DynArray<int> arr = new DynArray<int>();
            arr.append(10);
            arr.append(20);

            arr.remove(2);
            Assert.Equal(DynArrayATD<int>.REMOVE_ERR, arr.get_remove_status());
        }

        [Fact]
        public void Get_OutOfBounds_ReturnsError()
        {
            DynArray<int> arr = new DynArray<int>();
            arr.append(10);
            arr.append(20);

            arr.get(3);
            Assert.Equal(DynArrayATD<int>.GET_ERR, arr.get_get_status());
        }        
    }
}