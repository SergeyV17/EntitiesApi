namespace Domain;

public class Entity
{
    public Entity()
    {
        Id = Guid.NewGuid();
        OperationDate = DateTime.Now;
    }
    
    public Guid Id { get; set; }
    public DateTime OperationDate { get; set; }
    public decimal Amount { get; set; }
}