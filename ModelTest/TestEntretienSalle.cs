using System;
using Commun;
using Model;
using NUnit.Framework;

namespace Tests.Entretiens
{
    public class TestEntretienSalle
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
        public void Deux_entretiens_du_même_creneau_ne_peuvent_pas_être_passer_dans_la_même_salle()
        {
            Entretien sut = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExperimenter,
                salle1);
            Entretien autreEntretien = new Entretien(
                3,
                creneau,
                statut,
                new Candidat("Louis", Specialite.java, TimeSpan.FromDays(500)),
                new Recruteur("Candice", Specialite.java, TimeSpan.FromDays(700)),
                salle1);

            Assert.That(sut.PeutSePlanifierParRapport(autreEntretien), Is.False);
        }

        [Test]
        public void Un_entretien_ne_peut_pas_être_planifier_dans_une_salle_occupe()
        {
            Salle salleOccuper = new Salle("Kilimanjaro", SalleStatut.Occupee);
            Assert.Throws<SalleOccuperException>(() => new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExperimenter,
                salleOccuper));
        }
    }
}