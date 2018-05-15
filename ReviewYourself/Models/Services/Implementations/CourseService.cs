using System;
using System.Collections.Generic;
using ReviewYourself.Models.Repositories;

namespace ReviewYourself.Models.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;

        public CourseService(ICourseRepository courseRepository, IUserRepository userRepository,
            ITokenRepository tokenRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public void AddCourse(Course course, Token token)
        {
            //ResourceUser user = _tokenRepository.GetUserByToken(token);
            //if (user == null)
            //{
            //    throw new Exception();
            //}
            //if (course.Mentor.Id != user.Id)
            //{
            //    throw new Exception();
            //}

            _courseRepository.Create(course);
        }

        public void InviteUser(string username, Guid courseId, Token token)
        {
            //var user = _tokenRepository.GetUserByToken(token);
            //var course = _courseRepository.Read(courseId);
            var member = _userRepository.ReadByUserName(username);

            //if (user == null || course == null || member == null)
            //{
            //    throw new Exception();
            //}

            //if (course.Mentor.Id != user.Id)
            //{
            //    throw new Exception();
            //}

            _courseRepository.CreateMember(courseId, member.Id);
        }

        public void AcceptInvite(Guid courseId, Token token)
        {
            //var user = _tokenRepository.GetUserByToken(token);
            //if (user == null)
            //{
            //    throw new Exception();
            //}

            throw new NotImplementedException();
        }

        public Course GetCourse(Guid courseId, Token token)
        {
            //var user = _tokenRepository.GetUserByToken(token);
            //var course = _courseRepository.Read(courseId);

            //if (user == null || course == null)
            //{
            //    throw new Exception();
            //}

            return _courseRepository.Read(courseId);
        }

        public ICollection<Course> GetCourseCollectionByUser(Guid userId, Token token)
        {
            //TODO: Admin rights
            //var user = _tokenRepository.GetUserByToken(token);
            //if (user == null)
            //{
            //    throw new Exception();
            //}

            return _courseRepository.ReadByUser(userId);
        }

        public ICollection<Course> GetInviteCollectionByUser(Guid userId, Token token)
        {
            throw new NotImplementedException();
        }

        public void UpdateCourse(Course course, Token token)
        {
            //var user = _tokenRepository.GetUserByToken(token);
            //var mentor = _courseRepository.Read(course.Id).Mentor;

            //if (user == null || course == null)
            //{
            //    throw new Exception();
            //}

            //if (user.Id != mentor.Id)
            //{
            //    throw new Exception();
            //}

            _courseRepository.Update(course);
        }

        public void DeleteCourse(Guid courseId, Token token)
        {
            //var user = _tokenRepository.GetUserByToken(token);
            //var mentor = _courseRepository.Read(courseId)?.Mentor;

            //if (user == null || mentor == null)
            //{
            //    throw new Exception();
            //}

            //if (user.Id != mentor.Id)
            //{
            //    throw new Exception();
            //}

            _courseRepository.Delete(courseId);
        }

        public void DeleteMember(Guid courseId, Guid userId, Token token)
        {
            //var user = _tokenRepository.GetUserByToken(token);
            //var mentor = _courseRepository.Read(courseId)?.Mentor;

            //if (user == null || mentor == null)
            //{
            //    throw new Exception();
            //}

            //if (user.Id != mentor.Id)
            //{
            //    throw new Exception();
            //}

            _courseRepository.DeleteMember(courseId, userId);
        }
    }
}