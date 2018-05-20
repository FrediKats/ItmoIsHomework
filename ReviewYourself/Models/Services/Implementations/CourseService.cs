using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CreateCourse(Token token, Course course)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            //TODO: create mentor here?
            if (course.Mentor.Id != token.UserId)
            {
                throw new Exception("Wrong mentorId");
            }

            _courseRepository.Create(course);
        }

        public void InviteUser(Token token, string username, Guid courseId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            var course = _courseRepository.Read(courseId);
            if (course.Mentor.Id != token.UserId)
            {
                throw new Exception("User isn't mentor");
            }

            var newMember = _userRepository.ReadByUserName(username);

            if (newMember == null)
            {
                throw new Exception("User not found");
            }

            _courseRepository.CreateMember(courseId, newMember.Id);
        }

        public void AcceptInvite(Token token, Guid courseId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            var user = _courseRepository
                .ReadInvitedByCourse(courseId)
                .FirstOrDefault(u => u.Id == token.UserId);

            if (user == null)
            {
                throw new Exception("User not invited");
            }

            _courseRepository.AcceptInvite(courseId, token.UserId);
        }

        public bool IsMember(Token token, Guid courseId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            var user = _courseRepository
                .ReadMembersByCourse(courseId)
                .FirstOrDefault(u => u.Id == token.UserId);

            return user != null;
        }

        public bool IsMentor(Token token, Guid courseId)
        {
            var course = _courseRepository.Read(courseId);
            return token.UserId == course.Mentor.Id;
        }

        public Course GetCourse(Token token, Guid courseId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            return _courseRepository.Read(courseId);
        }

        public ICollection<Course> GetCourseListByUser(Token token, Guid userId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            return _courseRepository.ReadByUser(userId);
        }

        public ICollection<Course> GetInviteListByUser(Token token, Guid userId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            return _courseRepository.ReadInvitesByUser(userId);
        }

        public void UpdateCourse(Token token, Course course)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            var resultCourse = _courseRepository.Read(course.Id);
            if (resultCourse.Mentor.Id != token.UserId || resultCourse.Mentor.Id != course.Mentor.Id)
            {
                throw new Exception("User isn't mentor");
            }

            _courseRepository.Update(course);
        }

        public void DeleteCourse(Token token, Guid courseId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            var resultCourse = _courseRepository.Read(courseId);
            if (resultCourse.Mentor.Id != token.UserId)
            {
                throw new Exception("User isn't mentor");
            }

            _courseRepository.Delete(courseId);
        }

        public void DeleteMember(Token token, Guid courseId, Guid userId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            var resultCourse = _courseRepository.Read(courseId);
            if (resultCourse.Mentor.Id != token.UserId)
            {
                throw new Exception("User isn't mentor");
            }

            _courseRepository.DeleteMember(courseId, userId);
        }
    }
}