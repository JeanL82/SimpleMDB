namespace SimpleMDB;

public class MockUser : UserRpsository
{
    private List<User> users;
    private int idCount;



    public MockUser(MockUser userRepository)
    {
        users = [];
        idCount = 0;

        var usernames = new string[]
        {
            "papo" , "pepo","popo" , "pipo",
            "momo" , "moma" , "mama","papa",
            "lalo", "lola" , "jose" , "Juan"
        };

        Random r = new Random();

        foreach(var username in usernames)
        {
            var pass = Path.GetRandomFileName();
            var salt = Path.GetRandomFileName();
            User user = new User(idCount++,username,pass,salt,Roles.ROLES[r.Next(Roles.ROLES.Length)]);
            users.Add(user);
        }
    }

    public MockUser()
    {
    }

    public  async Task<PageResult<User>> ReadAll(int page , int size)
    {
        int totalCount = users.Count;
        int start = Math.Clamp((page - 1) * size, 0, totalCount);
        int length = Math.Clamp(size , 0, totalCount - start);
        List<User> values = users.Slice(start, length);
        var PageResult = new PageResult<User>(values, totalCount);

        return await Task.FromResult(PageResult);
    }
    public  async Task<User?> Create(User user)
    {
        user.Id = idCount++;
        users.Add(user);

        return await Task.FromResult(user);
    }
    public  async Task<User?> Read(int id)
    {
        User? user = users.FirstOrDefault((u) => u.Id == id);

        return await Task.FromResult(user);
    }

    public async Task<User?> Update(int id,User newUser)
    {
          User? user = users.FirstOrDefault((u) => u.Id == id);

         if (user != null)
         {
            user.Username = newUser.Username;
            user.Password = newUser.Password;
            user.Salt = newUser.Salt;
            user.Role = newUser.Role;
         }
        return await Task.FromResult(user);

    }
    public async Task<User?> Delete(int id)
    {
          User? user = users.FirstOrDefault((u) => u.Id == id);

if (user != null){
    users.Remove(user);

}
        return await Task.FromResult(user);
    }
}