using System;

namespace OOAP
{
    public class ParentList<T> : LinkedListATD<T>
    {
        protected LinkedList<T> list; // основное хранилище списка
        protected LinkedListNode<T>? cursor; // курсор
        private int head_status; // статус запроса head()
        private int tail_status; // статус запроса tail()
        private int right_status; // статус запроса right()
        private int find_status; // статус запроса find()
        private int put_right_status; // статус запроса right()
        private int put_left_status; // статус запроса left()
        private int remove_status; // статус запроса remove()
        private int replace_status; // статус запроса replace()
        private int get_status; // статус запроса get()
        private int add_to_empty_status; // статус запроса add_to_empty()

        // Конструктор
        public ParentList() // конструктор
        {
            list = new LinkedList<T>();
            head_status = HEAD_NIL;
            tail_status = TAIL_NIL;
            right_status = RIGHT_NIL;
            find_status = FIND_NIL;
            put_right_status = PUT_RIGHT_NIL;
            put_left_status = PUT_LEFT_NIL;
            remove_status = REMOVE_NIL;
            replace_status = REPLACE_NIL;
            get_status = GET_NIL;
            add_to_empty_status = ADD_TO_EMPTY_NIL;
        }

        // команды
        public override void head()
        {
            if (size() > 0)
            {
                head_status = HEAD_OK;
                cursor = list.First;
            }
            else
            {
                head_status = HEAD_ERR;
            }
        }

        public override void tail()
        {
            if (size() > 0)
            {
                tail_status = TAIL_OK;
                cursor = list.Last;
            }
            else
            {
                tail_status = TAIL_ERR;
            }
        }

        public override void right()
        {
            if (size() > 0 && cursor != list.Last)
            {
                right_status = RIGHT_OK;
                cursor = cursor.Next;
            }
            else
            {
                right_status = RIGHT_ERR_NOT_RIGHTER_ELEMENT;
            }
        }

        public override void put_right(T value)
        {
            if (size() > 0)
            {
                list.AddAfter(cursor, value);
                put_right_status = PUT_RIGHT_OK;
            }
            else
            {
                put_right_status = PUT_RIGHT_ERR;
            }
        }

        public override void put_left(T value)
        {
            if (size() > 0)
            {
                list.AddBefore(cursor, value);
                put_left_status = PUT_LEFT_OK;
            }
            else
            {
                put_left_status = PUT_LEFT_ERR;
            }
        }

        public override void remove()
        {
            if (size() > 0)
            {
                remove_status = REMOVE_OK;
                var temp = cursor.Next != null ? cursor.Next : cursor.Previous;
                list.Remove(cursor);
                cursor = temp;
            }
            else
            {
                remove_status = REMOVE_ERR;
            }
        }

        public override void clear()
        {
            list.Clear();
            head_status = HEAD_NIL;
            tail_status = TAIL_NIL;
            right_status = RIGHT_NIL;
            find_status = FIND_NIL;
            put_right_status = PUT_RIGHT_NIL;
            put_left_status = PUT_LEFT_NIL;
            remove_status = REMOVE_NIL;
            replace_status = REPLACE_NIL;
            get_status = GET_NIL;
            add_to_empty_status = ADD_TO_EMPTY_NIL;
        }

        public override void add_to_empty(T value)
        {
            if (size() > 0)
            {
                add_to_empty_status = ADD_TO_EMPTY_ERR;
            }
            else
            {
                add_to_empty_status = ADD_TO_EMPTY_OK;
                list.AddFirst(value);
                cursor = list.First;
            }
        }

        public override void find(T value)
        {
            if (size() == 0)
            {
                find_status = FIND_ERR_NOT_FOUND;
                return;
            }
            var temp = cursor.Next;
            while (temp != null)
            {
                if (temp.Value != null && temp.Value.Equals(value))
                {
                    cursor = temp;
                    find_status = FIND_OK;
                    return;
                }
                temp = temp.Next;
            }
            find_status = FIND_ERR_NOT_FOUND;
        }

        public override void remove_all(T value)
        {
            var current = list.First;
            while (current != null)
            {
                var next = current.Next; 
                if (current.Value != null && current.Value.Equals(value))
                {
                    if (current == cursor)
                    {
                        cursor = cursor.Next ?? cursor.Previous;
                    }
                    list.Remove(current);
                }
                current = next;
            }
            if (size() == 0) cursor = null;
        }

        public override void add_tail(T value)
        {
            if (size() > 0)
            {
                list.AddLast(value);
            }
            else
            {
                list.AddLast(value);
                cursor = list.Last;
            }
        }

        public override void replace(T value)
        {
            if (size() > 0)
            {
                cursor.Value = value;
                replace_status = REPLACE_OK;
            }
            else
            {
                replace_status = REPLACE_ERR;
            }
        }
        // Запросы 
        public override T get()
        {
            if (size() > 0)
            {
                get_status = GET_OK;
                return cursor.Value;
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

        public override bool is_head()
        {
            return size() > 0 && cursor == list.First;
        }

        public override bool is_tail()
        {
            return size() > 0 && cursor == list.Last;
        }

        public override bool is_value()
        {
            return size() > 0;
        }

        // Дополнительные запросы
        public override int get_head_status()
        {
            return head_status;
        }

        public override int get_tail_status()
        {
            return tail_status;
        }

        public override int get_right_status()
        {
            return right_status;
        }

        public override int get_find_status()
        {
            return find_status;
        }

        public override int get_put_right_status()
        {
            return put_right_status;
        }

        public override int get_put_left_status()
        {
            return put_left_status;
        }

        public override int get_remove_status()
        {
            return remove_status;
        }

        public override int get_replace_status()
        {
            return replace_status;
        }

        public override int get_get_status()
        {
            return get_status;
        }

        public override int get_add_to_empty_status()
        {
            return add_to_empty_status;
        }
    }

    public class OneWayLinkedList<T> : ParentList<T> {}

    public class TwoWayList<T> : ParentList<T>
    {
        public const int LEFT_OK = 0;
        public const int LEFT_ERR_NOT_LEFTER = 1;
        public const int LEFT_NIL = -1;
        private int left_status; // статус последнего запроса left()

        public TwoWayList() : base()
        {
            left_status = LEFT_NIL;
        }

        // предусловие: список не пустой и курсор не на первом узле
        // постусловие: курсор установлен на 1 узел левее
        public void left()
        {
            if (size() > 0 && cursor != list.First)
            {
                left_status = LEFT_OK;
                cursor = cursor.Previous;
            }
            else
            {
                left_status = LEFT_ERR_NOT_LEFTER;
            }
        }

        // дополнительные запросы
        public int get_left_status()
        {
            return left_status;
        }
    }
}