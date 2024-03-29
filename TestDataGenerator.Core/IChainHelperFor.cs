﻿// ReSharper disable UnusedMemberInSuper.Global

namespace TestDataGenerator.Core;

/// <summary>
/// </summary>
/// <typeparam name="TIn"></typeparam>
/// <typeparam name="TOut"></typeparam>
public interface IChainHelperFor<in TIn, out TOut> : IValueFor<TIn, TOut>
{
    /// <summary>
    /// </summary>
    bool AmIResponsible { get; }

    /// <summary>
    /// </summary>
    IChainHelperFor<TIn, TOut> NextChain { get; }
}