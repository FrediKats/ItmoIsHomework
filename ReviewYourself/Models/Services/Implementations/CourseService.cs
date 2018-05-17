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

        public void AddCourse(Token token, Course course)
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

        public void InviteUser(Token token, string username, Guid courseId)
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

        public void AcceptInvite(Token token, Guid courseId)
        {
            //var user = _tokenRepository.GetUserByToken(token);
            //if (user == null)
            //{
            //    throw new Exception();
            //}

            throw new NotImplementedException();
        }

        public bool IsMember(Token token)
        {
            throw new NotImplementedException();
        }

        public Course GetCourse(Token token, Guid courseId)
        {
            //var user = _tokenRepository.GetUserByToken(token);
            //var course = _courseRepository.Read(courseId);

            //if (user == null || course == null)
            //{
            //    throw new Exception();
            //}

            return _courseRepository.Read(courseId);
        }

        public ICollection<Course> GetCourseCollectionByUser(Token token, Guid userId)
        {
            //TODO: Admin rights
            //var user = _tokenRepository.GetUserByToken(token);
            //if (user == null)
            //{
            //    throw new Exception();
            //}

            return _courseRepository.ReadByUser(userId);
        }

        public ICollection<Course> GetInviteCollectionByUser(Token token, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCourse(Token token, Course course)
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

        public void DeleteCourse(Token token, Guid courseId)
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

        public void DeleteMember(Token token, Guid courseId, Guid userId)
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