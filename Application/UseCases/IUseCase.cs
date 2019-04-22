using System.Collections.Generic;

namespace Application
{
    public interface IUseCase<T>
    {
        IEnumerable<T> Execute(List<T> enume);
    }
}