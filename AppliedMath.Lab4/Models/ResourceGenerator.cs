namespace AppliedMath.Lab4.Models
{
    public class ResourceGenerator : ITransition
    {
        private int _state = 0;

        public string GetTransitionName()
        {
            switch (_state)
            {
                case 0:
                    return "Added K";
                case 1:
                    return "Added M";
                case 2:
                    return "Added N";
            }
            return "ResourceCreate";
        }

        public bool Invoke(SystemState state)
        {
            switch (_state)
            {
                case 0:
                    state.K++;
                    break;
                case 1:
                    state.M++;
                    break;
                case 2:
                    state.N++;
                    break;
            }

            _state = (_state + 1) % 3;
            return true;
        }
    }
}