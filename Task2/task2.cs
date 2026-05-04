using System;

namespace OOAP
{
    public abstract class LinkedListATD<T>
    {
        // статусы
        public const int HEAD_OK = 0;
        public const int HEAD_ERR = 1;
        public const int HEAD_NIL = -1;
        public const int TAIL_OK = 0;
        public const int TAIL_ERR = 1;
        public const int TAIL_NIL = -1;
        public const int RIGHT_OK = 0;
        public const int RIGHT_ERR = 1;
        public const int RIGHT_NIL = -1;
        public const int PUT_RIGHT_OK = 0;
        public const int PUT_RIGHT_ERR = 1;
        public const int PUT_RIGHT_NIL = -1;
        public const int PUT_LEFT_OK = 0;
        public const int PUT_LEFT_ERR = 1;
        public const int PUT_LEFT_NIL = -1;
        public const int REPLACE_OK = 0;
        public const int REPLACE_ERR = 1;
        public const int REPLACE_NIL = -1;
        public const int REMOVE_OK = 0;
        public const int REMOVE_ERR = 1;
        public const int REMOVE_NIL = -1;
        public const int FIND_OK = 0;
        public const int FIND_ERR = 1;
        public const int FIND_NIL = -1;
        public const int GET_OK = 0;
        public const int GET_ERR = 1;
        public const int GET_NIL = -1;
        // конструктор
        // постусловие: создан новый пустой список
        // public abstract LinkedListATD();
        
        // команды
        // предусловие: список не пустой
        // постусловие: курсор установлен на первый узел
        public abstract void head();
        // предусловие: список не пустой
        // постусловие: курсор установлен на последний узел
        public abstract void tail();
        // предусловие: список не пустой и курсор не на последнем узле
        // постусловие: курсор установлен на 1 узел правее
        public abstract void right();
        // предусловие: список не пустой
        // постусловие: после текущего узла вставлен новый узел
        public abstract void put_right(T value);
        // предусловие: список не пустой
        // постусловие: перед текущим узлом вставлен новый узел
        public abstract void put_left(T value);
        // предусловие: список не пустой
        // постусловие: курсор смещается к правому соседу(если он есть), иначе смещается к левому соседу(если он есть)
        //              если удаляется единственный элемент в списке, то курсор становится невалидным
        public abstract void remove();
        // постусловие: из списка удаляются все значения
        public abstract void clear();
        // предусловие: список пустой
        // постусловие: в список добавлен новый узел
        public abstract void add_to_empty(T value);
        // постусловие: курсор установлен на следующий узел с искомым значением(относительно текущего узла) 
        //              или не перемещается в случае отсутствия таких узлов
        public abstract void find(T value);
        // постусловие: в списке удалены все узлы с текущим значением
        public abstract void remove_all(T value);
        // постусловие: в конце списка добавлен новый узел
        public abstract void add_tail(T value);
        // предусловие: список не пустой
        // постусловие: значение текущего узла заменено на заданное
        public abstract void replace(T value);

        // запросы 
        // предусловие: список не пустой
        public abstract T get();
        public abstract int size();
        public abstract bool is_head();
        public abstract bool is_tail();
        public abstract bool is_value();

        // дополнительные запросы
        public abstract int get_head_status();
        public abstract int get_tail_status();
        public abstract int get_right_status();
        public abstract int get_find_status();
        public abstract int get_put_right_status();
        public abstract int get_put_left_status();
        public abstract int get_remove_status();
        public abstract int get_replace_status();
        public abstract int get_get_status();
    }
}