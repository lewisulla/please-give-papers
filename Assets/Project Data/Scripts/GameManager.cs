
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Space(20)] 
    public GameObject minimapPrefab;
    public Transform minimapSpawner;
    public Transform minimapParent;

    [Space(20)] 
    public Transform spawner;
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

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
        }else{
            Instance = this;
        }

        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSecondsRealtime(1f);
        SpawnCharacter();
        StartCoroutine(Spawner());
    }

    public void SpawnItem(MiniCharController character)
    {
        foreach (var itemData in character.items)
        {
            var instance=Instantiate(itemData.itemPrefab,spawner.position,quaternion.identity,spawnParent);
            instance.GetComponent<Document>().imageHolder.sprite = character.portrait;
        }
        
    }

    public void SpawnCharacter()
    {
        string name, surname, country;
        Sprite charPortrait;

        Random rng = new Random();
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
        charController.CharSetup(fullName,country,charPortrait,items);

    }
}
