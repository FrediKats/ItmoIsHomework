namespace SubjectSolutionManager.Models
{
    public static class RepositoryProvider
    {
        public static ISubjectSolutionRepository GetRepository()
        {
            return new JsonSubjectSolutionRepository();
            //return new MemoryRepository();
        }
    }
}