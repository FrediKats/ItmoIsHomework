namespace AppliedMath.Lab4.Models
{
    public interface ITransition
    {
        string GetTransitionName();
        bool IsActive(SystemState state);
        bool Invoke(SystemState state);
    }
}