namespace AdventureGame;

public class Player
{
    private bool hasKey;
    private bool hasLamp;


    public Player() : this(false, false)
    {

    }
    public Player(bool hasKey, bool hasLamp)
    {
        this.hasKey = hasKey;
        this.hasLamp = hasLamp;
    }

    public bool HasKey() { return hasKey; }
    public bool HasLamp() { return hasLamp; }

    public void SetHasKey(bool f) { hasKey = f; }
    public void SetHasLamp(bool f) { hasLamp = f; }

}
 