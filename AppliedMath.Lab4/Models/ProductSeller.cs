namespace AppliedMath.Lab4.Models
{
    public class ProductSeller : ITransition
    {
        public string GetTransitionName()
        {
            return "Product sold";
        }

        public bool Invoke(SystemState state)
        {
            if (state.S > 0)
            {
                state.S--;
                return true;
            }

            return false;
        }
    }
}