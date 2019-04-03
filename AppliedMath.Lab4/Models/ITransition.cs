namespace AppliedMath.Lab4.Models
{
    public interface ITransition
    {
        string GetTransitionName();
        bool Invoke(SystemState state);
    }
}