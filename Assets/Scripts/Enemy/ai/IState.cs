namespace Enemy.ai
{
    public interface IState
    {
        void Start();
        
        IState Update();
    }
}