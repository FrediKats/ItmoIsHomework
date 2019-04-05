namespace AppliedMath.Lab4.Models
{
    public class ProductCreator : ITransition
    {
        public string GetTransitionName()
        {
            return "Product creating";
        }

        public bool IsActive(SystemState state)
        {
            return (state.K >= 1 && state.M >= 2 && state.N >= 3 && state.S < 2);
        }

        public bool Invoke(SystemState state)
        {
            if (IsActive(state))
            {
                state.K -= 1;
                state.M -= 2;
                state.N -= 3;
                state.S += 1;
                return true;
            }

            return false;
        }
    }
}