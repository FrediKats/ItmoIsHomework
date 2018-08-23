namespace ReviewYourself.WebApi.Services
{
    public interface IAuthorizationService
    {
        //TODO: additional info about user
        void RegisterMember(string login, string password);
        //TODO: implement
        //void LogOut(Token token);

        //TODO: implement
        //Token LogIn(User user);
        bool IsUsernameAvaliable(string username);
    }
}