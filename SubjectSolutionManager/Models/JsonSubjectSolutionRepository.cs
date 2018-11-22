using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace SubjectSolutionManager.Models
{
    public class JsonSubjectSolutionRepository : ISubjectSolutionRepository
    {
        private const string RepositoryPath = "SubjectData.json";
        public SubjectSolutionModel Create(SubjectSolutionModel solution)
        {
            List<SubjectSolutionModel> list = Read();
            list.Add(solution);
            SaveToFile(list);
            return solution;
        }

        public List<SubjectSolutionModel> Read()
        {
            var dataList = new List<SubjectSolutionModel>();
            if (File.Exists(RepositoryPath))
            {
                string jsonData = File.ReadAllText(RepositoryPath);
                dataList = JsonConvert.DeserializeObject<List<SubjectSolutionModel>>(jsonData);
            }

            return dataList;
        }

        public SubjectSolutionModel Update(SubjectSolutionModel solution)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            List<SubjectSolutionModel> list = Read();
            SaveToFile(list.Where(e => e.Id != id));
        }

        private static void SaveToFile(IEnumerable<SubjectSolutionModel> solutions)
        {
            File.WriteAllText(RepositoryPath, JsonConvert.SerializeObject(solutions));
        }
    }
}