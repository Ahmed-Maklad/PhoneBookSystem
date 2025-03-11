namespace PhoneBook.DTOs.Shared;
public class EntityPaginated<T>
{
    public List<T>? Data { get; set; }
    public int Count { get; set; }
}
