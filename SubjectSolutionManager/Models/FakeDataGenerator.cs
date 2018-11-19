namespace SubjectSolutionManager.Models
{
    public static class FakeDataGenerator
    {
        public static void AddFakeSolution(ISubjectSolutionRepository repository)
        {
            repository.Create(new SubjectSolutionModel("Instance", "none", "desc"));
            repository.Create(new SubjectSolutionModel("Instance 2", "none", "desc"));
            repository.Create(new SubjectSolutionModel("Instance 3", "none", "desc"));
        }
    }
}