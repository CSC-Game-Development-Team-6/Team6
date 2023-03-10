using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitComponent : MonoBehaviour
{

    private Vector3 oldPostion;
    private Vector3 newPosition;

    private Vector3 currentVelocity;
    private GameObject healthBar;

    float smoothTime = .1f;

    public static GameData Game;
    public static GameComponent GameLogic;
    public Unit unit;

    private float shrinkSpeed = .9f;


    private void Start()
    {
        oldPostion = newPosition = this.transform.position;
        healthBar = this.gameObject.transform.Find("HealthBar").gameObject;
        
        this.transform.localScale = new Vector3(.01f, .01f, .01f);
    }

    private void Update()
    {

        if (unit.HitPoints <= 0)
        {
            HandleDeath();
            return;
        }else if (this.transform.localScale.x < 1 )
        {
            this.transform.localScale += new Vector3(.01f, .01f, .01f);
        }

        UpdatePosition();
        UpdateHealthBar();
        UpdateHexPosition();
    }

    public static void SetGameData(GameData GameData)
    {
        UnitComponent.Game = GameData;
    }
    public static void SetGameLogic(GameComponent GameLogic)
    {
        UnitComponent.GameLogic = GameLogic;
    }

    private void UpdateHexPosition()
    {
        HexComponent parentComponent = this.transform.parent.GetComponent<HexComponent>();

        if (parentComponent == null)
        {
            Debug.Log("Ooops");
            return;
        }

        Hex parentHex = parentComponent.hex;

        if (this.unit.hex != parentHex)
        {
            HexComponent newComponent = HexMap.HexToGameObject[this.unit.hex].GetComponent<HexComponent>();

            newPosition = newComponent.PositionFromCamera();

            currentVelocity = Vector3.zero;

            this.transform.parent = newComponent.transform;
        }
    }
    public void OnUnitMove(Hex newHex)
    {

        if (unit.Move(newHex))
        {
            HexComponent newComponent = HexMap.HexToGameObject[newHex].GetComponent<HexComponent>();

            newPosition = newComponent.PositionFromCamera();

            currentVelocity = Vector3.zero;

            this.transform.parent = newComponent.transform;
        }
        
    }

    public void UpdateHealthBar()
    {
        
        healthBar.transform.localScale = new Vector3((float)unit.HitPoints / (float)unit.MaxHitPoints,1,1);
    }


    private void HandleDeath()
    {
        if (this.gameObject.transform.localScale.magnitude < .1f)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.transform.localScale *= shrinkSpeed;
        }
    }

    private void UpdatePosition()
    {
        if (newPosition == oldPostion)
        {
            return;
        }

        if ( FinishedMove() )
        {
            oldPostion = newPosition ;
        }
        else
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, newPosition, ref currentVelocity, smoothTime);
        }
      
    }

    private bool FinishedMove()
    {
        return (newPosition - this.transform.position).magnitude < .01f;
    }
}
