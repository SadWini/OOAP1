using System;
using Xunit;

namespace OOAP
{
    public class NativeDictionaryTests
    {
        [Fact]
        public void PutAndGet_NewKey_ValidCase()
        {
            NativeDictionary<int> dict = new NativeDictionary<int>(17);
            
            dict.put("apple", 100);
            
            Assert.Equal(NativeDictionaryATD<int>.PUT_OK, dict.get_put_status());
            Assert.True(dict.is_key("apple"));
            Assert.Equal(100, dict.get("apple"));
            Assert.Equal(NativeDictionaryATD<int>.GET_OK, dict.get_get_status());
        }

        [Fact]
        public void Replace_ExistingKey_ValidCase()
        {
            NativeDictionary<int> dict = new NativeDictionary<int>(17);
            dict.put("apple", 100);
            
            dict.replace("apple", 200);

            Assert.Equal(1, dict.size()); 
            Assert.Equal(200, dict.get("apple"));
        }

        [Fact]
        public void Replace_NonExistingKey_ReturnsError()
        {
            NativeDictionary<int> dict = new NativeDictionary<int>(17);
            dict.put("apple", 100);
            
            dict.replace("orange", 200);
            Assert.Equal(NativeDictionaryATD<int>.REPLACE_ERR, dict.get_replace_status());
        }

        [Fact]
        public void Get_ExistingKey_ValidCase()
        {
            NativeDictionary<int> dict = new NativeDictionary<int>(17);
            dict.put("orange", 100);

            int val = dict.get("orange");

            Assert.Equal(NativeDictionaryATD<int>.GET_OK, dict.get_get_status());
            Assert.Equal(100, val);
        }

        [Fact]
        public void Get_NonExistingKey_ReturnsError()
        {
            NativeDictionary<int> dict = new NativeDictionary<int>(17);

            int val = dict.get("orange");

            Assert.Equal(NativeDictionaryATD<int>.GET_ERR, dict.get_get_status());
        }

        [Fact]
        public void Remove_ExistingKey_DeletesPair()
        {
            NativeDictionary<int> dict = new NativeDictionary<int>(17);
            dict.put("apple", 100);

            dict.remove("apple");
            
            Assert.Equal(NativeDictionaryATD<int>.REMOVE_OK, dict.get_remove_status());
            Assert.Equal(0, dict.size());
            Assert.False(dict.is_key("apple"));
        }

        [Fact]
        public void Remove_NonExistingKey_ReturnsError()
        {
            NativeDictionary<int> dict = new NativeDictionary<int>(17);
            dict.put("apple", 100);
            
            dict.remove("orange");

            Assert.Equal(NativeDictionaryATD<int>.REMOVE_ERR, dict.get_remove_status());
            Assert.Equal(1, dict.size());
        }
    }
}