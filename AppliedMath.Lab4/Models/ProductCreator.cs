namespace AppliedMath.Lab4.Models
{
    public class ProductCreator : ITransition
    {
        public string GetTransitionName()
        {
            return "Product creating";
        }

        public bool Invoke(SystemState state)
        {
            if (state.K >= 1 && state.M >= 2 && state.N >= 3 && state.S < 2)
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