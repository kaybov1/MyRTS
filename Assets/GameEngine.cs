using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {

    public void Map map = new Map();

    private Map map;
    private System.Timers.Timer time = new System.Timers.Timer();
    private int ticks = 0;
    private const int REFRESH = 60;
    private int gameTime = 0;
    public int units;
    public int buildings;
    public int width;
    public int height;

    // Use this for initialization
    void Start () {
        int newXPos, newYPos;
        Unit closest;

        //update units on map
        map.checkHealth();

        //combat    
        for (int j = 0; j < map.UnitsOnMap.Count; j++)
        {

            //Limits of the Game
            if (!map.UnitsOnMap[j].Attack)
            {
                closest = map.UnitsOnMap[j].NearestUnit(map.UnitsOnMap);
                if (map.UnitsOnMap[j].X < closest.X)
                    newXPos = map.UnitsOnMap[j].X + 1;
                else if (map.UnitsOnMap[j].X > closest.X)
                    newXPos = map.UnitsOnMap[j].X - 1;
                else
                    newXPos = map.UnitsOnMap[j].X;

                if (map.UnitsOnMap[j].Y < closest.Y)
                    newYPos = map.UnitsOnMap[j].Y + 1;
                else if (map.UnitsOnMap[j].Y > closest.Y)
                    newYPos = map.UnitsOnMap[j].Y - 1;
                else
                    newYPos = map.UnitsOnMap[j].Y;
                map.update(map.UnitsOnMap[j], newXPos, newYPos);
            }

            if (map.UnitsOnMap[j].Attack)
            {
                for (int i = 0; i < map.UnitsOnMap.Count; i++)
                {
                    if (map.UnitsOnMap[j].Faction != map.UnitsOnMap[i].Faction)
                        map.UnitsOnMap[j].combat(map.UnitsOnMap[i]);
                }
            }

            if (!map.UnitsOnMap[j].Attack)
            {
                for (int i = 0; i < map.UnitsOnMap.Count; i++)
                {
                    if ((map.UnitsOnMap[j].Faction != map.UnitsOnMap[i].Faction) && (map.UnitsOnMap[j].Faction != map.UnitsOnMap[i].Faction))
                        map.UnitsOnMap[j].Attack = true;
                }
            }

            if (map.UnitsOnMap[j].Health < 25)
            {
                newXPos = map.UnitsOnMap[j].X + 1;
                newYPos = map.UnitsOnMap[j].Y - 1;
                map.update(map.UnitsOnMap[j], newXPos, newYPos);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Instantiate();

        gameTime++;
        if (gameTime % REFRESH == 0)
        {
            playGame();
            redraw();
            //Instantiate(Resources.Load("resourceplus"));
        }

	}
}
