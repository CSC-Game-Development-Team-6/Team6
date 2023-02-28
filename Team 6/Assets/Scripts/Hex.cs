using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


/// <summary>
/// The Hex class defines the grid position, world space position, size,
/// neighbours,etc.. of a Hex Tile. However, it doesn NOT interact with 
/// Unity directly in any way.
/// </summary>


public class Hex 
{

    public readonly int Q;  //Column
    public readonly int R;  // Row
    public readonly int S;  //Some --- Not need

	//TODO: remove now in HexDem
    static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
    float radius = 1f;
	
	//TODO: remove now in Hexmap
    bool allowWrapEastWest = true;
    bool allowWrapNorthSouth = false;

	//TODO: remove now in HexDem
    private static int offset = Random.Range(0, 200);

    private HashSet<Unit> units;
    

    public Hex(int q, int r)
    {
        this.Q = q;
        this.R = r;
        this.S = -(q + r);
    }

    // Q + R + S = 0
    // S = - (Q+R)
    /// <summary>
    /// Returns the world space position on this hex
    /// </summary>

    public Vector3 Position()
    {

        return new Vector3(
         HexHorizontalSpacing() * (this.Q + this.R / 2f),
         0,
         HexVerticalSpacing() * this.R
        );

    }


	//TODO: remove now in HexDem
    public float HexHeight()
    {
        return radius * 2;
    }
	//TODO: remove now in HexDem
    public float HexWidth()
    {
        return WIDTH_MULTIPLIER * HexHeight();
    }
	//TODO: remove now in HexDem
    public float HexVerticalSpacing()
    {
        return HexHeight() * 0.75f;
    }
	//TODO: remove now in HexDem
    public float HexHorizontalSpacing()
    {
        return HexWidth();
    }

	//TODO: remove now in HexComp
    public Vector3 PositionFromCamera()
    {
        return HexMap.GetHexPosition(this);
    }
	//TODO: remove now in HexComp
    public Vector3 PositionFromCamera(Vector3 cameraPosition, float numRows, float numColumns)
    {

        float mapHeight = numRows * HexVerticalSpacing();
        float mapWidth = numColumns * HexHorizontalSpacing();



        Vector3 position = Position();



        if (allowWrapEastWest)

        {

            float howManyWidthsFromCamera = (position.x - cameraPosition.x) / mapWidth;


            //We want howmanyWidthsFromCamera to be between -0.5 to 0.5

            if (howManyWidthsFromCamera > 0)
            {
                howManyWidthsFromCamera += 0.5f;
            }
            else
            {
                howManyWidthsFromCamera -= 0.5f;
            }

            int howManyWidthToFix = (int)howManyWidthsFromCamera;


            position.x -= howManyWidthToFix * mapWidth;

        }


        if (allowWrapNorthSouth)
        {

            float howManyHeightsFromCamera = (position.z - cameraPosition.z) / mapHeight;

            //We want howmanyWidthsFromCamera to be between -0.5 to 0.5

            if (howManyHeightsFromCamera > 0)
            {
                howManyHeightsFromCamera += 0.5f;
            }else
            {
                howManyHeightsFromCamera -= 0.5f;
            }

            int howManyHeightsToFix = (int)howManyHeightsFromCamera;

            position.z -= howManyHeightsToFix * mapHeight;

        }

        return position;

    }


    public int GetElevation()
    {

        float scale = 10;

        int elevation =  (int)(Mathf.PerlinNoise((Q+ offset+(int)(R/2)) / scale, (R+offset)/scale) * 100) ;

        if (elevation > 99)
        {
            return 4;
        } 
        
        return elevation / 20;
    }


    public void AddUnit(Unit unit)
    {

        if(units == null)
        {
            units = new HashSet<Unit>();
        }

        units.Add(unit);

    }



    public void RemoveUnit(Unit unit)
    {
        if(units != null)
        {
            units.Remove(unit);
        }
    }

    public Unit[] Units()
    {
        return units.ToArray();
    }
}
