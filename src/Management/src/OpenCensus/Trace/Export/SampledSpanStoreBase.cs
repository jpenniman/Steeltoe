﻿// <copyright file="SampledSpanStoreBase.cs" company="OpenCensus Authors">
// Copyright 2018, OpenCensus Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

namespace OpenCensus.Trace.Export
{
    using System.Collections.Generic;

    public abstract class SampledSpanStoreBase : ISampledSpanStore
    {
        protected SampledSpanStoreBase()
        {
        }

        public abstract ISampledSpanStoreSummary Summary { get; }

        public abstract ISet<string> RegisteredSpanNamesForCollection { get; }

        internal static ISampledSpanStore NoopSampledSpanStore { get; } = new NoopSampledSpanStore();

        internal static ISampledSpanStore NewNoopSampledSpanStore
        {
            get
            {
                return new NoopSampledSpanStore();
            }
        }

        public abstract void ConsiderForSampling(ISpan span);

        public abstract IEnumerable<ISpanData> GetErrorSampledSpans(ISampledSpanStoreErrorFilter filter);

        public abstract IEnumerable<ISpanData> GetLatencySampledSpans(ISampledSpanStoreLatencyFilter filter);

        public abstract void RegisterSpanNamesForCollection(IList<string> spanNames);

        public abstract void UnregisterSpanNamesForCollection(IList<string> spanNames);
    }
}