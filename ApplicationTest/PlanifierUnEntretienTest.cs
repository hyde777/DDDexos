using Application;
using Model;
using NUnit.Framework;
using System.Collections.Generic;
using Commun;
using Commun.Dto;
using System;
using System.Linq;
using Moq;

namespace ApplicationTest
{
    public class PlanifierUnEntretienTest
    {
        CreneauDto creneau;
        CandidatDto candidat;
        RecruteurDto recruteur;
        SalleDto salle;
        private Mock<IGenerateur<int>> genMock;

        [SetUp]
        public void Setup()
        {
            creneau = new CreneauDto
            {
                date = new DateTime(2019, 10, 9, 10, 00, 00),
                duree = TimeSpan.FromHours(2)
            };

            candidat = new CandidatDto
            {
                experience = TimeSpan.FromDays(500),
                name = "Willy",
                specialite = Specialite.csharp
            };

            recruteur = new RecruteurDto
            {
                experience = TimeSpan.FromDays(5000),
                name = "Yohan",
                specialite = Specialite.csharp
            };

            salle = new SalleDto { name = "kilimanjaro", statut = SalleStatut.Libre };
            genMock = new Mock<IGenerateur<int>>();
            genMock.Setup(gen => gen.GetNewId()).Returns(1);
        }

        [Test]
        public void Devrait_ajouter_un_nouvel_entretien()
        {
           
            IUseCase<Entretien> planifierUnEntretien = new PlanifierUnEntretien(genMock.Object, creneau, candidat, recruteur, salle);

            IEnumerable<Entretien> entretiens = new List<Entretien>();
            IEnumerable<Entretien> sut = planifierUnEntretien.Execute(entretiens.ToList());

            Assert.That(sut.Count(), Is.EqualTo(1));
            Assert.That(sut.First(), Is.EqualTo(new Entretien(1, creneau, EntretienStatut.Planifier, candidat, recruteur, salle)));

        }

        [Test]
        public void Ne_devrait_pas_ajouter_un_nouvel_entretien_sil_est_dejà_present()
        {
            IUseCase<Entretien> planifierUnEntretien = new PlanifierUnEntretien(genMock.Object, creneau, candidat, recruteur, salle);
            IUseCase<Entretien> planifierUnEntretien2 = new PlanifierUnEntretien(genMock.Object, creneau, candidat, recruteur, salle);

            IEnumerable<Entretien> entretiens = new List<Entretien>();
            entretiens = planifierUnEntretien.Execute(entretiens.ToList());
            IEnumerable<Entretien> sut = planifierUnEntretien2.Execute(entretiens.ToList());

            Assert.That(sut.Count(), Is.EqualTo(1));
        }
    }
}