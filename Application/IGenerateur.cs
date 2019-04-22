namespace Application
{
    public interface IGenerateur<T>
    {
        T GetNewId();
    }
}