namespace _0_Framework.Application {
    public interface IAuthHelper {
        void SignIn(AuthViewModel command);
        void SignOut();
        bool IsLoggedIn();
    }
}
