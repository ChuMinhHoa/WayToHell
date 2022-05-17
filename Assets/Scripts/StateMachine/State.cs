public abstract class State<T>{
    public abstract void Enter(T go);
    public abstract void Execute(T go);
    public abstract void Exit(T go);
}
