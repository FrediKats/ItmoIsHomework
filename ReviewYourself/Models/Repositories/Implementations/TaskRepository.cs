using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DbExtensions;
using ReviewYourself.Models.Tools;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        private string _connectionString;

        public static TaskRepository Create(string connectionString)
        {
            return new TaskRepository()
            {
                _connectionString =  connectionString
            };
        }

        public TaskRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AzureConnect"].ConnectionString;
        }

        public void Create(ResourceTask task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SQL.INSERT_INTO("ResourceTask (TaskID, CourseID, Title, TaskDescription, Posted)")
                    .VALUES(Guid.NewGuid(), task.CourseId, task.Title, task.Description, DateTime.UtcNow)
                    .ToCommand(connection)
                    .ExecuteNonQuery();

                var insertCriteria = new SqlBuilder();

                foreach (var criteria in task.CriteriaCollection)
                {
                    insertCriteria = insertCriteria
                        .INSERT_INTO("Criteria (CriteriaID, TaskID, Title, CriteriaDescription, MaxPoint)")
                        .VALUES(Guid.NewGuid(), criteria.TaskId, criteria.Title, criteria.Description, criteria.MaxPoint)
                        .Append(";");
                    //insertCriteria = insertCriteria.VALUES(Guid.NewGuid(), criteria.TaskId, criteria.Title, criteria.Description, criteria.MaxPoint);
                }

                insertCriteria
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public ResourceTask Read(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = SQL
                    .SELECT("*")
                    .FROM("ResourceTask")
                    .WHERE("TaskID = {0}", id)
                    .ToCommand(connection);

                ResourceTask task;

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    task = ReaderConvertor.ToTask(reader);
                    //new ResourceTask
                    //{
                    //    Id = Guid.Parse(reader["TaskID"].ToString()),
                    //    CourseId = Guid.Parse(reader["CourseID"].ToString()),
                    //    CriteriaCollection = new List<Criteria>(),
                    //    Title = reader["Title"].ToString(),
                    //    Description = reader["TaskDescription"].ToString(),
                    //    PostTime = DateTime.Parse(reader["Posted"].ToString())
                    //};
                }

                command = SQL
                    .SELECT("*")
                    .FROM("Criteria")
                    .WHERE("TaskID = {0}", id)
                    .ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        task.CriteriaCollection.Add(ReaderConvertor.ToCriteria(reader));
                        //new Criteria
                        //{
                        //    Id = Guid.Parse(reader["CriteriaID"].ToString()),
                        //    TaskId = Guid.Parse(reader["TaskID"].ToString()),
                        //    Title = reader["Title"].ToString(),
                        //    Description = reader["CriteriaDescription"].ToString(),
                        //    MaxPoint = int.Parse(reader["MaxPoint"].ToString())
                        //});
                    }
                }

                return task;
            }
        }

        public ICollection<ResourceTask> ReadByCourse(Guid courseId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = SQL
                    .SELECT("*")
                    .FROM("ResourceTask")
                    .WHERE("CourseID = {0}", courseId)
                    .ToCommand(connection);

                ICollection<ResourceTask> taskList = new List<ResourceTask>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        taskList.Add(ReaderConvertor.ToTask(reader));
                        //new ResourceTask
                        //{
                        //    Id = Guid.Parse(reader["TaskID"].ToString()),
                        //    CourseId = Guid.Parse(reader["CourseID"].ToString()),
                        //    Title = reader["Title"].ToString(),
                        //    Description = reader["TaskDescription"].ToString(),
                        //    PostTime = DateTime.Parse(reader["Posted"].ToString())
                        //});
                    }
                }

                return taskList;
            }
        }

        public void Delete(Guid taskId)
        {
            throw new NotImplementedException();
        }
    }
}