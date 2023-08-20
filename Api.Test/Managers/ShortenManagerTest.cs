using Api.Managers;
using Domain.Models;

namespace Api.Test.Managers
{
    public class ShortenManagerTest
    {
        private ShortenManager manager;
        [SetUp]
        public void Setup()
        {
            manager = new ShortenManager();
        }

        [Test]
        public void Shorten_ReturnsUrlWithCorrectPrefix()
        {
            var urls = new List<Url>();
            var url = new Url
            {
                FullAddress = "https://google.com"
            };

            var shortenedUrl = manager.Shorten(url, urls);
            var shortAddress = shortenedUrl.ShortAddress;
            Assert.That(shortAddress.StartsWith(manager.Prefix));
        }
    }
}