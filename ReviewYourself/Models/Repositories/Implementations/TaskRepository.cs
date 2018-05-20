using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DbExtensions;

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
            _connectionString = ConfigurationManager.ConnectionStrings["SSConnection"].ConnectionString;
        }

        public void Create(ResourceTask task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var insertTask = SQL
                    .INSERT_INTO("ResourceTask (TaskID, CourseID, Title, TaskDescription, Posted)")
                    .VALUES(Guid.NewGuid(), task.CourseId, task.Title, task.Description, task.PostTime)
                    .ToCommand(connection)
                    .ExecuteNonQuery();

                var insertCriteria = SQL.INSERT_INTO("Criteria (CriteriaID, TaskID, Title, CriteriaDescription, MaxPoint)");
                if (task.CriteriaCollection == null)
                    return;


                foreach (var criteria in task.CriteriaCollection)
                {
                    insertCriteria = insertCriteria.VALUES(Guid.NewGuid(), criteria.TaskId, criteria.Title, criteria.Description, criteria.MaxPoint);
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

                var reader = SQL
                    .SELECT("*")
                    .FROM("ResourceTask")
                    .WHERE("TaskID = {0}", id)
                    .ToCommand(connection)
                    .ExecuteReader();

                reader.Read();
                
                var task = new ResourceTask
                {
                    Id = Guid.Parse(reader["TaskID"].ToString()),
                    CourseId = Guid.Parse(reader["CourseID"].ToString()),
                    CriteriaCollection = new List<Criteria>(),
                    Title = reader["Title"].ToString(),
                    Description = reader["TaskDescription"].ToString(),
                    PostTime = DateTime.Parse(reader["Posted"].ToString())
                };

                reader = SQL
                    .SELECT("*")
                    .FROM("Criteria")
                    .WHERE("TaskID = {0}", id)
                    .ToCommand(connection)
                    .ExecuteReader();

                while (reader.Read())
                {
                    task.CriteriaCollection.Add(new Criteria
                    {
                        Id = Guid.Parse(reader["CriteriaID"].ToString()),
                        TaskId = Guid.Parse(reader["TaskID"].ToString()),
                        Title = reader["Title"].ToString(),
                        Description = reader["CriteriaDescription"].ToString(),
                        MaxPoint = int.Parse(reader["MaxPoint"].ToString())
                    });

                }

                return task;
            }
        }

        public ICollection<ResourceTask> ReadByCourse(Guid courseId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("*")
                    .FROM("ResourceTask")
                    .WHERE("CourseID = {0}", courseId)
                    .ToCommand(connection)
                    .ExecuteReader();

                ICollection<ResourceTask> taskList = new List<ResourceTask>();

                while (reader.Read())
                {
                    taskList.Add(new ResourceTask
                    {
                        Id = Guid.Parse(reader["TaskID"].ToString()),
                        CourseId = Guid.Parse(reader["CourseID"].ToString()),
                        Title = reader["Title"].ToString(),
                        Description = reader["TaskDescription"].ToString(),
                        PostTime = DateTime.Parse(reader["Posted"].ToString())
                    });
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