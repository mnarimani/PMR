using NUnit.Framework;
using PMR.Signals;

namespace PMR
{
    public class SignalBusTests
    {
        [Test]
        public void SignalSubscriptions_DontMixBetweenInstances()
        {
            var s1 = new SignalBus();
            var s2 = new SignalBus();

            s1.Subscribe<int>(i => { });
            
            Assert.IsTrue(s1.Fire(1));
            Assert.IsFalse(s2.Fire(1));
        }

        [Test]
        public void Unsubscribe()
        {
            var s = new SignalBus();
            SignalHandle handle = s.Subscribe<int>(i => { });
            Assert.IsTrue(s.Fire(1));
            handle.Unsubscribe();
            Assert.IsFalse(s.Fire(1));
        }
    }
}