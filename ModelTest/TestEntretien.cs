using NUnit.Framework;
using System;
using Model;
using Commun;
using Commun.Dto;

namespace Tests
{
    public class TestEntretien
    {
        private Creneau creneau;
        private EntretienStatut statut;
        private Candidat candidatCsharp;
        private Recruteur recruteurCsharpExpérimenter;
        private Recruteur recruteurJava;
        private Salle salle1;
        private Salle salle2;

        [SetUp]
        public void Setup()
        {
            creneau = new Creneau(new DateTime(2019, 10, 10, 9, 0, 0), TimeSpan.FromHours(2));
            statut = EntretienStatut.Planifier;
            candidatCsharp = new Candidat("yoyo", Spécialité.csharp, TimeSpan.FromDays(1000));
            recruteurCsharpExpérimenter = new Recruteur("Arnaud", Spécialité.csharp, TimeSpan.FromDays(20000));
            recruteurJava = new Recruteur("rahma", Spécialité.java, TimeSpan.FromDays(300));
            salle1 = new Salle("Wagram", SalleStatut.Libre);
            salle2 = new Salle("République", SalleStatut.Libre);
        }

        [Test]
        public void Un_entretien_ne_devrait_pas_etre_egale_a_un_autre_entretien()
        {
            Entretien sut = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExpérimenter,
                salle1);

            Entretien autreEntretien = new Entretien(
                4,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExpérimenter,
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
                recruteurCsharpExpérimenter,
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
                recruteurCsharpExpérimenter,
                salle1);
            sut.Annuler("Une raison d'annullation");
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
                recruteurCsharpExpérimenter,
                salle1);
            sut.Replanifier(new CréneauDto { date = new DateTime(2020, 10, 10, 9, 0, 0), durée = TimeSpan.FromHours(2) });
            Assert.That(sut.statut, Is.EqualTo(EntretienStatut.Replanifier));
            Assert.That(sut.créneau, Is.Not.EqualTo(creneau));
        }

        [Test]
        public void Deux_entretien_ne_doivent_pas_se_chevaucher_avec_un_même_candidat()
        {
            Entretien sut = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExpérimenter,
                salle1);

            Entretien autreEntretien = new Entretien(
                3,
                creneau,
                statut,
                candidatCsharp,
                new Recruteur("Felix", Spécialité.csharp, TimeSpan.FromDays(1200)),
                salle2);

            Assert.That(sut.PeutSuivre(autreEntretien), Is.EqualTo(false));
            Assert.That(sut.PeutPrécéder(autreEntretien), Is.EqualTo(false));
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
                recruteurCsharpExpérimenter,
                salle1);

            Creneau justeAprèsLautreCréneau = new Creneau(entretien.créneau.fin, TimeSpan.FromHours(2));
            Entretien sut = new Entretien(
                3,
                justeAprèsLautreCréneau,
                statut,
                candidatCsharp,
                recruteurCsharpExpérimenter,
                salle2);

            Assert.That(entretien.PeutSuivre(sut), Is.EqualTo(true));
        }

        [Test]
        public void Un_entretien_peut_précédé_un_autre_entretien_avec_un_même_candidat()
        {
            Entretien entretien = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExpérimenter,
                salle1);

            DateTime deuxheuresAvantEntretien = entretien.créneau.début - TimeSpan.FromHours(2);
            Creneau justeAvantLautreCréneau = new Creneau(deuxheuresAvantEntretien, TimeSpan.FromHours(2));
            Entretien sut = new Entretien(
                3,
                justeAvantLautreCréneau,
                statut,
                candidatCsharp,
                recruteurCsharpExpérimenter,
                salle2);

            Assert.That(entretien.PeutPrécéder(sut), Is.EqualTo(true));
        }

        [Test]
        public void Deux_Recruteur_peuvent_faire_passer_deux_entretien_dans_le_même_créneau()
        {
            Entretien entretien = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExpérimenter,
                salle1);

            Entretien sut = new Entretien(
                3,
                creneau,
                statut,
                new Candidat("Hamza", Spécialité.c, TimeSpan.FromDays(200)),
                new Recruteur("Robin", Spécialité.c, TimeSpan.FromDays(700)),
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
                recruteurCsharpExpérimenter,
                salle1);

            Entretien sut = new Entretien(
                3,
                creneau,
                statut,
                new Candidat("Leeroy", Spécialité.csharp, TimeSpan.FromDays(2)),
                recruteurCsharpExpérimenter,
                salle2);

            Assert.That(sut.PeutSePlanifierParRapport(entretien), Is.False);
        }

        [Test]
        public void Un_entretien_doit_être_planifier_entre_un_candidat_et_un_recruteur_de_même_spécialité()
        {
            Assert.Throws<SpécialitéIncompatibleException>(() => new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurJava,
                salle1));
        }

        [Test]
        public void Un_recruteur_ne_peut_pas_faire_passer_entretien_où_il_a_moins_dexpérience_que_le_candidat()
        {
            Recruteur recruteurCSharpSousExpérimenter = new Recruteur("mickael", Spécialité.csharp, TimeSpan.FromDays(700));
            Assert.Throws<RecruteurSousExpérimenterException>(() => new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCSharpSousExpérimenter,
                salle1));
        }


        [Test]
        public void Deux_entretiens_du_même_créneau_ne_peuvent_pas_être_passer_dans_la_même_salle()
        {
            Entretien sut = new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExpérimenter,
                salle1);
            Entretien autreEntretien = new Entretien(
                3,
                creneau,
                statut,
                new Candidat("Louis", Spécialité.java, TimeSpan.FromDays(500)),
                new Recruteur("Candice", Spécialité.java, TimeSpan.FromDays(700)),
                salle1);

            Assert.That(sut.PeutSePlanifierParRapport(autreEntretien), Is.False);
        }

        [Test]
        public void Un_entretien_ne_peut_pas_être_planifier_dans_une_salle_occupé()
        {
            Salle salleOccuper = new Salle("Kilimanjaro", SalleStatut.Occupée);
            Assert.Throws<SalleOccuperException>(() => new Entretien(
                2,
                creneau,
                statut,
                candidatCsharp,
                recruteurCsharpExpérimenter,
                salleOccuper));
        }
    }
}
