using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DbExtensions;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class SolutionRepository : ISolutionRepository
    {
        private readonly string _connectionString;

        public SolutionRepository(/*string connectionString*/)
        {
            //_connectionString = connectionString;
            _connectionString = ConfigurationManager.ConnectionStrings["SSConnection"].ConnectionString;
        }

        public void Create(Solution solution)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var insertSolution = SQL
                    .INSERT_INTO("Solution (SolutionID, AuthorID, TaskID, TextData, Posted, Resolved)")
                    .VALUES(solution.Id, solution.AuthorId, solution.TaskId, solution.TextData, solution.PostTime, solution.Status)
                    .ToCommand(connection)
                    .ExecuteNonQuery();

                var insertAttachment = SQL.INSERT_INTO("Attachment (AttachmentID, SolutionID, Document)");

                if (solution.AttachmentCollection != null)
                {
                    foreach (var attachment in solution.AttachmentCollection)
                    {
                        insertAttachment = insertAttachment.VALUES(Guid.NewGuid(), solution.Id, attachment);
                    }

                    insertAttachment
                        .ToCommand(connection)
                        .ExecuteNonQuery();
                }
            }
        }

        public Solution Read(Guid solutionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("*")
                    .FROM("Solution")
                    .WHERE("SolutionID = {0}", solutionId)
                    .ToCommand(connection)
                    .ExecuteReader();

                reader.Read();

                var solution = new Solution
                {
                    Id = Guid.Parse(reader["SolutionID"].ToString()),
                    AuthorId = Guid.Parse(reader["AuthorID"].ToString()),
                    TaskId = Guid.Parse(reader["TaskID"].ToString()),
                    TextData = reader["TextData"].ToString(),
                    PostTime = DateTime.Parse(reader["Posted"].ToString()),
                    Status = bool.Parse(reader["Resolved"].ToString()),
                    //AttachmentCollection = new List<SqlFileStream>()
                };

                reader = SQL
                    .SELECT("*")
                    .FROM("Attachment")
                    .WHERE("SolutionID = {0}", solutionId)
                    .ToCommand(connection)
                    .ExecuteReader();

                while (reader.Read())
                {
                    //solution.AttachmentCollection.Add(new SqlFileStream());
                }

                return solution;
            }
        }

        public ICollection<Solution> ReadByTask(Guid taskId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("*")
                    .FROM("Solution")
                    .WHERE("TaskID = {0}", taskId)
                    .ToCommand(connection)
                    .ExecuteReader();

                ICollection<Solution> solutionList = new List<Solution>();

                while (reader.Read())
                {
                    solutionList.Add(new Solution
                    {
                        Id = Guid.Parse(reader["SolutionID"].ToString()),
                        AuthorId = Guid.Parse(reader["AuthorID"].ToString()),
                        TaskId = Guid.Parse(reader["TaskID"].ToString()),
                        TextData = reader["TextData"].ToString(),
                        PostTime = DateTime.Parse(reader["Posted"].ToString()),
                        Status = bool.Parse(reader["Resolved"].ToString())
                    });
                }

                return solutionList;
            }
        }

        public Solution ReadByTaskAndUser(Guid taskId, Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("*")
                    .FROM("Solution")
                    .WHERE("TaskID = {0}", taskId)
                    ._("AuthorID = {0}", userId)
                    .ToCommand(connection)
                    .ExecuteReader();

                reader.Read();

                var solution = new Solution
                {
                    Id = Guid.Parse(reader["SolutionID"].ToString()),
                    AuthorId = Guid.Parse(reader["AuthorID"].ToString()),
                    TaskId = Guid.Parse(reader["TaskID"].ToString()),
                    TextData = reader["TextData"].ToString(),
                    PostTime = DateTime.Parse(reader["Posted"].ToString()),
                    Status = bool.Parse(reader["Resolved"].ToString()),
                    //AttachmentCollection = new List<SqlFileStream>()
                };

                reader = SQL
                    .SELECT("*")
                    .FROM("Attachment")
                    .WHERE("SolutionID = {0}", solution.Id)
                    .ToCommand(connection)
                    .ExecuteReader();

                while (reader.Read())
                {
                    //solution.AttachmentCollection.Add(new SqlFileStream());
                }

                return solution;
            }
        }

        public void Delete(Guid solutionId)
        {
            throw new NotImplementedException();
        }

        public void ResolveSolution(Guid solutionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var update = SQL
                    .UPDATE("Solution")
                    .SET("Resolved = {0}", true)
                    .WHERE("SolutionID = {0}", solutionId)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public bool IsCanPostSolution(Guid taskId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}