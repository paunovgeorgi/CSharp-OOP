using System;
using System.Linq;
using System.Numerics;
using NUnit.Framework;

namespace FootballTeam.Tests
{
    public class Tests
    {
        private FootballPlayer footballPlayer;
        private FootballTeam footballTeam;

        [SetUp]
        public void Setup()
        {
            footballPlayer = new FootballPlayer("DanJames", 7, "Forward");
            footballTeam = new FootballTeam("United", 15);
        }

        [Test]
        public void FP_Constructor_IsWorkingProperly()
        {
            Assert.NotNull(footballPlayer);
            Assert.AreEqual("DanJames", footballPlayer.Name);
            Assert.AreEqual(7, footballPlayer.PlayerNumber);
            Assert.AreEqual("Forward", footballPlayer.Position);
            Assert.AreEqual(0, footballPlayer.ScoredGoals);
        }

        [Test]

        public void FP_NameProperty_IsWorkingCorrectly()
        {
            Assert.AreEqual("DanJames",footballPlayer.Name);
            Assert.IsNotNull(footballPlayer.Name);
        }

        [TestCase("")]
        [TestCase(null)]

        public void FP_NameProperty_ThrowsException_WhenNullOrEmpty(string name)
        {
          ArgumentException ex = Assert.Throws<ArgumentException>(()
              => footballPlayer = new FootballPlayer(name, 11, "Forward"));

          Assert.AreEqual("Name cannot be null or empty!", ex.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(22)]

        public void FP_PlayerNumber_ThrowsException_WhenNumbersIsLessThan1AndMorethan21(int number)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => footballPlayer = new FootballPlayer("DanJames", number, "Forward"));

            Assert.AreEqual("Player number must be in range [1,21]", ex.Message);
        }

        [TestCase("Forwardd")]
        [TestCase("Defender")]
        
        public void FP_Position_ThrowsException_WhenNotValid(string position)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => footballPlayer = new FootballPlayer("Harry", 5, position));

            Assert.AreEqual("Invalid Position", ex.Message);
        }

        [Test]

        public void FP_ScoredGoalsMethod_WorksProperly()
        {
            footballPlayer.Score();
            Assert.AreEqual(1, footballPlayer.ScoredGoals);
        }


        [Test]

        public void FT_Constructor_WorksProperly()
        {
            Assert.NotNull(footballTeam);
            Assert.AreEqual("United", footballTeam.Name);
            Assert.AreEqual(15, footballTeam.Capacity);
            Assert.NotNull(footballTeam.Players);
        }

        [TestCase("")]
        [TestCase(null)]

        public void FT_NameProperty_ThrowsException_WhenNullOrEmpty(string name)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => footballTeam = new FootballTeam(name, 11));

            Assert.AreEqual("Name cannot be null or empty!", ex.Message);
        }


        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(14)]

        public void FT_Capacity_ThrowsException_WhenLessThan15(int number)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => footballTeam = new FootballTeam("United", number));

            Assert.AreEqual("Capacity min value = 15", ex.Message);
        }

        [Test]

        public void FT_AddNewPlayerMethod_IsWorkingProperly()
        {
            Assert.IsEmpty(footballTeam.Players);
            Assert.AreEqual($"Added player {footballPlayer.Name} in position {footballPlayer.Position} with number {footballPlayer.PlayerNumber}",
                footballTeam.AddNewPlayer(footballPlayer));
            Assert.AreEqual(1, footballTeam.Players.Count);
        }

        [Test]

        public void FT_AddNewPlayerMethod_DoesNotWork_WhenCapacityIsFull()
        {
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(footballPlayer);

            Assert.AreEqual("No more positions available!", footballTeam.AddNewPlayer(footballPlayer));
            Assert.AreEqual(15, footballTeam.Players.Count);
        }

        [Test]

        public void FT_PickPlayerMethod_IsWorkingProperly()
        {
            FootballPlayer player2 = new FootballPlayer("DeHea", 1, "Goalkeeper");
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(player2);

            Assert.AreSame(player2, footballTeam.PickPlayer("DeHea"));
        }

        [Test]

        public void FT_PlayerScoreMethod_IsWorkingProperly()
        {
            FootballPlayer player2 = new FootballPlayer("DeHea", 1, "Goalkeeper");
            footballTeam.AddNewPlayer(footballPlayer);
            footballTeam.AddNewPlayer(player2);
            Assert.AreEqual($"{footballPlayer.Name} scored and now has 1 for this season!", footballTeam.PlayerScore(7));
        }
    }
}