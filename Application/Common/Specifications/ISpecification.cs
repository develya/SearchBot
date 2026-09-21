using System.Linq.Expressions;

namespace Application.Common.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();   //for sql , transform rool on term
    
    bool IsSatisfiedBy(T entity);
    
}