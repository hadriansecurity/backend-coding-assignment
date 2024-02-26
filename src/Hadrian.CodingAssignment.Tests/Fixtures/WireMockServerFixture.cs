using WireMock.Logging;
using WireMock.Matchers.Request;
using WireMock.Server;

namespace Hadrian.CodingAssignment.Tests.Fixtures;

public class WireMockServerFixture : IDisposable
{
    public virtual WireMockServer Server { get; } = WireMockServer.Start();
    public virtual Uri Uri => new(Server.Urls[0], UriKind.Absolute);

    public virtual IEnumerable<LogEntry> FindLogEntries(params IRequestMatcher[] matchers) =>
        Server.FindLogEntries(matchers);

    public virtual IRespondWithAProvider Given(IRequestMatcher requestMatcher) =>
        Server.Given(requestMatcher);

    public virtual void Reset()
    {
        Server.Reset();
        Server.ResetScenarios(); // NOTE Server.Reset() does NOT reset scenarios, see: https://github.com/WireMock-Net/WireMock.Net/issues/1030
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Server.Stop();
            Server.Dispose();
        }
    }
}
