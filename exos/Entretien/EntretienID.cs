namespace Model
{
    internal class EntretienID
    {
        public readonly int id;
        public EntretienID(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            if (obj is EntretienID e)
            {
                if (id == e.id)
                    return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}