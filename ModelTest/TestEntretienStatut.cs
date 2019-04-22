using NUnit.Framework;
using System;
using Model;
using Commun;
using Commun.Dto;

namespace Tests.Entretiens
{
    public class TestEntretienStatut
    {
        private Creneau creneau;
        private EntretienStatut statut;
        private Candidat candidatCsharp;
        private Recruteur recruteurCsharpExperimenter;
        private Salle salle1;
        private Salle salle2;

        [SetUp]
        public void Setup()
        {
            creneau = new Creneau(new DateTime(2019, 10, 10, 9, 0, 0), TimeSpan.FromHours(2));
            statut = EntretienStatut.Planifier;
            candidatCsharp = new Candidat("yoyo", Specialite.csharp, TimeSpan.FromDays(1000));
            recruteurCsharpExperimenter = new Recruteur("Arnaud", Specialite.csharp, TimeSpan.FromDays(20000));
            salle1 = new Salle("Wagram", SalleStatut.Libre);
            salle2 = new Salle("Republique", SalleStatut.Libre);
        }

        [Test]
        public void Un_entretien_ne_devrait_pas_etre_egale_a_un_autre_entretien()
        {
            Entretien sut = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExperimenter,
                salle1);

            Entretien autreEntretien = new Entretien(
                4,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExperimenter,
                salle2);

            Assert.That(sut, Is.Not.EqualTo(autreEntretien));
        }

        [Test]
        public void Un_entretien_devrait_etre_confimer()
        {
            // When
            Entretien sut = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExperimenter,
                salle1);
            sut.Confirmer();

            // Then
            Assert.That(sut.statut, Is.EqualTo(EntretienStatut.Confirmer));
        }

        [Test]
        public void Un_entretien_devrait_etre_annuler()
        {
            Entretien sut = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExperimenter,
                salle1);
            sut.Annuler(new RaisonDto { raison = "Une raison d'annullation" });
            Assert.That(sut.statut, Is.EqualTo(EntretienStatut.Annuler));
        }

        [Test]
        public void Un_entretien_devrait_etre_replanifier()
        {
            Entretien sut = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExperimenter,
                salle1);
            sut.Replanifier(new CreneauDto { date = new DateTime(2020, 10, 10, 9, 0, 0), duree = TimeSpan.FromHours(2) });
            Assert.That(sut.statut, Is.EqualTo(EntretienStatut.Replanifier));
            Assert.That(sut.creneau, Is.Not.EqualTo(creneau));
        }
    }
}
