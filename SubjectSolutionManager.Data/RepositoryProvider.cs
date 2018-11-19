namespace SubjectSolutionManager.Data
{
    public static class RepositoryProvider
    {
        public static ISubjectSolutionRepository GetRepository()
        {
            return new MemoryRepository();
        }
    }
}