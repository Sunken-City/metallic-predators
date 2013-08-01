using MetallicPredators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MetallicPredatorsTests
{
    
    
    /// <summary>
    ///This is a test class for DeckTest and is intended
    ///to contain all DeckTest Unit Tests
    ///</summary>
	[TestClass()]
	public class DeckTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for draw
		///</summary>
		[TestMethod()]
		public void drawTest()
		{
			Deck target = new Deck(); // TODO: Initialize to an appropriate value
			Card expected = new DefenceCard(5); // TODO: Initialize to an appropriate value
			Card actual;
			actual = target.draw();
			Assert.AreEqual(expected.getValue(), actual.getValue(),"The Value is not equal.");
		}

		/// <summary>
		///A test for shuffle
		///</summary>
		[TestMethod()]
		public void shuffleTest()
		{
			Deck target = new Deck(); // TODO: Initialize to an appropriate value
			target.shuffle();
			Card expected = new DefenceCard(5); // TODO: Initialize to an appropriate value
			Card actual;
			actual = target.draw();
			//NOTE! THIS IS AN UNPREDICTABLE TEST. I'm using it as an initial check for shuffling, 
			//but there's a good chance it will fail just because of the luck of the draw.
			Assert.AreEqual(expected.getValue(), actual.getValue(), "The deck has not been shuffled.");
		}
	}
}
