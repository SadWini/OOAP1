using System;
using System.Collections.Generic;

namespace OOAP
{
    public abstract class NativeDictionaryATD<T>
    {
        // статусы
        public const int PUT_OK = 0;
        public const int PUT_ERR = 1;
        public const int PUT_NIL = -1;
        public const int REMOVE_OK = 0;
        public const int REMOVE_ERR = 1;
        public const int REMOVE_NIL = -1; 
        public const int GET_OK = 0;
        public const int GET_ERR = 1; 
        public const int GET_NIL = -1;
        public const int REPLACE_OK = 0;
        public const int REPLACE_ERR = 1;
        public const int REPLACE_NIL = -1;

        // конструкторы
        // постусловие: создан пустой словарь максимального размера max_size
        // public NativeDictionaryATD(int max_size);

        // команды 

        // предусловие: словарь не полон
        // постусловие: значение value связано с ключом key.
        public abstract void put(string key, T value);

        // предусловие: в словаре есть ключ с заданным значением
        // постусловие: ключ и связанное с ним значение удалены
        public abstract void remove(string key);

        // предусловие: в словаре есть ключ с заданным значением
        // постусловие: значение value, связанное с этим ключом, изменено на заданное
        public abstract void replace(string key, T value);


        // запросы
        public abstract bool is_key(string key);

        // предусловие: ключ есть в словаре 
        public abstract T get(string key);

        public abstract int size();

        // дополнительные запросы
        public abstract int get_put_status();
        public abstract int get_remove_status();
        public abstract int get_get_status();
        public abstract int get_replace_status();
    }

    public class NativeDictionary<T> : NativeDictionaryATD<T>
    {
        private enum SlotState { Empty = 0, Filled, Deleted }
        
        private string[] slots; // хранилище ключей
        private T[] values;      // хранилище значений
        private SlotState[] states; // хранилище состояний
        private int max_size;
        private int count;
        private int step;

        private int put_status;
        private int remove_status;
        private int get_status;
        private int replace_status;

        public NativeDictionary(int max_size)
        {
            if (max_size <= 0) max_size = 17;
            this.max_size = max_size;

            slots = new string[max_size];
            values = new T[max_size];
            states = new SlotState[max_size];
            put_status = PUT_NIL; 
            remove_status = REMOVE_NIL;
            get_status = GET_NIL;
            replace_status = REPLACE_NIL;
            count = 0;
            
            step = 3;
            while (GCD(step, this.max_size) != 1) { step++; }
        }

        public override void put(string key, T value)
        {
            int slot = seek_slot_for_put(key);
            if (slot != -1)
            {
                if (states[slot] != SlotState.Filled) { count++; }
                
                slots[slot] = key;
                values[slot] = value; 
                states[slot] = SlotState.Filled;
                put_status = PUT_OK;
            }
            else
            {
                put_status = PUT_ERR;
            }
        }

        public override void replace(string key, T value)
        {
            int slot = seek_slot_for_key(key);
            
            if (slot != -1) 
            {
                values[slot] = value; 
                replace_status = REPLACE_OK;
            }
            else 
            {
                replace_status = REPLACE_ERR;
            }
        }
        
        public override void remove(string key)
        {
            int slot = seek_slot_for_key(key);
            if (slot != -1)
            {
                states[slot] = SlotState.Deleted;
                slots[slot] = null;
                values[slot] = default(T); 
                count--;
                remove_status = REMOVE_OK;
            }
            else
            {
                remove_status = REMOVE_ERR;
            }
        }
        
        public override bool is_key(string key)
        {
            return seek_slot_for_key(key) != -1;
        }

        public override T get(string key)
        {
            int slot = seek_slot_for_key(key);
            if (slot != -1)
            {
                get_status = GET_OK;
                return values[slot];
            }
            else
            {
                get_status = GET_ERR;
                return default(T);
            }
        }

        public override int size() => count;
        public override int get_put_status() => put_status;
        public override int get_remove_status() => remove_status;
        public override int get_get_status() => get_status;
        public override int get_replace_status() => replace_status;

        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return Math.Abs(a);
        }

        private int hash_fun(string value)
        {
            if (value == null) return 0;
            return Math.Abs(value.GetHashCode()) % max_size;
        }

        private int seek_slot_for_key(string key)
        {
            int index = hash_fun(key);
            for (int i = 0; i < max_size; i++)
            {
                if (states[index] == SlotState.Empty) return -1;
                if (states[index] == SlotState.Filled && slots[index] == key) return index;
                index = (index + step) % max_size;
            }
            return -1;
        }

        private int seek_slot_for_put(string key)
        {
            int index = hash_fun(key);
            for (int i = 0; i < max_size; i++)
            {
                if (states[index] == SlotState.Empty || states[index] == SlotState.Deleted) return index;
                if (states[index] == SlotState.Filled && slots[index] == key) return index;
                index = (index + step) % max_size;
            }
            return -1;
        }
    }
}