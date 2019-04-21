using Application;
using Commun.Dto;
using Model;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using Commun;

namespace ApplicationTest
{
    public class ReplanifierUnEntretienTest
    {
        CréneauDto créneau2;
        IUseCase<Entretien> planifierUnEntretien;
        [SetUp]
        public void Setup()
        {
            créneau2 = new CréneauDto
            {
                date = new DateTime(2019, 10, 9, 12, 00, 00),
                durée = TimeSpan.FromHours(2)
            };

            CréneauDto créneau = new CréneauDto
            {
                date = new DateTime(2019, 10, 9, 10, 00, 00),
                durée = TimeSpan.FromHours(2)
            };

            CandidatDto candidat = new CandidatDto
            {
                expérience = TimeSpan.FromDays(500),
                name = "Willy",
                spécialité = Spécialité.csharp
            };

            RecruteurDto recruteur = new RecruteurDto
            {
                expérience = TimeSpan.FromDays(5000),
                name = "Yohan",
                spécialité = Spécialité.csharp
            };

            SalleDto salle = new SalleDto { name = "kilimanjaro", statut = SalleStatut.Libre };
            planifierUnEntretien = new PlanifierUnEntretien(1, créneau, candidat, recruteur, salle);
        }

        [Test]
        public void Ne_peut_pas_replanifier_un_entretien_non_existant()
        {
            IUseCase<Entretien> sut = new ReplanifierUnEntretien(1, créneau2);

            List<Entretien> entretiens = new List<Entretien>();

            Assert.Throws<EntretienNonExistantException>(() => sut.Execute(entretiens));
        }

        [Test]
        public void Peut_replanifier_un_entretien()
        {
            IUseCase<Entretien> sut = new ReplanifierUnEntretien(1, créneau2);

            IEnumerable<Entretien> entretiens = new List<Entretien>();

            entretiens = planifierUnEntretien.Execute(entretiens.ToList());
            entretiens = sut.Execute(entretiens.ToList());
            Assert.That(entretiens.Count(), Is.EqualTo(1));
            Assert.That(entretiens.First().statut, Is.EqualTo(EntretienStatut.Replanifier));
        }
    }
}
