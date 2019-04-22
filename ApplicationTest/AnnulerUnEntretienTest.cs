using Application;
using Commun;
using Commun.Dto;
using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationTest
{
    public class AnnulerUnEntretienTest
    {
        IUseCase<Entretien> planifierUnEntretien;
        [SetUp]
        public void Setup()
        {
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
            planifierUnEntretien = new PlanifierUnEntretien(1, creneau, candidat, recruteur, salle);
        }

        [Test]
        public void Ne_peut_pas_annuler_un_entretien_non_existant()
        {
            IEnumerable<Entretien> entretiens = new List<Entretien>();
            AnnulerUnEntretien annulerUnEntretien = new AnnulerUnEntretien(1, "parce que j'en ai envie");
            Assert.Throws<EntretienNonExistantException>(() => annulerUnEntretien.Execute(entretiens.ToList()));
        }

        [Test]
        public void Annule_un_entretien()
        {
            IEnumerable<Entretien> entretiens = new List<Entretien>();
            entretiens = planifierUnEntretien.Execute(entretiens.ToList());
            AnnulerUnEntretien annulerUnEntretien = new AnnulerUnEntretien(1, "parce que j'en ai envie");
            entretiens = annulerUnEntretien.Execute(entretiens.ToList());

            Assert.That(entretiens.Count(), Is.EqualTo(1));
            Assert.That(entretiens.First().statut, Is.EqualTo(EntretienStatut.Annuler));
        }
    }
}
