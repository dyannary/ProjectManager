namespace ProjectManager.Domain.Common
{
    public interface IEntity<T>
    {
        T id { get; set; }
    }
}
