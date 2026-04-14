namespace DungeonKnocker.Interfaces
{
    interface ICard
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string ImagePath { get; set; }
    }
}
