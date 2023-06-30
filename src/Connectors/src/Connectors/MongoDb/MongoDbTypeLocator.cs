// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.Common.Reflection;

// ReSharper disable once CheckNamespace
namespace Steeltoe.Connectors.MongoDb;

/// <summary>
/// Assemblies and types used for interacting with MongoDB.
/// </summary>
public static class MongoDbTypeLocator
{
    /// <summary>
    /// Gets a list of supported MongoDB assemblies.
    /// </summary>
    public static string[] Assemblies { get; internal set; } =
    {
        "MongoDB.Driver"
    };

    /// <summary>
    /// Gets a list of supported MongoDB client interface types.
    /// </summary>
    public static string[] ConnectionInterfaceTypeNames { get; internal set; } =
    {
        "MongoDB.Driver.IMongoClient"
    };

    /// <summary>
    /// Gets a list of supported MongoDB client types.
    /// </summary>
    public static string[] ConnectionTypeNames { get; internal set; } =
    {
        "MongoDB.Driver.MongoClient"
    };

    /// <summary>
    /// Gets the class used for describing MongoDB connection information.
    /// </summary>
    public static string[] MongoConnectionInfo { get; internal set; } =
    {
        "MongoDB.Driver.MongoUrl"
    };

    /// <summary>
    /// Gets IMongoClient from MongoDB Library.
    /// </summary>
    /// <exception cref="ConnectorException">
    /// When type is not found.
    /// </exception>
    public static Type MongoClientInterface => ReflectionHelpers.FindTypeOrThrow(Assemblies, ConnectionInterfaceTypeNames, "IMongoClient", "a MongoDB driver");

    /// <summary>
    /// Gets MongoClient from MongoDB Library.
    /// </summary>
    /// <exception cref="ConnectorException">
    /// When type is not found.
    /// </exception>
    public static Type MongoClient => ReflectionHelpers.FindTypeOrThrow(Assemblies, ConnectionTypeNames, "MongoClient", "a MongoDB driver");

    /// <summary>
    /// Gets MongoUrl from MongoDB Library.
    /// </summary>
    /// <exception cref="ConnectorException">
    /// When type is not found.
    /// </exception>
    public static Type MongoUrl => ReflectionHelpers.FindTypeOrThrow(Assemblies, MongoConnectionInfo, "MongoUrl", "a MongoDB driver");
}
