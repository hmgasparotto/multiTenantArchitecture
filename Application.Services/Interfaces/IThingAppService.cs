namespace Application.Services.Interfaces
{
    public interface IThingAppService<TEntity> : IAppServiceBase<TEntity> where TEntity : class
    {
    }
}
