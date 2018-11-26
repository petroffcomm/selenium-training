using NUnit.Framework;

namespace LitecartTestsPObj
{
    public class TestBase
    {
        public ApplicationManager app;

        [SetUp]
        public void SetUp()
        {
            app = new ApplicationManager();
        }


        [TearDown]
        public void TearDown()
        {
            app.Quit();
            app = null;
        }
    }
}
