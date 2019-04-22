using System;
using Commun;

namespace Model
{
    internal class Recruteur
    {
        public readonly string nom;
        internal readonly Specialite specialite;
        public readonly TimeSpan experience;

        internal Recruteur(string name, Specialite specialite, TimeSpan jourDexperience)
        {
            this.nom = name;
            this.specialite = specialite;
            this.experience = jourDexperience;
        }

        public override bool Equals(object obj)
        {
            if(obj is Recruteur r)
                if (r.nom == nom 
                    && r.specialite == specialite
                    && r.experience == experience)
                    return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}