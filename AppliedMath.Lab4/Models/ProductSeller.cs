namespace AppliedMath.Lab4.Models
{
    public class ProductSeller : ITransition
    {
        public string GetTransitionName()
        {
            return "Product sold";
        }

        public bool IsActive(SystemState state)
        {
            return (state.S > 0);
        }

        public bool Invoke(SystemState state)
        {
            if (IsActive(state))
            {
                state.S--;
                return true;
            }

            return false;
        }
    }
}