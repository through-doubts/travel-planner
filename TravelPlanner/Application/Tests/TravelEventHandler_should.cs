using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Application.Tests
{
    [TestFixture]
    public class TravelEventHandler_should
    {
        [Test]
        public void NamesAreCorrect()
        {
            var transfer = new Transfer();
            var housing = new Housing();
            var events = new ITravelEvent[] { transfer, housing };
            var eventHandler = new TravelEventHandler(events);
            var nameList = new List<string> { transfer.Name, housing.Name };
            nameList.Sort();
            Assert.AreEqual(nameList, eventHandler.GetEventsNames());
        }

        [Test]
        public void GetCorrectEvent()
        {
            var dateTimeInterval = new DateTimeInterval(DateTime.Now, DateTime.Now);
            var cost = new Money(Currency.RUB, 10);
            var type = TransferType.Bus;

            var transfer = new Transfer(dateTimeInterval, cost, type);
            var housing = new Housing();
            var events = new ITravelEvent[] { transfer, housing };
            var eventHandler = new TravelEventHandler(events);

            var actualTransfer = (Transfer)eventHandler.GetEvent(transfer.Name, dateTimeInterval, cost, type);
            Assert.AreEqual(transfer, actualTransfer);
        }

        [Test]
        public void GetEventIncorrectName()
        {
            var events = new ITravelEvent[] { new Transfer(), new Housing() };
            var eventHandler = new TravelEventHandler(events);
            var name = "Unknown";
            Assert.Throws(typeof(ArgumentException), () => eventHandler.GetEvent(name));
        }

        [Test]
        public void GetEventIncorrectArguments()
        {
            var transfer = new Transfer();
            var events = new ITravelEvent[] { transfer, new Housing() };
            var eventHandler = new TravelEventHandler(events);
            var name = transfer.Name;
            Assert.Throws(
                typeof(ArgumentException),
                () => eventHandler.GetEvent(name, TransferType.Bus));
        }
    }
}
