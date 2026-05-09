using System;

namespace OOAP
{
    public abstract class DynArrayATD<T>
    {
        // статусы
        public const int INSERT_OK = 0;
        public const int INSERT_ERR= 1; 
        public const int INSERT_NIL = -1;

        public const int REMOVE_OK = 0;
        public const int REMOVE_ERR = 1; 
        public const int REMOVE_NIL = -1;

        public const int REPLACE_OK = 0;
        public const int REPLACE_ERR = 1;
        public const int REPLACE_NIL = -1;

        public const int GET_OK = 0;
        public const int GET_ERR = 1;
        public const int GET_NIL = -1;

        // конструктор
        // постусловие: создан пустой массив, size = 0, capacity = initial_capacity
        // public DynArrayATD(int initial_capacity = 16);

        // команды 
        // постусловие: элемент добавлен в конец массива, если перед добавлением size == capacity, то capacity будет увеличен
        public abstract void append(T value);

        // предусловие: index >= 0 и index <= size()
        // постусловие: элемент вставлен на позицию index, элементы начиная с index сдвинуты вправо, если перед вставкой
        // size == capacity, то capacity будет увеличен
        public abstract void insert(int index, T value);

        // предусловие: index >= 0 и index < size()
        // постусловие: элемент по индексу index удален, элементы правее сдвинуты влево,
        // capacity может быть уменьшен для экономии памяти, но не может стать меньше 16
        public abstract void remove(int index);

        // предусловие: index >= 0 и index < size()
        // постусловие: значение элемента по индексу index заменено на value
        public abstract void replace(int index, T value);

        // постусловие: массив очищен, size = 0, capacity = 16
        public abstract void clear();

        // запросы
        // предусловие: index >= 0 и index < size()
        public abstract T get(int index);

        public abstract int size(); // текущее количество элементов
        public abstract int capacity(); // текущая емкость буфера
        
        // дополнительные запросы
        public abstract int get_insert_status();
        public abstract int get_remove_status();
        public abstract int get_replace_status();
        public abstract int get_get_status();
    }

    public class DynArray<T> : DynArrayATD<T>
    {
        private T[] array; // основное хранилище памяти
        private int count; // текущий размер
        private int cap;   // емкость

        private int insert_status;
        private int remove_status;
        private int replace_status;
        private int get_status;

        public DynArray(int initial_capacity = 16)
        {
            if (initial_capacity < 16) 
            {
                initial_capacity = 16;
            }
            cap = initial_capacity;
            count = 0;
            array = new T[cap];
            
            insert_status = INSERT_NIL;
            remove_status = REMOVE_NIL;
            replace_status = REPLACE_NIL;
            get_status = GET_NIL;
        }

        private void make_array(int new_capacity)
        {
            if (new_capacity < 16) 
            {
                new_capacity = 16;
            }
            
            T[] new_array = new T[new_capacity];
            for (int i = 0; i < count; i++)
            {
                new_array[i] = array[i];
            }
            array = new_array;
            cap = new_capacity;
        }

        public override void append(T value)
        {
            if (count == cap)
            {
                make_array(cap * 2);
            }
            array[count] = value;
            count++;
        }

        public override void insert(int index, T value)
        {
            if (index >= 0 && index <= count)
            {
                if (count == cap)
                {
                    make_array(cap * 2);
                }
                
                for (int i = count; i > index; i--)
                {
                    array[i] = array[i - 1];
                }
                
                array[index] = value;
                count++;
                insert_status = INSERT_OK;
            }
            else
            {
                insert_status = INSERT_ERR;
            }
        }

        public override void remove(int index)
        {
            if (index >= 0 && index < count)
            {
                for (int i = index; i < count - 1; i++)
                {
                    array[i] = array[i + 1];
                }
                count--;
                array[count] = default(T);

                if (count < cap / 2.0)
                {
                    int new_capacity = (int)(cap / 1.5);
                    if (new_capacity < 16) 
                    {
                        new_capacity = 16;
                    }
                    
                    if (new_capacity < cap) 
                    {
                        make_array(new_capacity);
                    }
                }
                remove_status = REMOVE_OK;
            }
            else
            {
                remove_status = REMOVE_ERR;
            }
        }

        public override void replace(int index, T value)
        {
            if (index >= 0 && index < count)
            {
                array[index] = value;
                replace_status = REPLACE_OK;
            }
            else
            {
                replace_status = REPLACE_ERR;
            }
        }

        public override void clear()
        {
            cap = 16;
            count = 0;
            array = new T[cap];
            
            insert_status = INSERT_NIL;
            remove_status = REMOVE_NIL;
            replace_status = REPLACE_NIL;
            get_status = GET_NIL;
        }

        public override T get(int index)
        {
            if (index >= 0 && index < count)
            {
                get_status = GET_OK;
                return array[index];
            }
            else
            {
                get_status = GET_ERR;
                return default(T);
            }
        }

        public override int size()
        {
            return count;
        }
        public override int capacity() {
            return cap;
        }

        public override int get_insert_status() {
            return insert_status;
        }
        public override int get_remove_status()
        {
            return remove_status;
        }
        public override int get_replace_status() {
            return replace_status;
        }
        public override int get_get_status()
        {
            return get_status;
        } 
    }
}