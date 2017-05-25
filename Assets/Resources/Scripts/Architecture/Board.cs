﻿using System;

public abstract class Board
{
    protected Player[,] ownerArray;
    protected bool isFull;

    /// <summary>
    /// Used for determining who owns this board
    /// </summary>
    public Player[,] OwnerArray { get { return ownerArray; } }
    public bool IsFull { get { return isFull; } }

    public event EventHandler<BoardEventArgs> OwnerArrayChanged;
    protected virtual void RaiseOwnerArrayChanged(BoardEventArgs e)
    {
        if(OwnerArrayChanged != null) { OwnerArrayChanged(this, e); }
    }

    public event EventHandler<BoardEventArgs> SpotEnabledChanged;
    protected virtual void RaiseSpotEnabledChanged(BoardEventArgs e)
    {
        if (SpotEnabledChanged != null) { SpotEnabledChanged(this, e); }
    }
    
    public Board()
    {
        ownerArray = new Player[3, 3];
        isFull = false;
    }

    /// <summary>
    /// Assign <paramref name="owner"/> to <paramref name="loc"/> of ownerArray
    /// </summary>
    /// <param name="loc"></param>
    /// <param name="owner"></param>
    protected void UpdateOwnerArray(Location loc, Player owner)
    {
        ownerArray[loc.Row, loc.Col] = owner;

        if (owner == null) { isFull = false; }
        else { CheckIsFull(); }

        RaiseOwnerArrayChanged(GetArgs());
    }

    /// <summary>
    /// Updates the isFull indicator
    /// If any element of ownerArray is null, isFull becomes false
    /// Otherwise is full becomes true
    /// </summary>
    protected void CheckIsFull()
    {
        foreach(Player p in ownerArray)
        {
            if(p == null)
            {
                isFull = false;
                return;
            }
        }
        isFull = true;
    }

    protected BoardEventArgs GetArgs()
    {
        return new BoardEventArgs();
    }

    /* THE OLD CODE
    SpotUI[,] spots;
    bool active;
    /// <summary>
    /// The 8 combos that win the game
    /// 2D array, 8x3
    /// </summary>
    static Location[,] winLines;
    /// <summary>
    /// Difference between board and pieces when enabled
    /// </summary>
    static Color offset = Color.gray * 1.2f; // very light offset

    /// <summary>
    /// Change in board when enabled/disabled
    /// </summary>
    static Color enabledOffset = Color.gray / 2; // darker offset

    internal GlobalGame game;

    public bool Active
    {
        get { return active; }
        set
        {
            if (active != value) // changing active status
            {
                Color color = GetComponent<Image>().color;

                if (Owner != null) // has a winner: simply augment color by an offset
                {
                    if (value) { color += enabledOffset; } // enabling makes lighter
                    else { color -= enabledOffset; } // disabling makes darker
                }
                else // no winner (could be tie or game not over)
                {
                    color = value ? game.EnabledColor : game.DisabledColor;
                }

                GetComponent<Image>().color = color;
                active = value;
            }
        }
    }

    /// <summary>
    /// The possible ownerships of spots and winner of the game
    /// </summary>
    public const int EMPTY = 0;

    /// <summary>
    /// The spots on this board
    /// </summary>
    public SpotUI[,] Spots { get { return spots; } }
    
    /// <summary>
    /// Whether this game is over
    /// </summary>
    public bool GameOver { get { return Owner != null || IsFull; } }

    /// <summary>
    /// Returns whether this board is full
    /// </summary>
    public virtual bool IsFull
    {
        get
        {
            foreach (SpotUI spot in spots)
            {
                if (spot.Owner == null) { return false; }
            }
            return true;
        }
    }
    
    /// <summary>
    /// The outline image child of this board
    /// </summary>
    public Image Outline {
        get {
            return transform.GetChild(0).GetComponent<Image>();
        }
    }

    // Use this for initialization
    void Start()
    {
        InitializeSpots();
        PopulateWinLines();
        active = true;
        game = GameObject.Find("Global Board").GetComponent<GlobalGame>();
    }

    internal virtual void InitializeSpots()
    {
        if(spots == null)
        {
            spots = new SpotUI[3, 3];
            foreach(SpotUI spot in GetComponentsInChildren<SpotUI>())
            {
                if(spot.transform.parent == transform) // direct children only
                {
                    spots[spot.Loc.Row, spot.Loc.Col] = spot;
                }
            }
        }
    }

    /// <summary>
    /// Overrides the value at spots[loc.Row, loc.Col] with player
    /// </summary>
    /// <param name="loc"></param>
    /// <param name="player"></param>
    public void FillSpot(Spot spot, Player player)
    {
        // whether the game was over before the move was made
        bool gameOverState = GameOver; 

        spot.TrySetOwner(player); // make the move

        if ((!GameOver && player != null) // game may have just ended
            || (GameOver && player == null)) // or game could have been re-opened
        {
            CheckWinner(); // update game over status
        }

        if (gameOverState != GameOver) // game over state changed
        {
            UpdateColor();
            game.CheckWinner();
            game.Active = !game.GameOver;
            game.UpdateColor();
        }
    }

    /// <summary>
    /// Update the color of the board to reflect the winner of the game
    /// </summary>
    internal void UpdateColor()
    {
        Image image = GetComponent<Image>();
        if(Owner != null) // game is over, has a winner
        {
            image.color = Owner.Color + offset; // reflect board winner
            if(active) { image.color += enabledOffset; } // reflect active status
        }
        else if(IsFull)
        { image.color = game.DisabledColor; } // tie game
        else
        {
            image.color = active ? game.EnabledColor : game.DisabledColor;
        }
    }

    public SpotUI Get(Location loc)
    {
        return spots[loc.Row, loc.Col];
    }

    /// <summary>
    /// Updates winner if someone has won the game
    /// </summary>
    void CheckWinner()
    {
        bool emptySpotExists = false;
        bool firstSpot = false;

        for (int line = 0; line < 8; line++)
        {
            Player p = null; // the player that has a chance of winning on this line
            firstSpot = true;
            for (int spot = 0; spot < 3; spot++)
            {
                Player player = Get(winLines[line, spot]).Owner;

                if (player == null)
                {
                    emptySpotExists = true;
                    break;
                }

                if (firstSpot)
                {
                    p = player;
                    firstSpot = false;
                }
                else if (p != player) { break; }

                if (spot == 2) // p has matched player all 3 spots
                {
                    owner = player; // player wins this board
                    return;
                }
            }
        }
        if (emptySpotExists) { owner = null; }
    }

    internal void PopulateWinLines()
    {
        if (winLines != null) { return; }
        winLines = new Location[8, 3]; // 8 lines of length 3

        // horizontal and vertical lines
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                winLines[i, j] = new Location(i, j);
                winLines[i + 3, j] = new Location(j, i);
            }
        }

        // diagonals
        for (int i = 0; i < 3; i++)
        {
            winLines[6, i] = new Location(i, i); // top-right to bottom-left
            winLines[7, i] = new Location(2 - i, i); // bottom-left to top-right
        }
    }
    
    public override bool CanBeOwnedBy(Player p)
    {
        if(Owner == p) { return true; }
        if(Owner != null) { return false; } // owned by someone else

        int lineCount = winLines.GetLength(0);
        int lineLength = winLines.GetLength(1);

        for (int r = 0; r < lineCount; r++)
        {
            // instantiate the line
            Location[] line = new Location[lineLength];
            for(int c = 0; c < lineLength; c++)
            {
                line[c] = winLines[r,c];
            }

            // find out if it can be owned
            bool canOwn = true;
            foreach(Location loc in line)
            {
                if(!Get(loc).CanBeOwnedBy(p))
                {
                    canOwn = false;
                    break;
                }
            }
            if(canOwn) { return true; }
        }
        return false;
    }
    //*/
}