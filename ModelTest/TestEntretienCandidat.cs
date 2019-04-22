using System;
using System.Collections.Generic;
using System.Text;
using Commun;
using Model;
using NUnit.Framework;

namespace Tests.Entretiens
{
    class TestEntretienCandidat
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
        public void Deux_entretien_ne_doivent_pas_se_chevaucher_avec_un_même_candidat()
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
                candidatCsharp,
                new Recruteur("Felix", Specialite.csharp, TimeSpan.FromDays(1200)),
                salle2);

            Assert.That(sut.PeutSuivre(autreEntretien), Is.EqualTo(false));
            Assert.That(sut.PeutPreceder(autreEntretien), Is.EqualTo(false));
            Assert.That(sut.PeutSePlanifierParRapport(autreEntretien), Is.EqualTo(false));
        }

        [Test]
        public void Un_entretien_peut_suivre_un_autre_entretien_avec_un_même_candidat()
        {
            Entretien entretien = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExperimenter,
                salle1);

            Creneau justeAprèsLautreCreneau = new Creneau(entretien.creneau.fin, TimeSpan.FromHours(2));
            Entretien sut = new Entretien(
                3,
                justeAprèsLautreCreneau,
                statut,
                candidatCsharp,
                recruteurCsharpExperimenter,
                salle2);

            Assert.That(entretien.PeutSuivre(sut), Is.EqualTo(true));
        }

        [Test]
        public void Un_entretien_peut_precede_un_autre_entretien_avec_un_même_candidat()
        {
            Entretien entretien = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExperimenter,
                salle1);

            DateTime deuxheuresAvantEntretien = entretien.creneau.debut - TimeSpan.FromHours(2);
            Creneau justeAvantLautreCreneau = new Creneau(deuxheuresAvantEntretien, TimeSpan.FromHours(2));
            Entretien sut = new Entretien(
                3,
                justeAvantLautreCreneau,
                statut,
                candidatCsharp,
                recruteurCsharpExperimenter,
                salle2);

            Assert.That(entretien.PeutPreceder(sut), Is.EqualTo(true));
        }

    }
}
