namespace SimpleMDB;

public class MockActorService : IActorService
{
    private IActorRepository actorRepository;

    public MockActorService(IActorRepository actorRepository)
    {
        this.actorRepository = actorRepository;
    }
    public async Task<Result<PageResult<Actor>>> ReadAll(int page, int size)
    {
        var pagedResult = await actorRepository.ReadAll(page, size);

        var result = (pagedResult == null) ?
        new Result<PageResult<Actor>>(new Exception("No results found.")) :
        new Result<PageResult<Actor>>(pagedResult);

        return await Task.FromResult(result);
    }
    public async Task<Result<Actor>> Create(Actor newActor)
    {
        if (string.IsNullOrWhiteSpace(newActor.FirstName))
        {
            return new Result<Actor>(new Exception("First name cannot be empty."));
        }
        else if (newActor.FirstName.Length > 16)
        {
            return new Result<Actor>(new Exception("First name cannot have more than 16 characters."));
        }

        else if (string.IsNullOrWhiteSpace(newActor.LastName))
        {
            return new Result<Actor>(new Exception("Last name cannot be empty."));
        }
        else if (newActor.LastName.Length > 16)
        {
            return new Result<Actor>(new Exception("Last name cannot have more than 16 characters."));
        }

        Actor? actor = await actorRepository.Create(newActor);

        var result = (actor == null) ?
        new Result<Actor>(new Exception("Actor could not be created.")) :
        new Result<Actor>(actor);

        return await Task.FromResult(result);
    }
    public async Task<Result<Actor>> Read(int id)
    {
        Actor? actor = await actorRepository.Read(id);

        var result = (actor == null) ?
        new Result<Actor>(new Exception("Actor could not be read.")) :
        new Result<Actor>(actor);
        return await Task.FromResult(result);
    }
    public async Task<Result<Actor>> Update(int id, Actor newActor)
    {
         if (string.IsNullOrWhiteSpace(newActor.FirstName))
        {
            return new Result<Actor>(new Exception("First name cannot be empty."));
        }
        else if (newActor.FirstName.Length > 16)
        {
            return new Result<Actor>(new Exception("First name cannot have more than 16 characters."));
        }

        else if (string.IsNullOrWhiteSpace(newActor.LastName))
        {
            return new Result<Actor>(new Exception("Last name cannot be empty."));
        }
        else if (newActor.LastName.Length > 16)
        {
            return new Result<Actor>(new Exception("Last name cannot have more than 16 characters."));
        }


        Actor? actor = await actorRepository.Update(id, newActor);

        var result = (actor == null) ?
        new Result<Actor>(new Exception("Actor could not be Updated.")) :
        new Result<Actor>(actor);
        return await Task.FromResult(result);
    }
    public async Task<Result<Actor>> Delete(int id)
    {
        Actor? actor = await actorRepository.Delete(id);

        var result = (actor == null) ?
        new Result<Actor>(new Exception("Actor could not be deleted.")) :
        new Result<Actor>(actor);
        return await Task.FromResult(result);
    }
}