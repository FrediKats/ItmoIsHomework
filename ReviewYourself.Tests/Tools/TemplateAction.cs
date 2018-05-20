using System;
using System.Collections.Generic;
using System.Linq;
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
            var currentCourse = controller.GetByUser(token).First(c => c.Title == course.Title);
            return currentCourse;
        }

        public static ResourceTask CreateTask(Token token, Course course, TaskController controller)
        {
            var task = InstanceGenerator.GenerateTask();

            task.CourseId = course.Id;
            controller.Add(task, token);

            var resultTask = controller
                .GetByCourse(course.Id, token)
                .First(t => t.Title == task.Title && t.Description == task.Description);
            return resultTask;
        }

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
                .First(t => t.Title == task.Title && t.Description == task.Description);
            return resultTask;
        }

        public static Solution CreateSolution(Token token, ResourceTask task, SolutionController controller)
        {
            var solution = InstanceGenerator.GenerateSolution();
            solution.TaskId = task.Id;
            controller.Add(token, solution);

            var resultSolution = controller.GetByTask(task.Id, token)
                .First(s => s.TextData == solution.TextData);
            return resultSolution;
        }
    }
}