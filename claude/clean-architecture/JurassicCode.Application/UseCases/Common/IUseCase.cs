namespace JurassicCode.Application.UseCases.Common;

/// <summary>
/// Base use case interface for parameter-less use cases
/// </summary>
public interface IUseCase<TResult>
{
    Task<TResult> ExecuteAsync();
}

/// <summary>
/// Base use case interface that takes a parameter
/// </summary>
public interface IUseCase<in TParam, TResult>
{
    Task<TResult> ExecuteAsync(TParam parameter);
}