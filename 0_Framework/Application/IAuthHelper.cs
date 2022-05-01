namespace _0_Framework.Application {
    public interface IAuthHelper {
        void SignIn(AuthViewModel command);
        void SignOut();
        bool IsAuthenticated();
        string CurrentAccountRole();
        AuthViewModel CurrentAccountInfo();
        List<int> GetPermissions();
    }
}
