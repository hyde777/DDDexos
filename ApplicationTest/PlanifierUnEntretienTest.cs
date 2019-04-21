using Application;
using Model;
using NUnit.Framework;
using System.Collections.Generic;
using Commun;
using Commun.Dto;
using System;
using System.Linq;

namespace ApplicationTest
{
    public class PlanifierUnEntretienTest
    {
        CréneauDto créneau;
        CandidatDto candidat;
        RecruteurDto recruteur;
        SalleDto salle;
        [SetUp]
        public void Setup()
        {
            créneau = new CréneauDto
            {
                date = new DateTime(2019, 10, 9, 10, 00, 00),
                durée = TimeSpan.FromHours(2)
            };

            candidat = new CandidatDto
            {
                expérience = TimeSpan.FromDays(500),
                name = "Willy",
                spécialité = Spécialité.csharp
            };

            recruteur = new RecruteurDto
            {
                expérience = TimeSpan.FromDays(5000),
                name = "Yohan",
                spécialité = Spécialité.csharp
            };

            salle = new SalleDto { name = "kilimanjaro", statut = SalleStatut.Libre };
        }

        [Test]
        public void Devrait_ajouter_un_nouvel_entretien()
        {
           
            IUseCase<Entretien> planifierUnEntretien = new PlanifierUnEntretien(1, créneau, candidat, recruteur, salle);

            IEnumerable<Entretien> entretiens = new List<Entretien>();
            IEnumerable<Entretien> sut = planifierUnEntretien.Execute(entretiens.ToList());

            Assert.That(sut.Count(), Is.EqualTo(1));
            Assert.That(sut.First(), Is.EqualTo(new Entretien(1, créneau, EntretienStatut.Planifier, candidat, recruteur, salle)));

        }

        [Test]
        public void Ne_devrait_pas_ajouter_un_nouvel_entretien_sil_est_déjà_présent()
        {
            IUseCase<Entretien> planifierUnEntretien = new PlanifierUnEntretien(1, créneau, candidat, recruteur, salle);
            IUseCase<Entretien> planifierUnEntretien2 = new PlanifierUnEntretien(1, créneau, candidat, recruteur, salle);

            IEnumerable<Entretien> entretiens = new List<Entretien>();
            entretiens = planifierUnEntretien.Execute(entretiens.ToList());
            IEnumerable<Entretien> sut = planifierUnEntretien2.Execute(entretiens.ToList());

            Assert.That(sut.Count(), Is.EqualTo(1));
        }
    }
}