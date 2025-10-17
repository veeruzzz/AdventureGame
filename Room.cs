namespace AdventureGame;

public class Room
{
    private string description;
    private bool isLit;
    private bool hasKey;
    private bool hasLamp;
    private bool hasChest;
    private bool hasGrue;
    private Room? north;
    private Room? south;
    private Room? west;
    private Room? east;

    public Room() : this("N/A", false, false, false, false, false)
    {
    }
    public Room(string desc, bool il, bool hk, bool hl, bool hc, bool hg,
                Room? n = null, Room? s = null, Room? w = null, Room? e = null)
    {
        SetDescription(desc);
        SetLit(il);
        SetKey(hk);
        SetLamp(hl);
        SetChest(hc);
        SetGrue(hg);
        SetNorth(n);
        SetSouth(s);
        SetWest(w);
        SetEast(e);
    }


    public string GetDescription()
    {
        return description;
    }
    public bool IsLit() { return isLit; }
    public bool HasKey() { return hasKey; }
    public bool HasLamp() { return hasLamp; }
    public bool HasChest() { return hasChest; }
    public bool HasGrue() { return hasGrue; }
    public Room? North() { return north; }
    public Room? South() { return south; }
    public Room? West() { return west; }
    public Room? East() { return east; }

    public void SetDescription(string desc) { description = desc; }
    public void SetLit(bool b) { isLit = b; }
    public void SetLamp(bool b) { hasLamp = b; }
    public void SetChest(bool b) { hasChest = b; }
    public void SetKey(bool b) { hasKey = b; }
    public void SetGrue(bool b) { hasGrue = b; }
    public void SetNorth(Room? n) { north = n; }
    public void SetSouth(Room? n) { south = n; }
    public void SetWest(Room? n) { west = n; }
    public void SetEast(Room? n) { east = n; }
    public void ShowDescription(bool playerHasLamp)
    {
        if (isLit || playerHasLamp)
        {
            Console.WriteLine(description);
            if (hasLamp) Console.WriteLine("There is a lamp here.");
            if (hasKey) Console.WriteLine("There is a key here.");
            if (hasChest) Console.WriteLine("There is a locked treasure chest here.");
        }
        else
        {
            Console.WriteLine("It's pitch black! You cannot see anything.");
        }
    }
}