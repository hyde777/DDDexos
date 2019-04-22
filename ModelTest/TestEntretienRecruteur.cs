using System;
using Commun;
using Model;
using NUnit.Framework;

namespace Tests.Entretiens
{
    public class TestEntretienRecruteur
    {
        private Creneau creneau;
        private EntretienStatut statut;
        private Candidat candidatCsharp;
        private Recruteur recruteurCsharpExperimenter;
        private Recruteur recruteurJava;
        private Salle salle1;
        private Salle salle2;

        [SetUp]
        public void Setup()
        {
            creneau = new Creneau(new DateTime(2019, 10, 10, 9, 0, 0), TimeSpan.FromHours(2));
            statut = EntretienStatut.Planifier;
            candidatCsharp = new Candidat("yoyo", Specialite.csharp, TimeSpan.FromDays(1000));
            recruteurCsharpExperimenter = new Recruteur("Arnaud", Specialite.csharp, TimeSpan.FromDays(20000));
            recruteurJava = new Recruteur("rahma", Specialite.java, TimeSpan.FromDays(300));
            salle1 = new Salle("Wagram", SalleStatut.Libre);
            salle2 = new Salle("Republique", SalleStatut.Libre);
        }

        [Test]
        public void Deux_Recruteur_peuvent_faire_passer_deux_entretien_dans_le_même_creneau()
        {
            Entretien entretien = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExperimenter,
                salle1);

            Entretien sut = new Entretien(
                3,
                creneau,
                statut,
                new Candidat("Hamza", Specialite.c, TimeSpan.FromDays(200)),
                new Recruteur("Robin", Specialite.c, TimeSpan.FromDays(700)),
                salle2);

            Assert.That(sut.PeutSePlanifierParRapport(entretien), Is.EqualTo(true));
        }

        [Test]
        public void Un_Recruteur_ne_peut_pas_passer_deux_entretien_en_même_temps()
        {
            Entretien entretien = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExperimenter,
                salle1);

            Entretien sut = new Entretien(
                3,
                creneau,
                statut,
                new Candidat("Leeroy", Specialite.csharp, TimeSpan.FromDays(2)),
                recruteurCsharpExperimenter,
                salle2);

            Assert.That(sut.PeutSePlanifierParRapport(entretien), Is.False);
        }

        [Test]
        public void Un_entretien_doit_être_planifier_entre_un_candidat_et_un_recruteur_de_même_specialite()
        {
            Assert.Throws<SpecialiteIncompatibleException>(() => new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurJava,
                salle1));
        }

        [Test]
        public void Un_recruteur_ne_peut_pas_faire_passer_entretien_où_il_a_moins_dexperience_que_le_candidat()
        {
            Recruteur recruteurCSharpSousExperimenter = new Recruteur("mickael", Specialite.csharp, TimeSpan.FromDays(700));
            Assert.Throws<RecruteurSousExperimenterException>(() => new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCSharpSousExperimenter,
                salle1));
        }

    }
}