using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Hadrian.CodingAssignment.Tests.Fixtures;

public class WebhookFixture : WireMockServerFixture
{
    private readonly string _path;

    public IUrlAndPathRequestBuilder Post => Request
        .Create()
        .UsingPost()
        .WithPath(_path);

    public Uri WebhookUrl => new(Uri, _path);

    public WebhookFixture(string path = "")
    {
        _path = path;

        Server
            .Given(Post)
            .RespondWith(Response
                .Create()
                .WithSuccess());
    }
}
