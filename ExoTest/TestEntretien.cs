using NUnit.Framework;
using System;
using exos;

namespace Tests
{
    public class TestEntretien
    {
        [Test]
        public void Un_entretien_ne_devrait_pas_etre_egale_a_un_autre_entretien()
        {
            Creneau creneau = new Creneau(new DateTime(2019, 10, 10, 9, 0, 0), TimeSpan.FromHours(2));
            EntretienStatut statut = EntretienStatut.Planifier;
            Candidat candidat = new Candidat();
            Recruteur recruteur = new Recruteur();
            Entretien sut = new Entretien(
                new EntretienID(2),
                creneau,
                statut,
                candidat,
                recruteur);

            Entretien autreEntretien = new Entretien(
                new EntretienID(4),
                creneau,
                statut,
                candidat,
                recruteur);

            Assert.That(sut, Is.Not.EqualTo(autreEntretien));
        }

        [Test]
        public void Un_entretien_devrait_etre_confimer()
        {
            // Given
            Creneau creneau = new Creneau(new DateTime(2019, 10, 10, 9, 0, 0), TimeSpan.FromHours(2));
            EntretienStatut statut = EntretienStatut.Planifier;
            Candidat candidat = new Candidat();
            Recruteur recruteur = new Recruteur();

            // When
            Entretien sut = new Entretien(
                new EntretienID(2),
                creneau,
                statut,
                candidat,
                recruteur);
            sut.confirmer();

            // Then
            Assert.That(sut.statut, Is.EqualTo(EntretienStatut.Confirmer));
        }

        [Test]
        public void Un_entretien_devrait_etre_annuler()
        {
            Creneau creneau = new Creneau(new DateTime(2019, 10, 10, 9, 0, 0), TimeSpan.FromHours(2));
            EntretienStatut statut = EntretienStatut.Planifier;
            Candidat candidat = new Candidat();
            Recruteur recruteur = new Recruteur();
            Raison raison = new Raison();
            Entretien sut = new Entretien(
                new EntretienID(2),
                creneau,
                statut,
                candidat,
                recruteur);
            sut.annuler(raison);
            Assert.That(sut.statut, Is.EqualTo(EntretienStatut.Annuler));
        }
    }
}
