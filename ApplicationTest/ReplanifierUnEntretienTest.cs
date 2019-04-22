using Application;
using Commun.Dto;
using Model;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Commun;
using Moq;

namespace ApplicationTest
{
    public class ReplanifierUnEntretienTest
    {
        CreneauDto creneau2;
        private Mock<IGenerateur<int>> genMock;
        IUseCase<Entretien> planifierUnEntretien;
        [SetUp]
        public void Setup()
        {
            creneau2 = new CreneauDto
            {
                date = new DateTime(2019, 10, 9, 12, 00, 00),
                duree = TimeSpan.FromHours(2)
            };

            CreneauDto creneau = new CreneauDto
            {
                date = new DateTime(2019, 10, 9, 10, 00, 00),
                duree = TimeSpan.FromHours(2)
            };

            CandidatDto candidat = new CandidatDto
            {
                experience = TimeSpan.FromDays(500),
                name = "Willy",
                specialite = Specialite.csharp
            };

            RecruteurDto recruteur = new RecruteurDto
            {
                experience = TimeSpan.FromDays(5000),
                name = "Yohan",
                specialite = Specialite.csharp
            };

            SalleDto salle = new SalleDto { name = "kilimanjaro", statut = SalleStatut.Libre };
            genMock = new Mock<IGenerateur<int>>();
            genMock.Setup(gen => gen.GetNewId()).Returns(1);
            planifierUnEntretien = new PlanifierUnEntretien(genMock.Object, creneau, candidat, recruteur, salle);
        }

        [Test]
        public void Ne_peut_pas_replanifier_un_entretien_non_existant()
        {
            IUseCase<Entretien> sut = new ReplanifierUnEntretien(genMock.Object, creneau2);

            List<Entretien> entretiens = new List<Entretien>();

            Assert.Throws<EntretienNonExistantException>(() => sut.Execute(entretiens));
        }

        [Test]
        public void Peut_replanifier_un_entretien()
        {
            IUseCase<Entretien> sut = new ReplanifierUnEntretien(genMock.Object, creneau2);

            IEnumerable<Entretien> entretiens = new List<Entretien>();

            entretiens = planifierUnEntretien.Execute(entretiens.ToList());
            entretiens = sut.Execute(entretiens.ToList());
            Assert.That(entretiens.Count(), Is.EqualTo(1));
            Assert.That(entretiens.First().statut, Is.EqualTo(EntretienStatut.Replanifier));
        }
    }
}
