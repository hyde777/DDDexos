using NUnit.Framework;
using System;
using exos;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Creneau_devrait_être_égual_au_même_créneau()
        {
            DateTime date = new DateTime(2020, 1, 10, 12, 00, 00);
            TimeSpan duréeDeCreneau = TimeSpan.FromHours(5);
            Creneau sut = new Creneau(date, duréeDeCreneau);

            Assert.That(sut, Is.EqualTo(new Creneau(date, duréeDeCreneau)));
        }

        [Test]
        public void Creneau_ne_devrait_pas_être_égual_a_un_créneau_différent()
        {
            DateTime date = new DateTime(2020, 1, 10, 12, 00, 00);
            TimeSpan duréeDeCreneau = TimeSpan.FromHours(7);
            TimeSpan autreDuréeDeCreneau = TimeSpan.FromHours(10);

            Creneau sut = new Creneau(date, duréeDeCreneau);

            Assert.That(sut, Is.Not.EqualTo(new Creneau(date, autreDuréeDeCreneau)));
        }

        [Test]
        public void Un_créneau_devrait_être_d_au_moins_1_heure()
        {
            DateTime date = new DateTime(2020, 1, 10, 12, 00, 00);
            TimeSpan duréeDeCreneau = TimeSpan.FromMinutes(10);

            Assert.Throws<DuréeMinimaleInvalideException>(() =>
                new Creneau(date, duréeDeCreneau)
            );
        }

        [Test]
        public void Un_créneau_devrait_commencer_a_pile_ou_30()
        {
            DateTime midiPile = new DateTime(2020, 1, 10, 12, 15, 00);
            TimeSpan duréeDeCreneau = TimeSpan.FromHours(5);

            Assert.Throws<HeureIncorrecteException>(() =>
                new Creneau(midiPile, duréeDeCreneau)
            );
        }

        [Test]
        public void Un_créneau_devrait_finir_a_pile_ou_30()
        {
            DateTime midiPile = new DateTime(2020, 1, 10, 12, 00, 00);
            TimeSpan duréeDeCreneau = TimeSpan.FromMinutes(95);

            Assert.Throws<HeureIncorrecteException>(() =>
                new Creneau(midiPile, duréeDeCreneau)
            );
        }


    }
}