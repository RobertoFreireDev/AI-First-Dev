namespace CLIAirpg.Core;

public class Inventory
{
    private List<Item> _items;
    public int MaxWeight { get; set; }

    public IReadOnlyList<Item> Items => _items.AsReadOnly();
    public int CurrentWeight => _items.Sum(i => i.Weight);
    public int RemainingWeight => MaxWeight - CurrentWeight;

    public Inventory(int maxWeight = 100)
    {
        _items = new List<Item>();
        MaxWeight = maxWeight;
    }

    public bool AddItem(Item item)
    {
        if (CurrentWeight + item.Weight <= MaxWeight)
        {
            _items.Add(item);
            return true;
        }
        return false;
    }

    public bool RemoveItem(string itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            _items.Remove(item);
            return true;
        }
        return false;
    }

    public Item? FindItem(string itemId)
    {
        return _items.FirstOrDefault(i => i.Id == itemId);
    }

    public void Clear()
    {
        _items.Clear();
    }
}
