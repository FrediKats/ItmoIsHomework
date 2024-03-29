﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace SubjectSolutionManager.Models
{
    public class JsonSubjectSolutionRepository : ISubjectSolutionRepository
    {
        private const string RepositoryPath = "SubjectData.json";

        public void Create(SubjectSolutionModel solution)
        {
            List<SubjectSolutionModel> list = Read();
            list.Add(solution);
            SaveToFile(list);
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
            List<SubjectSolutionModel> solutions = Read();
            SubjectSolutionModel newSolution = solutions.First(s => s.Id == solution.Id);
            newSolution.Id = solution.Id;
            newSolution.Description = solution.Description;
            newSolution.Title = solution.Title;
            newSolution.Path = solution.Path;
            SaveToFile(solutions);
            return newSolution;
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