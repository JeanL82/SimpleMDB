namespace SimpleMDB;


public interface UserServices
{
    public Task<Result<PageResult<User>>> ReadAll(int page , int pageSize);
    public Task<Result<User?>> Create(User user);
    public Task<Result<User?>> Read(int id);

    public Task<Result<User?>> Update(int id,User newUser);
    public Task<Result<User?>> Delete(int id);
}