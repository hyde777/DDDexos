using NUnit.Framework;
using System;
using Model;

namespace Tests
{
    public class TestCreneau
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Creneau_devrait_être_egual_au_même_creneau()
        {
            DateTime date = new DateTime(2020, 1, 10, 12, 00, 00);
            TimeSpan dureeDeCreneau = TimeSpan.FromHours(5);
            Creneau sut = new Creneau(date, dureeDeCreneau);

            Assert.That(sut, Is.EqualTo(new Creneau(date, dureeDeCreneau)));
        }

        [Test]
        public void Creneau_ne_devrait_pas_être_egual_a_un_creneau_different()
        {
            DateTime date = new DateTime(2020, 1, 10, 12, 00, 00);
            TimeSpan dureeDeCreneau = TimeSpan.FromHours(7);
            TimeSpan autreDureeDeCreneau = TimeSpan.FromHours(10);

            Creneau sut = new Creneau(date, dureeDeCreneau);

            Assert.That(sut, Is.Not.EqualTo(new Creneau(date, autreDureeDeCreneau)));
        }

        [Test]
        public void Un_creneau_devrait_être_d_au_moins_1_heure()
        {
            DateTime date = new DateTime(2020, 1, 10, 12, 00, 00);
            TimeSpan dureeDeCreneau = TimeSpan.FromMinutes(10);

            Assert.Throws<DureeMinimaleInvalideException>(() =>
                new Creneau(date, dureeDeCreneau)
            );
        }

        [Test]
        public void Un_creneau_devrait_commencer_a_pile_ou_30()
        {
            DateTime midiPile = new DateTime(2020, 1, 10, 12, 15, 00);
            TimeSpan dureeDeCreneau = TimeSpan.FromHours(5);

            Assert.Throws<HeureIncorrecteException>(() =>
                new Creneau(midiPile, dureeDeCreneau)
            );
        }

        [Test]
        public void Un_creneau_devrait_finir_a_pile_ou_30()
        {
            DateTime midiPile = new DateTime(2020, 1, 10, 12, 00, 00);
            TimeSpan dureeDeCreneau = TimeSpan.FromMinutes(95);

            Assert.Throws<HeureIncorrecteException>(() =>
                new Creneau(midiPile, dureeDeCreneau)
            );
        }


    }
}