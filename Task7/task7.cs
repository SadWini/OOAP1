using System;
using System.Collections.Generic;

namespace OOAP
{
    public abstract class HashTableATD<T>
    {
        // статусы
        public const int PUT_OK = 0;
        public const int PUT_ERR = 1; 
        public const int PUT_NIL = -1;

        public const int REMOVE_OK = 0;
        public const int REMOVE_ERR = 1; 
        public const int REMOVE_NIL = -1;

        // конструктор
        // предусловие: max_size > 0
        // постусловие: создана пустая хэш-таблица заданного максимального размера
        // public HashTableATD(int max_size);

        // команды
        // постусловие: если в таблице есть свободное место, то значение добавлено в таблицу. Если значение уже было, оно не дублируется.
        public abstract void put(T value);

        // предусловие: таблица не пуста
        // постусловие: если значение найдено, оно удалено из таблицы
        public abstract void remove(T value);

        // постусловие: из таблицы удалены все элементы
        public abstract void clear();

        // запросы 
        // постусловие: возвращает true, если значение есть в таблице, иначе false
        public abstract bool get(T value);

        public abstract int size();

        // дополнительные запросы 
        public abstract int get_put_status();
        public abstract int get_remove_status();
    }

    public class HashTable<T> : HashTableATD<T>
    {
        private enum SlotState { Empty = 0, Filled, Deleted } // состояние для разрешения коллизий
        private T[] slots; // основное хранилище хэш-таблицы
        private SlotState[] states; // хранилище состояний ячеек
        private int max_size; // максимальный размер
        private int count; // текущее количество элементов
        private int step; // Шаг пробирования 

        private int put_status;
        private int remove_status;

        public HashTable(int max_size)
        {
            if (max_size <= 0) max_size = 17;

            this.max_size = max_size;
            slots = new T[max_size];
            states = new SlotState[max_size];
            count = 0;
            
            step = 3; 
            while (GCD(step, this.max_size) != 1)
            {
                step++;
            }

            put_status = PUT_NIL;
            remove_status = REMOVE_NIL;
        }

        public override void put(T value)
        {
            int slot = seek_slot_for_put(value);
            if (slot != -1)
            {
                if (states[slot] != SlotState.Filled)
                {
                    count++;
                }
                slots[slot] = value;
                states[slot] = SlotState.Filled;
                put_status = PUT_OK;
            }
            else
            {
                put_status = PUT_ERR;
            }
        }

        public override void remove(T value)
        {
            if (size() > 0)
            {
                int slot = seek_slot_for_value(value);
                if (slot != -1)
                {
                    states[slot] = SlotState.Deleted; 
                    slots[slot] = default(T);
                    count--;
                    remove_status = REMOVE_OK;
                }
                else
                {
                    remove_status = REMOVE_ERR;
                }
            }
            else
            {
                remove_status = REMOVE_ERR;
            }
        }

        public override void clear()
        {
            slots = new T[max_size];
            states = new SlotState[max_size];
            count = 0;
            put_status = PUT_NIL;
            remove_status = REMOVE_NIL;
        }

        public override bool get(T value)
        {
            return seek_slot_for_value(value) != -1;
        }

        public override int size() => count;

        public override int get_put_status() => put_status;
        public override int get_remove_status() => remove_status;

        private int hash_fun(T value)
        {
            if (value == null) return 0;
            return Math.Abs(value.GetHashCode()) % max_size;
        }

        private int seek_slot_for_value(T value)
        {
            int index = hash_fun(value);
            
            for (int i = 0; i < max_size; i++)
            {
                if (states[index] == SlotState.Empty) 
                    return -1; 
                
                if (states[index] == SlotState.Filled && EqualityComparer<T>.Default.Equals(slots[index], value))
                    return index; 
                
                index = (index + step) % max_size; 
            }
            return -1;
        }

        private int seek_slot_for_put(T value)
        {
            int index = hash_fun(value);
            
            for (int i = 0; i < max_size; i++)
            {
                if (states[index] == SlotState.Empty || states[index] == SlotState.Deleted)
                    return index; 
                
                if (states[index] == SlotState.Filled && slots[index].Equals(value))
                    return index;
                
                index = (index + step) % max_size;
            }
            return -1; 
        }

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
    }
}