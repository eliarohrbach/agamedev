namespace Enemy.ai
{
    /// <summary>
    /// Author: Alexander Wyss
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Called once when it switches to the current state.
        /// </summary>
        void Start();

        /// <summary>
        /// Called every Frame for as long the state is active.
        /// </summary>
        /// <returns>The new state, or itself</returns>
        IState Update();
    }
}