namespace TestDataGenerator.Core;

/// <inheritdoc />
public abstract class ChainHelperFor<TIn, TOut> : IChainHelperFor<TIn, TOut>
{
    /// <summary>
    /// </summary>
    protected TIn Input;

    /// <summary>
    ///     Initializes a new instance of the <see cref="T:System.Object" /> class.
    /// </summary>
    protected ChainHelperFor(IChainHelperFor<TIn, TOut> chainHelperFor)
    {
        if (chainHelperFor == null)
        {
            //   throw new ArgumentNullException(nameof(chainHelperFor));
        }

        NextChain = chainHelperFor;
    }

    /// <inheritdoc />
    public IChainHelperFor<TIn, TOut> NextChain { get; }

    /// <inheritdoc />
    public abstract bool AmIResponsible { get; }

    /// <inheritdoc />
    public TOut ValueFor(TIn input)
    {
        Input = input;
        return AmIResponsible ? InnerValueFor(input) : NextChain.ValueFor(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    // ReSharper disable once UnusedParameter.Global
    protected abstract TOut InnerValueFor(TIn input);
}