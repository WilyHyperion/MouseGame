using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectRecipe : OvenUIState
{
    //TODO - this is how we'd control unlocked recipes. Prob want to make it static but for now Im manually setting it
    public  Recipe[] allRecipes;
    public Image[] allImages;
    public int current;

    void Start()
    {
        var imglist = GetComponentsInChildren<Image>().ToList();
        imglist.RemoveAll((Image) =>
        {
            return Image.CompareTag("NoChangeImage");
        });
        allImages = imglist.ToArray();
        UpdateImages();
    }
    public void OnEvent(OvenUIMessage e)
    {
        switch (e)
        {
            case OvenUIMessage.Left:
                current = (current  - 1) % (allRecipes.Length);
                break;
            case OvenUIMessage.Right:
                current = (current +1 ) % (allRecipes.Length);
                break;
            case OvenUIMessage.Confirm:
                Debug.Log(allRecipes[current].DisplayName);
                break;
        }
        UpdateImages();
        Debug.Log(current);

    }
    void UpdateImages()
    {
        int amount = allImages.Length;
        if(amount % 2 == 0)
        {
            Debug.LogWarning("Even amount of recipe images - " + amount );
        }
        int offset = (amount / 2);
        for(int i = 0; i < amount; i++)
        {
            Image cur = allImages[i];
            int index = ((current - offset + i) % allRecipes.Length);
            Debug.Log(index);
            if(index < 0)
            {
                index = amount + index ;
            }
            Debug.Log(index);
            cur.sprite = allRecipes[index].Sprite;
        }
    }
}
