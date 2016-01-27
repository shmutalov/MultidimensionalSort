namespace MultidimensionalSort.Models
{
    public class Member
    {
        public Member(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
