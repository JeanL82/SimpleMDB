namespace SimpleMDB;

public class UserRepository
{
    public async Task<PageResult<User>> ReadAll(int page, int size)
    {
        await Task.CompletedTask; // Simula trabajo asincrónico
        throw new NotImplementedException();
    }

    public async Task<User?> Create(User user)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task<User?> Read(int id)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task<User?> Update(int id, User user)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }

    public async Task<User?> Delete(int id)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}