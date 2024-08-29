// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Steeltoe.Common.HealthChecks;

public interface IHealthAggregator
{
    Task<HealthCheckResult> AggregateAsync(ICollection<IHealthContributor> contributors, ICollection<HealthCheckRegistration> healthCheckRegistrations,
        IServiceProvider serviceProvider, CancellationToken cancellationToken);
}