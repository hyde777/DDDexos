using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace exos
{
    public class Creneau
    {
        public readonly DateTime début;
        public readonly DateTime fin;

        public Creneau(DateTime date, TimeSpan durée)
        {
            this.début = date;
            if (!Enum.IsDefined(typeof(AcceptableMinutes), this.début.Minute))
                throw new HeureIncorrecteException();
            if (durée < TimeSpan.FromHours(1))
                throw new DuréeMinimaleInvalideException();
            this.fin = date.Add(durée);
            if (!Enum.IsDefined(typeof(AcceptableMinutes), this.fin.Minute))
                throw new HeureIncorrecteException();
        }

        public override bool Equals(object obj)
        {
            if (obj is Creneau cr)
            {
                if(cr.début == début
                    && cr.fin == fin)
                {
                    return true;
                }
            }
            return false; 
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
