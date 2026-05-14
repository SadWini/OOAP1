using System;
using System.Collections.Generic;

namespace OOAP
{
    public abstract class ParentQueueATD<T>
    {
        // статусы
        public const int DEQUEUE_OK = 0;
        public const int DEQUEUE_ERR = 1;
        public const int DEQUEUE_NIL = -1;

        public const int GET_OK = 0;
        public const int GET_ERR = 1; 
        public const int GET_NIL = -1;

        // конструкторы
        // постусловие: создана пустая очередь
        // public QueueATD();

        // команды
        // постусловие: элемент с заданным значением добавлен в хвост очереди
        public abstract void enqueue(T item);

        // предусловие: очередь не пуста
        // постусловие: элемент удален из головы очереди
        public abstract void dequeue();

        // постусловие: очередь очищена
        public abstract void clear();

        // запросы
        // предусловие: очередь не пуста
        public abstract T get();
        
        public abstract int size();

        // дополнительные запросы 
        public abstract int get_dequeue_status();
        public abstract int get_get_status();
    }

    public class ParentQueue<T> : ParentQueueATD<T>
    {
        protected LinkedList<T> list; // основное хранилище очереди
        private int dequeue_status;
        private int get_status;

        public Queue()
        {
            list = new LinkedList<T>();
            clear();
        }

        public override void enqueue(T item)
        {
            list.AddLast(item);
        }

        public override void dequeue()
        {
            if (size() > 0)
            {
                list.RemoveFirst();
                dequeue_status = DEQUEUE_OK;
            }
            else
            {
                dequeue_status = DEQUEUE_ERR;
            }
        }

        public override void clear()
        {
            list.Clear();
            dequeue_status = DEQUEUE_NIL;
            get_status = GET_NIL;
        }

        public override T get()
        {
            if (size() > 0)
            {
                get_status = GET_OK;
                return list.First.Value; 
            }
            else
            {
                get_status = GET_ERR;
                return default(T);
            }
        }

        public override int size()
        {
            return list.Count;
        }

        public override int get_dequeue_status()
        {
            return dequeue_status;
        }

        public override int get_get_status()
        {
            return get_status;
        }
    }
}