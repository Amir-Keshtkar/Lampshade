namespace _0_Framework.Application;

public class AuthViewModel {
    public long Id { get; set; }
    public long RoleId { get; set; }
    public string Role { get; set; }
    public string FullName { get; set; }
    public string Username { get; set; }
    public List<int> Permissions { get; set; }

    public AuthViewModel () {
    }

    public AuthViewModel (long id, long roleId, string role, string fullName, string username, List<int> permissions) {
        Id = id;
        RoleId = roleId;
        Role= role;
        FullName = fullName;
        Username = username;
        Permissions= permissions;
    }
}