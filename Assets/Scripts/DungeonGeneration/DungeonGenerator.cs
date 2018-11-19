using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    Tilemap map;

    [SerializeField]
    CustomRuleTile ground;

    [SerializeField]
    CustomRuleTile wall;

    [SerializeField]
    CustomRuleTile water;

    public List<DungeonFeature> features;
    public List<DungeonFeature> corridors;
    public List<DungeonFeature> fishingSpots;

    public int mapSizeX = 25;
    public int mapSizeY = 25;

    public int maxFeatures = 20;
    public int corridorChance = 10;

    public int nbFloors = 10;
    public int currentFloor = 0;
    int fishFloor = 0;
    public List<Tilemap> maps;
    public List<TilemapRenderer> mapRenderers;

    // Use this for initialization
    void Start()
    {
        LoadFeatures();
        ResetMap();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetMap();
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            ChangeFloor(true);
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            ChangeFloor(false);
        }
    }

    void ChangeFloor(bool up)
    {
        if (up && currentFloor < nbFloors - 1)
        {
            mapRenderers[currentFloor].enabled = false;
            currentFloor++;
            mapRenderers[currentFloor].enabled = true;
            //Debug.Log("Floor : " + currentFloor);
        }
        else if (!up && currentFloor > 0)
        {
            mapRenderers[currentFloor].enabled = false;
            currentFloor--;
            mapRenderers[currentFloor].enabled = true;
            //Debug.Log("Floor : " + currentFloor);
        }
    }

    void LoadFeatures()
    {
        foreach (DungeonFeature feature in features)
        {
            feature.text = (TextAsset)Resources.Load("DungeonFeatures/" + feature.path, typeof(TextAsset));
            if (null != feature.text)
            {
                //string content = feature.text.text;                
                //Debug.Log(content);
            }
        }

        foreach (DungeonFeature feature in fishingSpots)
        {
            feature.text = (TextAsset)Resources.Load("DungeonFeatures/" + feature.path, typeof(TextAsset));
            if (null != feature.text)
            {
                //string content = feature.text.text;                
                //Debug.Log(content);
            }
        }

        foreach (DungeonFeature feature in corridors)
        {
            feature.text = (TextAsset)Resources.Load("DungeonFeatures/" + feature.path, typeof(TextAsset));
            if (null != feature.text)
            {
                //string content = feature.text.text;                
                //Debug.Log(content);
            }
        }
    }

    void ResetMap()
    {
        DestroyMaps();
        Generate();

    }

    void DestroyMaps()
    {
        var children = new List<GameObject>();
        foreach (Transform child in transform) children.Add(child.gameObject);
        children.ForEach(child => Destroy(child));
        maps.Clear();
        mapRenderers.Clear();
    }

    void Generate()
    {
        int attempt = 0;

        for (int i = 0; i < nbFloors; i++)
        {
            GameObject tempObject = new GameObject(i.ToString());
            tempObject.transform.parent = gameObject.transform;
            map = tempObject.AddComponent<Tilemap>();
            maps.Add(map);
            TilemapRenderer tempRenderer = tempObject.AddComponent<TilemapRenderer>();
            mapRenderers.Add(tempRenderer);
        }

        for (int j = 0; j < nbFloors; j++)
        {

            map = maps[j];
            FillMap(wall);

            bool succes = GenerateFloor(j);
            if (succes)
            {
                mapRenderers[j].enabled = false;
                attempt = 0;
            }
            else
            {
                Debug.Log("Floor " + j + " attempt " + attempt);
                attempt++;
                j--;
            }

            if (attempt > 100)
            {
                j = nbFloors;
            }


        }

        mapRenderers[currentFloor].enabled = true;
    }

    bool GenerateFloor(int floor)
    {

        if (floor == 0)
        {
            fishFloor = Random.Range(0, 2);
            //fishFloor = 0;
        }

        //Debug.Log("Fish floor : " + fishFloor);



        if (fishFloor == floor)
        {
            bool stop = false;
            while (!stop)
            {
                Vector3Int newPosition = new Vector3Int(Random.Range(-mapSizeX + 5, mapSizeX - 5), Random.Range(-mapSizeY + 5, mapSizeY - 5), 0);
                int index = Random.Range(0, fishingSpots.Count);
                bool inverseX;
                bool inverseY;
                if (CanPlaceFeature(newPosition, fishingSpots[index], out inverseX, out inverseY))
                {
                    //Debug.Log("floor " + j + " " + inverseX + " " + inverseY);
                    PlaceFeature(newPosition, fishingSpots[index], true, inverseY, false);
                    stop = true;
                }
            }

            fishFloor += 3 + Random.Range(0, 6);
            if (fishFloor >= nbFloors && floor < nbFloors - 1)
            {
                fishFloor = nbFloors - 1;
            }
            //fishFloor++;

            //Debug.Log("Fish floor : " + fishFloor);
        }
        else
        {
            PlaceFeature(new Vector3Int(0, 0, 0), features[0], false, false, false);
        }

        for (int i = 0; i < maxFeatures; i++)
        {
            int nbTryMax = 100000;
            int nbTry = 0;
            bool stop = false;

            while (!stop && nbTry < nbTryMax)
            {
                Vector3Int newPosition = new Vector3Int(Random.Range(-mapSizeX, mapSizeX), Random.Range(-mapSizeY, mapSizeY), 0);
                if (map.GetTile(newPosition) == wall && ValidPos(newPosition))
                {
                    int index = 0;
                    int type = Random.Range(0, corridorChance);
                    bool inverseX;
                    bool inverseY;

                    if (type == 0)
                    {
                        index = Random.Range(0, corridors.Count);

                        if (CanPlaceFeature(newPosition, corridors[index], out inverseX, out inverseY))
                        {
                            PlaceFeature(newPosition, corridors[index], inverseX, inverseY);
                            nbTry = -1;
                            stop = true;
                        }
                    }
                    else
                    {
                        index = Random.Range(0, features.Count);

                        if (CanPlaceFeature(newPosition, features[index], out inverseX, out inverseY))
                        {
                            PlaceFeature(newPosition, features[index], inverseX, inverseY);
                            nbTry = -1;
                            stop = true;
                        }
                    }



                }
                nbTry++;
            }

            if (nbTry >= nbTryMax)
            {
                if (i <= 5)
                {
                    return false;
                }
                i = 100;
            }

        }



        return true;
    }

    bool CanPlaceFeature(Vector3Int startPos, DungeonFeature feature, out bool inverseX, out bool inverseY)
    {
        bool canPlace = true;

        int startPosX = startPos.x - feature.offset.x - 1;
        int startPosY = startPos.y - feature.offset.y - 1;

        int endPosX = startPosX + feature.size.x + 1;
        int endPosY = startPosY + feature.size.y + 1;

        //if (feature.offset.y == 0)
        //{
        //    startPosY -= 1;
        //    endPosY += 1;
        //}

        //if (feature.offset.x == 0)
        //{
        //    startPosX -= 1;
        //    endPosX += 1;
        //}

        for (int i = startPosX; i <= endPosX; i++)
        {
            for (int j = startPosY; j <= endPosY; j++)
            {
                if (i >= mapSizeX - 1 || j >= mapSizeY - 1 || map.GetTile(new Vector3Int(i, j, 0)) != wall)
                {
                    canPlace = false;
                    j = mapSizeY + 100;
                    i = mapSizeX + 100;
                }
            }
        }

        if (canPlace == true)
        {
            inverseX = false;
            inverseY = false;

            return canPlace;
        }



        //inverse X ?
        canPlace = true;
        startPosX = startPos.x + feature.offset.x + 1;

        endPosX = startPosX - feature.size.x - 1;

        //if (feature.offset.x == 0)
        //{
        //    startPosX += 1;
        //    endPosX -= 1;
        //}

        for (int i = startPosX; i >= endPosX; i--)
        {
            for (int j = startPosY; j <= endPosY; j++)
            {
                if (i <= -mapSizeX || j >= mapSizeY - 1 || map.GetTile(new Vector3Int(i, j, 0)) != wall)
                {
                    canPlace = false;
                }
            }
        }

        if (canPlace == true)
        {
            inverseX = true;
            inverseY = false;
            return canPlace;
        }

        //inverse Y ?

        canPlace = true;
        startPosX = startPos.x - feature.offset.x - 1;
        startPosY = startPos.y + feature.offset.y + 1;

        endPosX = startPosX + feature.size.x + 1;
        endPosY = startPosY - feature.size.y - 1;

        //if (feature.offset.y == 0)
        //{
        //    startPosY += 1;
        //    endPosY -= 1;
        //}

        //if (feature.offset.x == 0)
        //{
        //    startPosX -= 1;
        //    endPosX += 1;
        //}

        for (int i = startPosX; i <= endPosX; i++)
        {
            for (int j = startPosY; j >= endPosY; j--)
            {
                if (i >= mapSizeX - 1 || j <= -mapSizeY || map.GetTile(new Vector3Int(i, j, 0)) != wall)
                {
                    canPlace = false;
                }
            }
        }

        if (canPlace == true)
        {
            inverseX = false;
            inverseY = true;
            return canPlace;
        }


        //inverse X et Y ?

        canPlace = true;
        startPosX = startPos.x + feature.offset.x + 1;
        startPosY = startPos.y + feature.offset.y + 1;

        endPosX = startPosX - feature.size.x - 1;
        endPosY = startPosY - feature.size.y - 1;

        //if (feature.offset.y == 0)
        //{
        //    startPosY += 1;
        //    endPosY -= 1;
        //}

        //if (feature.offset.x == 0)
        //{
        //    startPosX += 1;
        //    endPosX -= 1;
        //}

        for (int i = startPosX; i >= endPosX; i--)
        {
            for (int j = startPosY; j >= endPosY; j--)
            {
                if (i <= -mapSizeX || j <= -mapSizeY || map.GetTile(new Vector3Int(i, j, 0)) != wall)
                {
                    canPlace = false;
                }
            }
        }

        if (canPlace == true)
        {
            inverseX = true;
            inverseY = true;
            return canPlace;
        }

        inverseX = false;
        inverseY = false;
        return canPlace;
    }

    bool ValidPos(Vector3Int position)
    {
        bool valid = true;
        int nbGround = 0;

        if (map.GetTile(position - new Vector3Int(1, 0, 0)) == ground)
        {
            nbGround++;
        }
        if (map.GetTile(position + new Vector3Int(1, 0, 0)) == ground)
        {
            nbGround++;
        }
        if (map.GetTile(position - new Vector3Int(0, 1, 0)) == ground)
        {
            nbGround++;
        }
        if (map.GetTile(position + new Vector3Int(0, 1, 0)) == ground)
        {
            nbGround++;
        }

        if (nbGround == 1)
        {
            valid = true;
        }
        else
        {
            valid = false;
        }

        return valid;
    }

    void PlaceFeature(Vector3Int position, DungeonFeature feature, bool inverseX, bool inverseY, bool placeDoor = true, int doorSize = 1)
    {
        int startPosX = position.x;
        int startPosY = position.y;

        //*
        if (placeDoor)
        {
            Vector3Int pos = new Vector3Int();

            for (int i = 0; i < doorSize; i++)
            {
                pos.x = startPosX;
                pos.y = startPosY;
                map.SetTile(pos, ground);
                /*
                map.SetTileFlags(pos, TileFlags.None);
                map.SetColor(pos, Color.red);
                //*/


                
                

            }


            /*
            map.SetTileFlags(pos, TileFlags.None);
            map.SetColor(pos, Color.red);
            //*/
        }
        if (!inverseX && !inverseY)
        {
            startPosX -= feature.offset.x;
            startPosY -= feature.offset.y;
            if (null != feature.text)
            {
                int i = startPosX;
                int j = startPosY;

                foreach (char c in feature.text.text)
                {
                    switch (c)
                    {
                        case '0':
                            map.SetTile(new Vector3Int(i, j, 0), ground);
                            i++;
                            break;
                        case '1':
                            map.SetTile(new Vector3Int(i, j, 0), wall);
                            i++;
                            break;
                        case '2':
                            map.SetTile(new Vector3Int(i, j, 0), water);
                            i++;
                            break;
                        case 'x':
                            j++;
                            i = startPosX;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                for (int i = startPosX; i < startPosX + feature.size.x; i++)
                {
                    for (int j = startPosY; j < startPosY + feature.size.y; j++)
                    {
                        map.SetTile(new Vector3Int(i, j, 0), ground);
                    }
                }
            }

        }
        else if (inverseX && !inverseY)
        {
            startPosX += feature.offset.x;
            startPosY -= feature.offset.y;

            if (null != feature.text)
            {
                int i = startPosX;
                int j = startPosY;

                foreach (char c in feature.text.text)
                {
                    switch (c)
                    {
                        case '0':
                            map.SetTile(new Vector3Int(i, j, 0), ground);
                            i--;
                            break;
                        case '1':
                            map.SetTile(new Vector3Int(i, j, 0), wall);
                            i--;
                            break;
                        case '2':
                            map.SetTile(new Vector3Int(i, j, 0), water);
                            i--;
                            break;
                        case 'x':
                            j++;
                            i = startPosX;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                for (int i = startPosX; i > startPosX - feature.size.x; i--)
                {
                    for (int j = startPosY; j < startPosY + feature.size.y; j++)
                    {
                        map.SetTile(new Vector3Int(i, j, 0), ground);
                    }
                }
            }

        }
        else if (!inverseX && inverseY)
        {
            startPosX -= feature.offset.x;
            startPosY += feature.offset.y;
            if (null != feature.text)
            {
                int i = startPosX;
                int j = startPosY;

                foreach (char c in feature.text.text)
                {
                    switch (c)
                    {
                        case '0':
                            map.SetTile(new Vector3Int(i, j, 0), ground);
                            i++;
                            break;
                        case '1':
                            map.SetTile(new Vector3Int(i, j, 0), wall);
                            i++;
                            break;
                        case '2':
                            map.SetTile(new Vector3Int(i, j, 0), water);
                            i++;
                            break;
                        case 'x':
                            j--;
                            i = startPosX;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                for (int i = startPosX; i < startPosX + feature.size.x; i++)
                {
                    for (int j = startPosY; j > startPosY - feature.size.y; j--)
                    {
                        map.SetTile(new Vector3Int(i, j, 0), ground);
                    }
                }
            }
        }
        else
        {
            startPosX += feature.offset.x;
            startPosY += feature.offset.y;
            if (null != feature.text)
            {
                int i = startPosX;
                int j = startPosY;

                foreach (char c in feature.text.text)
                {
                    switch (c)
                    {
                        case '0':
                            map.SetTile(new Vector3Int(i, j, 0), ground);
                            i--;
                            break;
                        case '1':
                            map.SetTile(new Vector3Int(i, j, 0), wall);
                            i--;
                            break;
                        case '2':
                            map.SetTile(new Vector3Int(i, j, 0), water);
                            i--;
                            break;
                        case 'x':
                            j--;
                            i = startPosX;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                for (int i = startPosX; i > startPosX - feature.size.x; i--)
                {
                    for (int j = startPosY; j > startPosY - feature.size.y; j--)
                    {
                        map.SetTile(new Vector3Int(i, j, 0), ground);
                    }
                }
            }
        }

    }

    void FillMap(CustomRuleTile tile)
    {
        Vector3Int position = new Vector3Int(0, 0, 0);
        for (int i = -mapSizeX - 0; i < mapSizeX + 0; i++)
        {
            position.x = i;
            for (int j = -mapSizeY - 0; j < mapSizeY + 0; j++)
            {
                position.y = j;

                map.SetTile(position, tile);
            }
        }
    }

}
