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
        public void Creneau_devrait_�tre_�gual_au_m�me_cr�neau()
        {
            DateTime date = new DateTime(2020, 1, 10, 12, 00, 00);
            TimeSpan dur�eDeCreneau = TimeSpan.FromHours(5);
            Creneau sut = new Creneau(date, dur�eDeCreneau);

            Assert.That(sut, Is.EqualTo(new Creneau(date, dur�eDeCreneau)));
        }

        [Test]
        public void Creneau_ne_devrait_pas_�tre_�gual_a_un_cr�neau_diff�rent()
        {
            DateTime date = new DateTime(2020, 1, 10, 12, 00, 00);
            TimeSpan dur�eDeCreneau = TimeSpan.FromHours(7);
            TimeSpan autreDur�eDeCreneau = TimeSpan.FromHours(10);

            Creneau sut = new Creneau(date, dur�eDeCreneau);

            Assert.That(sut, Is.Not.EqualTo(new Creneau(date, autreDur�eDeCreneau)));
        }

        [Test]
        public void Un_cr�neau_devrait_�tre_d_au_moins_1_heure()
        {
            DateTime date = new DateTime(2020, 1, 10, 12, 00, 00);
            TimeSpan dur�eDeCreneau = TimeSpan.FromMinutes(10);

            Assert.Throws<Dur�eMinimaleInvalideException>(() =>
                new Creneau(date, dur�eDeCreneau)
            );
        }

        [Test]
        public void Un_cr�neau_devrait_commencer_a_pile_ou_a_30()
        {
            DateTime midiPile = new DateTime(2020, 1, 10, 12, 00, 00);
            TimeSpan dur�eDeCreneau = TimeSpan.FromHours(5);

            Assert.Throws<HeureIncorrecteException>(() =>
                new Creneau(midiPile, dur�eDeCreneau)
            );
        }

    }
}