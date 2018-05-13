using System;
using System.Collections.Generic;
using ReviewYourself.Models.Repositories;

namespace ReviewYourself.Models.Services.Implementations
{
    public class CourseService : ICourseService
    {
        private IUserRepository _userRepository;
        private ICourseRepository _courseRepository;
        private ITokenRepository _tokenRepository;
        public CourseService(ICourseRepository courseRepository, IUserRepository userRepository, ITokenRepository tokenRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }
        public void AddCourse(Course course, Token token)
        {
            throw new NotImplementedException();
        }

        public void InviteUser(string username, Guid courseId, Token token)
        {
            throw new NotImplementedException();
        }

        public void AcceptInvite(Guid courseId, Token token)
        {
            throw new NotImplementedException();
        }

        public Course GetCourse(Guid courseId, Token token)
        {
            throw new NotImplementedException();
        }

        public ICollection<Course> GetCourseCollectionByUser(Guid userId, Token token)
        {
            throw new NotImplementedException();
        }

        public void UpdateCourse(Course course, Token token)
        {
            throw new NotImplementedException();
        }

        public void DeleteCourse(Guid courseId, Token token)
        {
            throw new NotImplementedException();
        }

        public void DeleteMember(Guid courseId, Guid userId, Token token)
        {
            throw new NotImplementedException();
        }
    }
}