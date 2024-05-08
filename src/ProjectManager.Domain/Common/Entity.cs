namespace ProjectManager.Domain.Common
{
    public abstract class Entity<T>
    {
        T Id { get; set; }
    }
}
