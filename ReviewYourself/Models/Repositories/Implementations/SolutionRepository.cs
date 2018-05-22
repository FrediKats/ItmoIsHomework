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

                solution.Id = Guid.NewGuid();
                solution.PostTime = DateTime.UtcNow;

                SQL.INSERT_INTO("Solution (SolutionID, AuthorID, TaskID, TextData, Posted, Resolved)")
                    .VALUES(solution.Id, solution.AuthorId, solution.TaskId, solution.TextData, solution.PostTime, 0)
                    .ToCommand(connection)
                    .ExecuteNonQuery();

                if (solution.AttachmentCollection == null) return;

                var insertAttachment = new SqlBuilder();
                //var insertAttachment = SQL.INSERT_INTO("Attachment (AttachmentID, SolutionID, Document)");

                foreach (var attachment in solution.AttachmentCollection)
                {
                    insertAttachment = insertAttachment
                        .INSERT_INTO("Attachment (AttachmentID, SolutionID, Document)")
                        .VALUES(Guid.NewGuid(), solution.Id, attachment)
                        .Append(";");
                    //insertAttachment = insertAttachment.VALUES(Guid.NewGuid(), solution.Id, attachment);
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

                var command = SQL
                    .SELECT("*")
                    .FROM("Solution")
                    .WHERE("SolutionID = {0}", solutionId)
                    .ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    return ReaderConvertor.ToSolution(reader);
                }

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
            }
        }

        public ICollection<Solution> ReadByTask(Guid taskId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = SQL
                    .SELECT("*")
                    .FROM("Solution")
                    .WHERE("TaskID = {0}", taskId)
                    .ToCommand(connection);

                ICollection<Solution> solutionList = new List<Solution>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        solutionList.Add(ReaderConvertor.ToSolution(reader));
                    }
                }

                return solutionList;
            }
        }

        public Solution ReadByTaskAndUser(Guid taskId, Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = SQL
                    .SELECT("*")
                    .FROM("Solution")
                    .WHERE("TaskID = {0}", taskId)
                    ._("AuthorID = {0}", userId)
                    .ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    return ReaderConvertor.ToSolution(reader);
                }

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

                SQL.UPDATE("Solution")
                    .SET("Resolved = {0}", 1)
                    .WHERE("SolutionID = {0}", solutionId)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public bool CanPostSolution(Guid taskId, Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var permission = SQL
                    .SELECT("Permission")
                    .FROM("CourseMembership")
                    .JOIN("({0}) res ON CourseMembership.CourseID = res.CourseID",
                        SQL.SELECT("CourseID")
                            .FROM("ResourceTask")
                            .WHERE("TaskID = {0}", taskId))
                    .WHERE("UserID = {0}", userId)
                    .ToCommand(connection)
                    .ExecuteScalar();

                return (int.Parse(permission?.ToString() ?? "0") > 0);
            }
        }
    }
}