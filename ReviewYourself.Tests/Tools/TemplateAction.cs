using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using ReviewYourself.Controllers;
using ReviewYourself.Models;

namespace ReviewYourself.Tests.Tools
{
    public static class TemplateAction
    {
        public static Course CreateCourse(Token token, CourseController controller)
        {
            var course = InstanceGenerator.GenerateCourse();

            controller.Create(token, course);
            var currentCourse = controller.GetByUser(token)
                .Cast<ICollection<Course>>()
                .First(c => c.Title == course.Title);
            return currentCourse;
        }

        //public static ResourceTask CreateTask(Token token, Course course, TaskController controller)
        //{
        //    var task = InstanceGenerator.GenerateTask();

        //    task.CourseId = course.Id;
        //    controller.Add(task, token);

        //    var resultTask = controller
        //        .GetByCourse(course.Id, token)
        //        .Cast<ICollection<ResourceTask>>()
        //        .First(t => t.Title == task.Title && t.Description == task.Description);
        //    return resultTask;
        //}

        public static ResourceTask CreateTaskWithCriteria(Token token, Course course, TaskController controller)
        {
            var task = InstanceGenerator.GenerateTask();
            task.CriteriaCollection = new List<Criteria>();
            task.CourseId = course.Id;
            for (int i = 0; i < 3; i++)
            {
                task.CriteriaCollection.Add(new Criteria()
                {
                    Title = InstanceGenerator.GenerateString(),
                    Description = InstanceGenerator.GenerateString(),
                    MaxPoint = new Random().Next(5, 10),
                    TaskId = task.Id
                });
            }
            controller.Add(task, token);

            var resultTask = controller
                .GetByCourse(course.Id, token)
                .Cast<ICollection<ResourceTask>>()
                .First(t => t.Title == task.Title && t.Description == task.Description);
            var taskData = controller.Get(task.Id, token).Cast<ResourceTask>();
            return taskData;
        }

        public static Solution CreateSolution(Token token, ResourceTask task, SolutionController controller)
        {
            var solution = InstanceGenerator.GenerateSolution();
            solution.TaskId = task.Id;
            controller.Add(token, solution);

            var resultSolution = controller.GetByTask(task.Id, token)
                .Cast<ICollection<Solution>>()
                .First(s => s.TextData == solution.TextData);
            return resultSolution;
        }

        public static T Cast<T>(this IHttpActionResult result)
        {
            return ((OkNegotiatedContentResult<T>)result).Content;
        }
    }
}