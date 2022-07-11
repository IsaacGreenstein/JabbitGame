using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public GameObject targetButton; // this is the button that will move from hole to hole and show/hide as needed

    public GameObject holePrefab; // the hole object that you will spawn in
    public List<GameObject> holePositions = new List<GameObject>(); // the places you set up in the scene that holes can be spawned at
    public int holeAmount; // the number of holes to spawn.  This should be much less than the different positions to spawn holes.
    private List<GameObject> usedHolePositions = new List<GameObject>(); // the list used to store the hole positions that we already used
    public List<GameObject> spawnedHoles = new List<GameObject>(); // the list of hole prefabs/templates that we have spawned.  This will be used for clearing the scene


    // NOTE:
    // Using lists as opposed to arrays is generally not as good for RAM usage, but they have the benefit ofbeing able to add and remove items as
    // We need.  Also, because the game is so simple, it won't be a problem.

    // These are UnityEvents that are called in the correct/incorrect methods below.  It just makes it easier to set up game logic without code
    public UnityEvent onCorrectHoleSelected;
    public UnityEvent onIncorrectHoleSelected;

    // ------------------------------------------------
    private void Start()
    {
        CreateLevel(holeAmount); 
    }


    /// <summary>
    /// This function clears the scene and creates a new level with the desired number of holes
    /// All you have to do is call this function with the number of holes you want
    /// </summary>
    public void CreateLevel(int desiredHoleAmound)
    {
        if(desiredHoleAmound != -1)
            holeAmount = desiredHoleAmound;
        CancelInvoke();
        ClearLevel();
        GenerateHoleLayer();
        InvokeRepeating("SetCorrectHoleToRandomHolePosition",0.5f , 1.5f);
    }

    // The functions below are public so you can use them elsewhere easier

    public void GenerateHoleLayer()

    {
        // this loops through the number of desired holes, essentially doing the enclosed code that number of times
        for (int i = 0; i <= holeAmount; i++)
        {
            GameObject holePos = NewHolePosition(); // Uses the function below to get a random hole position gameobject
            GameObject spawnedHole = Instantiate(holePrefab, holePos.transform.position, holePos.transform.rotation); // creates a clone of the hole prefab at the chosen hole position
            spawnedHole.transform.SetParent(transform, true);
            usedHolePositions.Add(holePos); // adds that position to the list of used hole positions
            spawnedHoles.Add(spawnedHole); // adds the newly spawned hole to the list of spawned holes
            spawnedHole.SetActive(true);
        }
    }

    /// <summary>
    /// This function goes through evenry spawned hole in the list and destroys the gameobject.
    /// Then, it clears the list of spawned holes.
    /// </summary>
    public void ClearLevel()
    {
        targetButton.SetActive(false);
        foreach (GameObject obj in spawnedHoles)
            Destroy(obj);

        spawnedHoles.Clear();
        usedHolePositions.Clear();
    }


    /// <summary>
    /// This function sets the correct hole to a random hole from the usedHolePositions list
    /// </summary>
    public void SetCorrectHoleToRandomHolePosition()
    {
        targetButton.gameObject.SetActive(true); // set it as active
        int randomHole = RandomIndexFromList(usedHolePositions); // get a random index from the list of used hole positions
        targetButton.transform.position = usedHolePositions[randomHole].transform.position; // set the target button's position to that position
        targetButton.transform.SetAsLastSibling();
    }


    /// <summary>
    /// This method should be set up in the OnClick event in the correct hole button component
    /// </summary>
    public void CorrectHoleSelected()
    {
        CreateLevel(holeAmount);
        print("yayyyyy");
        onCorrectHoleSelected.Invoke();
    }

    /// <summary>
    /// This method should be setup in the OnClick event of the incorrect hole button component
    /// </summary>
    public void IncorrectHoleSelected()
    {
        print("boooooo");
        onIncorrectHoleSelected.Invoke();
    }

    /// <summary>
    /// This tries to return a new random hole position from the list that isn't already used
    /// </summary>
    /// <returns></returns>
    GameObject NewHolePosition()
    {
        int newHoleIndex = RandomIndexFromList(holePositions); // Use the int function below to get a random index from that list
        GameObject holePos = holePositions[newHoleIndex]; // get the gameObject at this random index from the list of hole positions.  These three lines essentially gets a random position from the list of positions

        if (usedHolePositions.Contains(holePos)) // if we already got this hole position, try again
        {
            return NewHolePosition();
            // We only need to try again once because as long as the number of desired holes to spawn is less than the total number of 
            // preset hole positions it won't be so hard to find a unique one.
            // For some context, if the desired number of holes was less than the possible hole positions, this could result in an endless loop and 
            // crash the game
        }
        else //otherwise, return that hole position
        {
            return holePos;
        }

    }

    int RandomIndexFromList(List<GameObject> usedList)
    {
        var random = new System.Random(); // create random reference 
        int randomIndex = random.Next(usedList.Count); // use it to get a random value between 0 and the count of the passed in list
        return randomIndex; // return that number
    }

}