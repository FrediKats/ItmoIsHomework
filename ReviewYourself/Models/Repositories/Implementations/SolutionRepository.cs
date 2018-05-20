using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DbExtensions;
using ReviewYourself.Models.Tools;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class SolutionRepository : ISolutionRepository
    {
        private string _connectionString;


        public static SolutionRepository Create(string connectionString)
        {
            return new SolutionRepository()
            {
                _connectionString = connectionString
            };
        }
        public SolutionRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AzureConnect"].ConnectionString;
        }

        public void Create(Solution solution)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var insertSolution = SQL
                    .INSERT_INTO("Solution (SolutionID, AuthorID, TaskID, TextData, Posted, Resolved)")
                    .VALUES(Guid.NewGuid(), solution.AuthorId, solution.TaskId, solution.TextData, DateTime.UtcNow, 0)
                    .ToCommand(connection)
                    .ExecuteNonQuery();

                if (solution.AttachmentCollection == null)
                {
                    return;
                }

                var insertAttachment = SQL.INSERT_INTO("Attachment (AttachmentID, SolutionID, Document)");
                foreach (var attachment in solution.AttachmentCollection)
                {
                    insertAttachment = insertAttachment.VALUES(Guid.NewGuid(), solution.Id, attachment);
                }

                insertAttachment
                    .ToCommand(connection)
                    .ExecuteNonQuery();
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

                var solution = ReaderConvertor.ToSolution(reader);

                //reader = SQL
                //    .SELECT("*")
                //    .FROM("Attachment")
                //    .WHERE("SolutionID = {0}", solutionId)
                //    .ToCommand(connection)
                //    .ExecuteReader();

                //while (reader.Read())
                //{
                //    solution.AttachmentCollection.Add(new SqlFileStream());
                //}

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
                    solutionList.Add(ReaderConvertor.ToSolution(reader));
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

                var solution = ReaderConvertor.ToSolution(reader);

                //reader = SQL
                //    .SELECT("*")
                //    .FROM("Attachment")
                //    .WHERE("SolutionID = {0}", solution.Id)
                //    .ToCommand(connection)
                //    .ExecuteReader();

                //while (reader.Read())
                //{
                //    //solution.AttachmentCollection.Add(new SqlFileStream());
                //}

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
                    .SET("Resolved = {0}", 1)
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