using System;

namespace SubjectSolutionManager.Models
{
    public class SubjectSolutionModel
    {
        public SubjectSolutionModel(string title, string path, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Path = path;
            Description = description;
        }

        public Guid Id { get; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
    }
}