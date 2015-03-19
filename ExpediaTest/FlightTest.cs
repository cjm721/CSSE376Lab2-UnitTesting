using System;
using Expedia;
using NUnit.Framework;

namespace ExpediaTest
{
	[TestFixture()]
	public class FlightTest
	{

		[Test]
        public void TestFlightCreationNormal()
        {
            Flight flight = new Flight(DateTime.Now, DateTime.Now.AddDays(1), 10);
            Assert.NotNull(flight);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestFlightErrorEndBeforeStart()
        {
            Flight flight = new Flight(DateTime.Now, DateTime.Now.AddDays(-1), 10);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestFlightErrorNegativeMiles()
        {
            Flight flight = new Flight(DateTime.Now, DateTime.Now.AddDays(1), -1);
        }

        [Test]
        public void TestFlightMileLowerBoundry()
        {
            Flight flight = new Flight(DateTime.Now, DateTime.Now.AddDays(1), 0);
            Assert.AreEqual(0, flight.Miles);
        }

        [Test]
        public void TestFlightMileUpperBoundry()
        {
            Flight flight = new Flight(DateTime.Now, DateTime.Now.AddDays(1), Int32.MaxValue);
            Assert.AreEqual(Int32.MaxValue, flight.Miles);
        }

        [Test]
        public void TestBasePriceLow()
        {
            Flight flight = new Flight(DateTime.Now, DateTime.Now.AddHours(1), 0);
            Assert.AreEqual(200D, flight.getBasePrice());
        }

        [Test]
        public void TestBasePriceAverage()
        {
            Flight flight = new Flight(DateTime.Now, DateTime.Now.AddDays(7), 500);
            Assert.AreEqual(200+7*20, flight.getBasePrice());
        }

        [Test]
        public void TestBasePriceMax()
        {
            Flight flight = new Flight(DateTime.Now, DateTime.MaxValue, 500);
            Double price = 200 + 20 * (DateTime.MaxValue - DateTime.Now).Days;
            Assert.AreEqual(price, flight.getBasePrice());
        }

        [Test]
        public void TestEqualsWithNonFlightObject()
        {
            Flight flight = new Flight(DateTime.Now, DateTime.Now.AddDays(7), 500);
            Assert.False(flight.Equals("Not a Flight"));
        }

        [Test]
        public void TestEqualsWithNull()
        {
            Flight flight = new Flight(DateTime.Now, DateTime.Now.AddDays(7), 500);
            Assert.False(flight.Equals(null));
        }

        [Test]
        public void TestEqualsWithFlightObjectTrue()
        {
            Flight flightA = new Flight(DateTime.Now, DateTime.Now.AddDays(7), 500);
            Flight flightB = new Flight(DateTime.Now, DateTime.Now.AddDays(7), 500);
            Assert.True(flightA.Equals(flightB));
            Assert.True(flightB.Equals(flightA));
        }

        [Test]
        public void TestEqualsWithFlightObjectFalseDate()
        {
            Flight flightA = new Flight(DateTime.Now, DateTime.Now.AddDays(7), 500);
            Flight flightB = new Flight(DateTime.Now.AddHours(1), DateTime.Now.AddDays(7), 500);
            Assert.False(flightA.Equals(flightB));
            Assert.False(flightB.Equals(flightA));


            Flight flightC = new Flight(DateTime.Now, DateTime.Now.AddDays(7), 500);
            Flight flightD = new Flight(DateTime.Now, DateTime.Now.AddDays(7).AddHours(1), 500);
            Assert.False(flightC.Equals(flightD));
            Assert.False(flightD.Equals(flightC));
        }

        [Test]
        public void TestEqualsWithFlightObjectFalseMiles()
        {
            Flight flightA = new Flight(DateTime.Now, DateTime.Now.AddDays(7), 500);
            Flight flightB = new Flight(DateTime.Now, DateTime.Now.AddDays(7), 1000);
            Assert.False(flightA.Equals(flightB));
            Assert.False(flightB.Equals(flightA));
        }
	}
}
