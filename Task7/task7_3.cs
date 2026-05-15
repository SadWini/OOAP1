using System;
using Xunit;

namespace OOAP
{
    public class HashTableTests
    {
        [Fact]
        public void Put_ValidValues_ValidCase()
        {
            HashTable<string> ht = new HashTable<string>(17);
            ht.put("apple");

            Assert.Equal(HashTableATD<string>.PUT_OK, ht.get_put_status());
            Assert.Equal(2, ht.size());
        }

        [Fact]
        public void Put_TableFull_ReturnsError()
        {
            HashTable<int> ht = new HashTable<int>(3); 
            ht.put(1);
            ht.put(2);
            ht.put(3);
            
            ht.put(4);

            Assert.Equal(HashTableATD<int>.PUT_ERR, ht.get_put_status());
            Assert.Equal(3, ht.size()); 
        }

        [Fact]
        public void Put_SameValueTwice_DoesNotDuplicate()
        {
            HashTable<int> ht = new HashTable<int>(17);
            ht.put(42);
            ht.put(42); 

            Assert.Equal(1, ht.size());
            Assert.Equal(HashTableATD<int>.PUT_OK, ht.get_put_status());
        }
        
        [Fact]
        public void Remove_ExistingValue_ValidCase()
        {
            HashTable<int> ht = new HashTable<int>(17);
            ht.put(10);
            
            ht.remove(10);

            Assert.Equal(HashTableATD<int>.REMOVE_OK, ht.get_remove_status());
            Assert.Equal(0, ht.size());
            Assert.False(ht.get(10)); 
        }

        [Fact]
        public void Remove_NonExistingValue_ReturnsError()
        {
            HashTable<int> ht = new HashTable<int>(17);
            ht.put(10);

            ht.remove(9);

            Assert.Equal(HashTableATD<int>.REMOVE_ERR, ht.get_remove_status());
            Assert.Equal(1, ht.size());
        }
    }
}