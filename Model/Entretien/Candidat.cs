using System;
using Commun;

namespace Model
{
    internal class Candidat
    {
        public readonly string nom;
        internal readonly Specialite specialite;
        public readonly TimeSpan experience;

        internal Candidat(string nom, Specialite specialite, TimeSpan jourDexperience)
        {
            this.nom = nom;
            this.specialite = specialite;
            this.experience = jourDexperience;
        }

        public override bool Equals(object obj)
        {
            if (obj is Candidat c)
                if (c.nom == nom
                    && c.specialite == specialite
                    && c.experience == experience)
                    return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}