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
            int[] minute =new int[] { 00, 30 };
            if (!minute.Contains(date.Minute))
                throw new HeureIncorrecteException();
            if (date.Second != 00 && date.Millisecond != 00)
                throw new HeureIncorrecteException();
            this.début = date;
            if (durée < TimeSpan.FromHours(1))
                throw new DuréeMinimaleInvalideException();
            this.fin = date.Add(durée);
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
    }
}
