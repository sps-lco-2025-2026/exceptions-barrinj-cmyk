class InventoryException : Exception
{
    public InventoryException(string message) : base(message) { }
    public InventoryException(string message, Exception inner) : base(message, inner) { }
}

class ItemNotFoundException : InventoryException
{
    public string ItemName { get; }

    public ItemNotFoundException(string itemName) : base($"{itemName} does not exist in the inventory.")
    {
        ItemName = itemName;
    }
}

class InsufficientQuantityException : InventoryException
{
    public string ItemName  { get; }
    public int    Requested { get; }
    public int    Available { get; }

    public InsufficientQuantityException(string itemName, int requested, int available) : base($"Not enough {itemName}: requested {requested}, but only {available} available.")
    {
        ItemName  = itemName;
        Requested = requested;
        Available = available;
    }
}

class Inventory
{
    private Dictionary<string, int> _items;

    public Inventory(Dictionary<string, int> startingItems)
    {
        _items = new Dictionary<string, int>(startingItems, StringComparer.OrdinalIgnoreCase);
    }

    public void Take(string itemName, int quantity = 1)
    {
        if (!_items.ContainsKey(itemName))
            throw new ItemNotFoundException(itemName);

        if (_items[itemName] < quantity)
            throw new InsufficientQuantityException(itemName, quantity, _items[itemName]);

        _items[itemName] -= quantity;
    }

    public void Add(string itemName, int quantity = 1)
    {
        if (_items.ContainsKey(itemName))
            _items[itemName] += quantity;
        else
            _items[itemName] = quantity;
    }

    public void Display() //Used claude for the table/UI aspect
    {
        Console.WriteLine("\n── Inventory ───────────────────────");
        foreach (var entry in _items) Console.WriteLine($"  {entry.Key,-15} x{entry.Value}");
        Console.WriteLine("────────────────────────────────────\n");
    }
}

class Program
{
    static void Main()
    {
        var inventory = new Inventory(new Dictionary<string, int>
        {
            { "sword",  1 },
            { "potion", 3 },
            { "arrow", 12 },
            { "shield", 1 },
        });

        Console.WriteLine("Commands: take <item> [amount]  |  add <item> [amount]  |  list  |  quit\n");
        inventory.Display();

        while (true)
        {
            Console.Write("> ");
            string? line = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string command = tokens[0].ToLower();

            if (command == "quit") break;

            if (command == "list")
            {
                inventory.Display();
                continue;
            }

            if (command is "take" or "add")
            {
                if (tokens.Length < 2)
                {
                    Console.WriteLine("Usage: take <item> [amount]  or  add <item> [amount]\n");
                    continue;
                }

                string itemName = tokens[1];
                int quantity = 1;

                if (tokens.Length >= 3 && !int.TryParse(tokens[2], out quantity))
                {
                    Console.WriteLine($"'{tokens[2]}' is not a valid number.\n");
                    continue;
                }

                try
                {
                    if (command == "take")
                    {
                        inventory.Take(itemName, quantity);
                        Console.WriteLine($"  Took {quantity}x {itemName}.\n");
                    }
                    else
                    {
                        inventory.Add(itemName, quantity);
                        Console.WriteLine($"  Added {quantity}x {itemName}.\n");
                    }
                }
                catch (ItemNotFoundException ex)
                {
                    Console.WriteLine($"  [Not Found]       {ex.Message}\n");
                }
                catch (InsufficientQuantityException ex)
                {
                    Console.WriteLine($"  [Insufficient]    {ex.ItemName}: only {ex.Available} left, but you asked for {ex.Requested}.\n");
                }
                catch (InventoryException ex)
                {
                    Console.WriteLine($"  [Inventory Error] {ex.Message}\n");
                }

                continue;
            }

            Console.WriteLine($"Unknown command '{command}'.\n");
        }
    }
}