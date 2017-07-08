using DomainInterfaces;
using Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class ProviderTests
    {
        [TestMethod]
        public void ProviderFetchTextTest()
        {
            ITextProvider provider = new TextProvider();

            string text = provider.FetchText();

            Assert.AreEqual("Hello world!", text);
        }
    }
}
