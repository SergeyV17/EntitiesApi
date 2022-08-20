namespace Api.Dtos;

public class EntityGetDto
{
    public Guid Id { get; set; }
    public DateTime OperationDate { get; set; }
    public decimal Amount { get; set; }
}