// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Steeltoe.Common.Util;
using Steeltoe.Integration.Attributes;
using Steeltoe.Messaging;
using Steeltoe.Messaging.Support;
using Steeltoe.Stream.Messaging;

namespace Steeltoe.Stream.Test.Tck;

public sealed class PojoMessageToStringMessageServiceActivator
{
    [ServiceActivator(InputChannel = ISink.InputName, OutputChannel = ISource.OutputName)]
    public IMessage<string> Echo(IMessage<Person> value)
    {
        return (IMessage<string>)MessageBuilder.WithPayload(value.Payload.ToString()).SetHeader(MessageHeaders.ContentType, MimeTypeUtils.TextPlain).Build();
    }
}