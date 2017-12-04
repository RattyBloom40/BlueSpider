using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level {
    ArrayList crates = new ArrayList();
    int enemies, level;

    public ArrayList Crates {
        get {
            return crates;
        }
    }

    public int Enemies {
        get {
            return enemies;
        }
    }

    Level(ArrayList c, int level) {
        crates = c;
        this.level = level;
        this.enemies = (int)Random.Range(0, level);
    }

    public Level Next() {
        return new Level(Get(),level+1);
    }

    public static Level First() {
        return new Level(Get(), 0);
    }

    static ArrayList crateSpots = new ArrayList();

    public static void Init() {
        ArrayList t = new ArrayList();
        t.Add(new Vector2(-4.5f, .5f));
        t.Add(new Vector2(-4.5f, 1.5f));
        t.Add(new Vector2(-4.5f, -.5f));
        t.Add(new Vector2(4.5f, .5f));
        t.Add(new Vector2(4.5f, 1.5f));
        t.Add(new Vector2(4.5f, -.5f));
        crateSpots.Add(t);
    }

    static ArrayList Get() {
        return (ArrayList)(crateSpots.ToArray()[((int)Mathf.Round(Random.Range(0, crateSpots.Count - 1)))]);
    }
}
