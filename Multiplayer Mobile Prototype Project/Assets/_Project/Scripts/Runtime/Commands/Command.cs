namespace Core.Runtime.Commands
{
    public abstract class Command<T> where T : class
    {
        public abstract bool Execute(T obj);
    }

    public abstract class Command<T1, T2> where T1 : class where T2 : class
    {
        public abstract bool Execute(T1 obj1, T2 obj2);
    }

    public abstract class Command<T1, T2, T3> where T1 : class where T2 : class where T3 : class
    {
        public abstract bool Execute(T1 obj1, T2 obj2, T3 obj3);
    }
    
    public abstract class Command<T1, T2, T3, T4> where T1 : class where T2 : class where T3 : class where T4 : class
    {
        public abstract bool Execute(T1 obj1, T2 obj2, T3 obj3, T4 obj4);
    }
    
    public abstract class Command<T1, T2, T3, T4, T5> where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class
    {
        public abstract bool Execute(T1 obj1, T2 obj2, T3 obj3, T4 obj4, T5 obj5);
    }
}