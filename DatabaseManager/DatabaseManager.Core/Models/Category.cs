namespace DatabaseManager.Core.Models
{
    public class Category
    {
        public Category(int id, string name)
            :this(id, name, null)
        {
        }

        public Category(int id, string name, Category parent)
        {
            this.Id = id;
            this.Name = name;
            this.Parent = parent;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public Category Parent { get; set; }
    }
}
