using System;
using System.Collections.Generic;

namespace OOAP
{
    public class Queue<T> : ParentQueue<T> {}

    public class Deque<T> : ParentQueue<T>
    {
        // статусы
        public const int REMOVE_TAIL_OK = 0;
        public const int REMOVE_TAIL_ERR = 1;
        public const int REMOVE_TAIL_NIL = -1;

        public const int GET_TAIL_OK = 0;
        public const int GET_TAIL_ERR = 1;
        public const int GET_TAIL_NIL = -1;

        private int remove_tail_status;
        private int get_tail_status;

        // конструкторы
        public Deque() : base()
        {
            remove_tail_status = REMOVE_TAIL_NIL;
            get_tail_status = GET_TAIL_NIL;
        }

        // команды
        // постусловие: элемент с заданным значением добавлен в голову очереди
        public void addFront(T item)
        {
            list.AddFirst(item);
        }

        // предусловие: очередь не пуста
        // постусловие: элемент удален из хвоста очереди
        public void removeTail()
        {
            if (size() > 0)
            {
                list.RemoveLast();
                remove_tail_status = REMOVE_TAIL_OK;
            }
            else
            {
                remove_tail_status = REMOVE_TAIL_ERR;
            }
        }

        // дополнительные запросы
        // предусловие: очередь не пуста
        public T getTail()
        {
            if (size() > 0)
            {
                get_tail_status = GET_TAIL_OK;
                return list.Last.Value;
            }
            else
            {
                get_tail_status = GET_TAIL_ERR;
                return default(T);
            }
        }

        public override void clear()
        {
            base.clear();
            remove_tail_status = REMOVE_TAIL_NIL;
            get_tail_status = GET_TAIL_NIL;
        }

        public int get_remove_tail_status() => remove_tail_status;
        public int get_get_tail_status() => get_tail_status;

        // алиасы для соответствия названий методов из материала
        public void addTail(T item) => enqueue(item);
        public void removeFront() => dequeue();
        public T getFront() => get();
    }
}