
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Randomm = System.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Space(20)] 
    public GameObject minimapPrefab;
    public Transform minimapSpawner;
    public Transform minimapParent;

    [Space(20)] 
    public Transform[] spawners;
    public Transform spawnParent;

    [Space(20)] 
    public Image portrait; 
    
    [Space(20)] 
    public TextData maleNames;
    public TextData femaleNames;
    public TextData surnames;
    public TextData countries;
    public SpriteData malePortraits;
    public SpriteData femalePortraits;
    public List<ItemData> items;

    [Space(20)] 
    public TextMeshProUGUI successText, failureText;

    private List<string> fakeNames = new List<string> {"Chuck Norris", "Moby Dick", "Muhammed Ali", "Neil Armstrong", "J.D. Salinger","Benjamin Franklin","Holden Caulfield"};
    private List<string> fakeCountries = new List<string> {"Beleriand", "Gondor", "Mordor", "Mirkwood", "Ambrosia", "Avalon", "Branda", "Averna", "Carbombya", "El Dorado", "Freedonia", "Laputa", "Arstotzka" };


    private MiniCharController currentArrival;
    private int success, failure;
    private List<GameObject> currentObjects;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        }else{
            Instance = this;
        }

        StartCoroutine(Spawner());
    }

    private void Start()
    {
        successText.text = "Success: 0";
        failureText.text = "Failures: 0";
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSecondsRealtime(1f);
        SpawnCharacter();
        StartCoroutine(Spawner());
    }

    public IEnumerator SpawnItem(MiniCharController character)
    {
        currentArrival = character;
        
        Randomm rng = new Randomm();
        int fake;
        if (character.isReal == false)
        {
            fake = rng.Next(0, 4);
        }
        else
        {
            fake = 5;
        }
        
        int counter = 0;
        foreach (var itemData in character.items)
        {
            var instance=Instantiate(itemData.itemPrefab,spawners[counter++].position,quaternion.identity,spawnParent);
            currentObjects.Add(instance);
            Document document = instance.GetComponent<Document>();
            document.imageHolder.sprite = character.portrait;
            document.textHolder[0].text = character.fullName;
            document.textHolder[1].text = character.country;
            document.textHolder[2].text = character.birthDate;
            document.textHolder[3].text = character.gender;

            int index;
            switch (fake)
            {
                case 0:
                    if (document.imageHolder.sprite != malePortraits.sprites[0])
                    {
                        document.imageHolder.sprite = malePortraits.sprites[0];
                    }
                    else
                    {
                        document.imageHolder.sprite = femalePortraits.sprites[1];
                    }

                    break;
                case 1:
                    if (fakeNames.Count >0)
                    {
                        index = Random.Range(0, fakeNames.Count);
                        document.textHolder[0].text = fakeNames[index];
                        fakeNames.RemoveAt(index);
                    }
                    else
                    {
                        document.textHolder[0].text = "********";
                    }

                    break;
                case 2:
                    if (fakeCountries.Count >0)
                    {
                        index = Random.Range(0, fakeCountries.Count);
                        document.textHolder[1].text = fakeCountries[index];
                        fakeCountries.RemoveAt(index);
                    }
                    else
                    {
                        document.textHolder[1].text = "********";
                    }

                    break;
                case 3:
                    document.textHolder[2].text = "********";
                    break;
                case 4:
                    document.textHolder[3].text = "********";
                    break;
            }
            
            yield return new WaitForSecondsRealtime(0.5f);
        }
        
    }

    public void SpawnCharacter()
    {
        string name, surname, country;
        Sprite charPortrait;

        Randomm rng = new Randomm();
        int gender = rng.Next(1, 3);

        List<string> stringData;
        List<Sprite> spriteData;
        int index;
        if (gender == 1 )
        {
            stringData = maleNames.data;
            index = rng.Next(stringData.Count);
            name = stringData[index];

            spriteData = malePortraits.sprites;
            index = rng.Next(spriteData.Count);
            charPortrait = spriteData[index];


        }
        else
        {
            stringData = femaleNames.data;
            index = rng.Next(stringData.Count);
            name = stringData[index];
            
            spriteData = femalePortraits.sprites;
            index = rng.Next(spriteData.Count);
            charPortrait = spriteData[index];
        }
        
        

        stringData = surnames.data;
        index = rng.Next(stringData.Count);
        surname = stringData[index];

        stringData = countries.data;
        index = rng.Next(stringData.Count);
        country = stringData[index];

        string fullName = name + " " + surname;
        GameObject charObject = Instantiate(minimapPrefab, minimapSpawner.position, Quaternion.identity, minimapParent);
        MiniCharController charController = charObject.GetComponent<MiniCharController>();

        string year = rng.Next(1970, 2001).ToString();
        string month = rng.Next(1, 13).ToString();
        string day = rng.Next(1, 13).ToString();
        string date = day+"/"+month + "/" + year;
        
        charController.CharSetup(fullName,country,charPortrait,items, date, gender);
    }

    public void ButtonCall(bool isRed)
    {
        if (currentArrival == null)
        {
            return;
        }
        if (currentArrival.isReal == !isRed)
        {
            success++;
            successText.text = $"Success: {success}";
        }
        else
        {
            failure++;
            failureText.text = $"Failures: {failure}";
        }
        
        foreach (GameObject obj in currentObjects)
        {
            Destroy(obj);
        }
        currentArrival.Served();
        currentArrival = null;
    }
}
