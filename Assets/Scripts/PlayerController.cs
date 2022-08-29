using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private List<Transform> car1ContainerPositions = new List<Transform>();
    private List<Transform> car2ContainerPositions = new List<Transform>();

    public List<Transform> finishPositions = new List<Transform>();
    public List<GameObject> carPrefabs = new List<GameObject>();


    private Dictionary<string, int> loadAmounts = new Dictionary<string, int>() { { "car1", 0 }, { "car2", 0 } };

    #region UnityCallbacks

    private void Start()
    {
        for (int i = 0; i < transform.GetChild(0).GetChild(0).childCount; i++)
        {
            car1ContainerPositions.Add(transform.GetChild(0).GetChild(0).GetChild(i));
        }

        for (int i = 0; i < transform.GetChild(1).GetChild(0).childCount; i++)
        {
            car2ContainerPositions.Add(transform.GetChild(1).GetChild(0).GetChild(i));
        }
    }

    private void OnEnable()
    {
        EquationTrigger.OnEquationTrigger += (value, sign, car) => SetCarCount(value, sign, car);

        SwipeDetection.OnSwipeLeft += MoveLoadsToCar1;
        SwipeDetection.OnSwipeRight += MoveLoadsToCar2;

        FinishTrigger.OnFinish1 += (positionL, positionR) => FinishLoaader(positionL, positionR);
        FinishTrigger.OnFinish2 += (positionL, positionR) => FinishLoaader(positionL, positionR);
        FinishTrigger.OnFinish3 += (positionL, positionR) => FinishLoaader(positionL, positionR);
    }

    private void OnDisable()
    {
        EquationTrigger.OnEquationTrigger -= (value, sign, car) => SetCarCount(value, sign, car);

        SwipeDetection.OnSwipeLeft -= MoveLoadsToCar1;
        SwipeDetection.OnSwipeRight -= MoveLoadsToCar2;
        
        FinishTrigger.OnFinish1 -= (positionL, positionR) => FinishLoaader(positionL, positionR);
        FinishTrigger.OnFinish2 -= (positionL, positionR) => FinishLoaader(positionL, positionR);
        FinishTrigger.OnFinish3 -= (positionL, positionR) => FinishLoaader(positionL, positionR);
    }

    #endregion

    #region Load Manager Functions

    private void SetCarCount(int value, bool sign, GameObject car)
    {
        if (car.name == "tir1")
        {
            if (sign) LoadCar(car1ContainerPositions, value, "car1");
            else ExtractCar(car1ContainerPositions, value, "car1");
        }

        else
        {
            if (sign) LoadCar(car2ContainerPositions, value, "car2");
            else ExtractCar(car2ContainerPositions, value, "car2");
        }
    }

    private void LoadCar(List<Transform> posList, int amount, string carType)
    {
        foreach (var pos in posList)
        {
            if (pos.childCount == 0 && amount != 0 && loadAmounts[carType] != 12)
            {
                Instantiate(carPrefabs[Random.Range(0, carPrefabs.Count)], posList[loadAmounts[carType]]);
                amount--;
                loadAmounts[carType]++;
            }
        }
    }

    private void ExtractCar(List<Transform> posList, int amount, string carType)
    {
        for (int i = posList.Count - 1; i >= 0; i--)
        {
            if (posList[i].childCount != 0 && amount != 0)
            {
                Destroy(posList[i].GetChild(0).gameObject);
                amount--;
                loadAmounts[carType]--;
            }
        }
    }

    private void MoveLoadsToCar1()
    {
        Debug.Log("swipeLeft");

        if (car2ContainerPositions[0].childCount == 0)
        {
            return;
        }

        for (int i = car2ContainerPositions.Count - 1; i >= 0; i--)
        {
            if (car2ContainerPositions[i].childCount != 0 && loadAmounts["car1"] != 12)
            {
                car2ContainerPositions[i].GetChild(0).SetParent(car1ContainerPositions[loadAmounts["car1"]]);
                car1ContainerPositions[loadAmounts["car1"]].GetChild(0).localPosition = Vector3.zero;

                loadAmounts["car1"]++;
                loadAmounts["car2"]--;
            }
        }
    }

    private void MoveLoadsToCar2()
    {
        if (car1ContainerPositions[0].childCount == 0)
        {
            return;
        }

        for (int i = car1ContainerPositions.Count - 1; i >= 0; i--)
        {
            if (car1ContainerPositions[i].childCount != 0 && loadAmounts["car2"] != 12)
            {

                car1ContainerPositions[i].GetChild(0).SetParent(car2ContainerPositions[loadAmounts["car2"]]);
                car2ContainerPositions[loadAmounts["car2"]].GetChild(0).localPosition = Vector3.zero;

                loadAmounts["car1"]--;
                loadAmounts["car2"]++;
            }
        }
    }

    #endregion

    #region Finish Functions

    private void SetLoadsEven()
    {
        int totalLoads = loadAmounts["car1"] + loadAmounts["car2"];
        if (loadAmounts["car1"] != loadAmounts["car2"])
        {
            int avarage = totalLoads / 2;

            if (loadAmounts["car1"] >= avarage)
            {
                MoveLoadsToCar2();
            }
            else
            {
                MoveLoadsToCar1();
            }
        }

        else
        {
            return;
        }
    }

    private void FinishLoaader(Transform positionL, Transform positionR)
    {
        car1ContainerPositions[loadAmounts["car1"] - 1].GetChild(0).SetParent(positionL);
        positionL.GetChild(0).position = Vector3.zero;

        car2ContainerPositions[loadAmounts["car2"] - 1].GetChild(0).SetParent(positionR);
        positionR.GetChild(0).position = Vector3.zero;

        Debug.Log(positionL.name);
    }

    private void MoveLoadsToSpots()
    {

    }

    #endregion
}