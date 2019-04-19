using System;

namespace exos
{
    public class Recruteur
    {
        public readonly string nom;
        public readonly Spécialité spécialité;
        public readonly TimeSpan jourDexpérience;

        public Recruteur(string name, Spécialité spécialité, TimeSpan jourDexpérience)
        {
            this.nom = name;
            this.spécialité = spécialité;
            this.jourDexpérience = jourDexpérience;
        }

        public override bool Equals(object obj)
        {
            if(obj is Recruteur r)
                if (r.nom == nom 
                    && r.spécialité == spécialité
                    && r.jourDexpérience == jourDexpérience)
                    return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}