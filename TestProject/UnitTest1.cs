using System.Net.Sockets;
using SUT;

namespace TestProject {
    public class NewCustomerTests
    {
        private Customer receiver;
        [SetUp]
        public void Setup() {
            receiver = new Customer(DateTime.Now);
        }

        [TearDown]
        public void TearDown() {
            receiver = null;
        }

        [Test]
        public void PriceLowerThan100PriceAsGiven() {
            var itemCost = 42.42;
            var actual = receiver.ExpectedCost(itemCost);
            Assert.That(actual, Is.EqualTo(itemCost).Within(0.00001));
        }

        [TestCase(100.0, 100.0)]
        [TestCase(200.0, 194.0)]
        public void PriceHigherThan100Get3PercentDiscount(double itemCost, double expectedPrice) {
            var actual = receiver.ExpectedCost(itemCost);
            Assert.That(actual, Is.EqualTo(expectedPrice).Within(0.00001));
        }

        [Test]
        public void PriceLowerThan100MoreThan100OrdersGet0dot5PercDiscount() {
            receiver.OrderNumber = 12;
            var itemCost = 42.42;
            var actual = receiver.ExpectedCost(itemCost);
            var expectedPrice = 42.2079; //0.5% as customer has more than 10 orders 
            Assert.That(actual, Is.EqualTo(expectedPrice).Within(0.00001));

        }

        [Test]
        public void PriceHigherhan100MoreThan100OrdersGet3dot5PercDiscount() {
            receiver.OrderNumber = 12;
            const int itemCost = 200;
            var actual = receiver.ExpectedCost(itemCost);
            var expectedPrice = 193; //3.5% as customer has more than 10 orders and 
            Assert.That(actual, Is.EqualTo(expectedPrice).Within(0.00001));

        }

        [Test]
        public void SetOrderNumberOnNegativeThrows() {
            Assert.That(()=> receiver.OrderNumber=-8, Throws.InstanceOf<ArgumentException>());
        }
    }
}