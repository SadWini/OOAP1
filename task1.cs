using System;

namespace OOAP
{
    public abstract class BoundedStackATD<T>
    {
        public const int POP_NIL = -1; // pop() ещё не вызывалась
        public const int POP_OK = 0; // последняя pop() отработала нормально
        public const int POP_ERR = 1; // стек пуст

        public const int PEEK_NIL = -1; // peek() ещё не вызывалась
        public const int PEEK_OK = 0; // последняя peek() вернула корректное значение 
        public const int PEEK_ERR = 1; // стек пуст
        public const int PUSH_NIL = -1; // push() еще не вызывалась
        public const int PUSH_OK = 0; // последняя push() сработала корректно
        public const int PUSH_ERR = 1; // стек полон

        // конструкторы
        // постусловие: создан новый пустой стек максимальной емкости cap
        //public abstract BoundedStackATD(int cap); 

        // команды 
        // предусловие: стек не полон
        // постусловие: в стек добавлено новое значение
        public abstract void push(T value);

        // предусловие: стек не пустой
        // постусловие: из стека удален верхний элемент
        public abstract void pop();

        // постусловие: из стека удалены все элементы
        public abstract void clear();

        // запросы 
        // предусловие: стек не пустой
        public abstract T peek();

        public abstract int size();
        public abstract int capacity();

        // дополнительные запросы
        public abstract int get_pop_status(); // возвращает значение POP_*
        public abstract int get_push_status(); // возвращает значение PUSH_*
        public abstract int get_peek_status(); // возвращает значение PEEK_*

    }

    public class BoundedStack<T> : BoundedStackATD<T>
    {
        // скрытые поля
        private List<T> stack; // основное хранилище стека
        private int cap; // предельная емкость хранилища
        private int peek_status; // статус запроса peek()
        private int pop_status; // статус запроса pop()
        private int push_status;// статус запроса push()

        public BoundedStack(int cap = 32) // конструктор
        {
            if (cap > 0)
            {
                this.cap = cap;
                stack = new List<T>(cap);
                clear();
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(cap), "Емкость стека должна быть целым положительным числом.");
            }
        }

        public override void push(T value)
        {
            if (size() < cap)
            {
                stack.Add(value);
                push_status = PUSH_OK;
            }
            else
            {
                push_status = PUSH_ERR;
            }
        }

        public override void pop()
        {
            if (size() > 0)
            {
                stack.RemoveAt(size() - 1);
                pop_status = POP_OK;
            }
            else
            {
                pop_status = POP_ERR;
            }
        }

        public override void clear()
        {
            stack.Clear();
            peek_status = PEEK_NIL;
            pop_status = POP_NIL;
            push_status = PUSH_NIL;
        }

        public override T peek()
        {
            T result = default;
            if (size() > 0)
            {
                result = stack[^1];
                peek_status = PEEK_OK;
            }
            else
            {
                peek_status = PEEK_ERR;
            }
            return result;
        }

        // запросы
        public override int size()
        {
            return stack.Count;
        }

        public override int capacity()
        {
            return cap;
        }

        //запросы статуса
        public override int get_pop_status()
        {
            return pop_status;
        }

        public override int get_peek_status()
        {
            return peek_status;
        }

        public override int get_push_status()
        {
            return push_status;
        }
    }
}
