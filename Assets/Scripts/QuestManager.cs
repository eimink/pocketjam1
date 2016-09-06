using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public float margin;
    public GameObject questCardTemplate;
    string[] questTexts = new string[7];
    string[] succeedTexts = new string[7];
    int questIndex = 0;

    string[] neededItemID1 = new string[7];
    string[] neededItemID2 = new string[7];

    List<QuestCard> questCards = new List<QuestCard>();
    List<GameObject> questCardGameObjects = new List<GameObject>();

    public Vector3 offScreen = new Vector3(400f, 0, 0);

    public TextEventHandler textEventHandler;

    private string lastSeenItemID = "-";
    public Image lastSeenImage;
    bool firstDone = false;

    // Use this for initialization
    void Start()
    {
        SetQuestTexts();
        SetQuestSucceedTexts();
        SetNeededItemIDSlot1();
        SetNeededItemIDSlot2();
        MakeQuestCards();
        FillQuestCardTheList();
        SetCards();
        questCardTemplate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            questIndex++;
            Debug.Log("questIndex >" + questIndex);
            ShowNextCard();
        }

       // if (Input.GetKeyDown(KeyCode.S))
       // {
            if (lastSeenItemID == neededItemID1[questIndex] || lastSeenItemID == neededItemID2[questIndex])
            {
                Debug.Log("Found:"+ lastSeenItemID);
           

            if (firstDone)
            {
                questCards[questIndex].SetSlot2(lastSeenImage);
            }
            else
            {
                questCards[questIndex].SetSlot1(lastSeenImage);
                firstDone = true;
            }
               
                questCards[questIndex].ShowTheSucceedText(true);
            }
       // }

     }

    public void SetLastSeenImage( Image img)
    {
        lastSeenImage = img;
    }

    public void SetLastSeenItem( string item )
    {
        lastSeenItemID = item;
    }

    void ShowNextCard()
    {
        this.transform.position += offScreen;
    }

    void SetCards()
    {
        for (int i = 0; i < 7; i++)
        {
            questCards[i].SetDescription(questTexts[i]);
            questCards[i].SetSucceed(succeedTexts[i]);
            questCards[i].SetNeededIDs(neededItemID1[i], neededItemID2[i]);
        }
    }

    void MakeQuestCards()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject questC = Instantiate(questCardTemplate, questCardTemplate.transform.position, questCardTemplate.transform.rotation) as GameObject;
            questC.transform.SetParent(this.transform);
            questC.transform.position = new Vector3(questC.transform.position.x + i*margin, questC.transform.position.y, questC.transform.position.z);
            questCardGameObjects.Add(questC);
        }
    }

    void FillQuestCardTheList()
    {
        foreach (GameObject g in questCardGameObjects)
        {
            questCards.Add(g.GetComponent<QuestCard>());
            Debug.Log("questcard list size: " + questCards.Count);
        }
    }

    void SetQuestTexts()
    {
        questTexts[0] = "Quest 1: You are Dr. Juicy Oakes, a world renowned juicer. Instead of fruits, you collect words from your surroundings & blend them to create magical juices with special properties. Hint: Once upon a time, a nun tried to catch a chicken, but found she had left her handcuffs in bed. Try it out! Start scanning!";
        questTexts[1] = "Quest 2: Little did the nun know, she was on Reality T.V. Big brother would have expected her to wear a cross and pray, for the events that follow were far beyond the capabilities of one misguided nun.";
        questTexts[2] = "Quest 3: You find Mr.Torvalds, the president of The Secret Hacker’s Society, passed out at his desk, surrounded by feathers, take -out boxes & cold pizza.His screen shows that he was googling Donald Trump.Mix him some Conspiring Addict juice to help him down off his latest junk-food binge.You need his help tracking down the feathered conspires,  after all!";
        questTexts[3] = "Quest 4: Ms. Gianna, Chairperson of the Philanthropist Friends of Dolphins Foundation, is rumoured to be using the new tank she’s building to harbour wayward chickens. You find her having salad at her office canteen and plan to question her when an electric blue dildo slipped out of her pocket. Help her cover it up by making some Sainted Pervert juice.";
        questTexts[4] = "Quest 5: You find DJ Candy Cruncher alone in a motel room, looking forlornly at a tower of records. When you approach him and ask about the chickens, he says he’s had enough of the drugs & rave scene, and has nothing to do with chickens. “I want to make like Johnny Cash in Folsom Prison, and free my inner do-gooder” he says. Mix him some Addicted Philanthropist juice and help him walk the line.";
        questTexts[5] = "Quest 6: Studio Tentacle was working on its new anime, Alien Moon, when the cloud servers were compromised by The Secret Hackers Society. Their entire archive of fan service is now offline, and their community manager is swamped with tweets from angry fans. Only a dash of Pervert Conspiracy juice can save the day.";
        questTexts[6] = "Quest 7: You stayed up late, binge watching pikachu spoofs, and are out walking your dog at four a.m. You see a woman in dark black robes (the nun?!) pour some mysterious pink liquid with a picture of a pyramid and eye on the label into the city’s water supply. “This will stop them,” you overhear her muttering. Quick! Counteract the effects by pouring in some Conspiring Addict juice!";
    }

    void SetQuestSucceedTexts()
    {
        succeedTexts[0] = "Good job! You have collected your first few items! Mix them up to see what juice you’ll get!";
        succeedTexts[1] = "Congratulations! You’ve mixed some reality T.V. & a cross to make some Addict Philanthropist juice.";
        succeedTexts[2] = "Congratulations! You’ve mixed some Trump & Pizza and made Conspiring Addict juice. Mr. Torvald’s has granted you unlimited free access to the society's hacking services.";
        succeedTexts[3] = "Congratulations! You’ve mixed some vegetables and a blue dildo to Sainted Pervert juice & saved Ms. Gianna’s reputation. It turns out she was keeping wild pheasants & no foul play was involved. She can focus on saving Flipper now! She tells you her son may know something.";
        succeedTexts[4] = "Awesome! You mixed some drugs & charity to make some Addicted Philanthropist juice! DJ Candy Cruncher has started a halfway house for reforming ravers. He has you to thank for it & gives you all his leftover ecstasy tablets. These will surely come in handy! Inside the ziplock bag is a business card for Studio Tentacle, a famous animation studio.";
        succeedTexts[5] = "Great work! You’ve mixed an alien & a tentacle to make a Pervert Conspiracy juice. You’ve been granted lifetime access to Studio Tentacle’s high quality Internet streaming service.";
        succeedTexts[6] = "Congratulations! You’ve mixed pikachu & a pyramid & saved the city! You are hot on the trail of the nun, who is rushing through the night to escape you.";
    }

    void SetNeededItemIDSlot1()
    {
        neededItemID1[0] = "trump";
        neededItemID1[1] = "reality";
        neededItemID1[2] = "trump";
        neededItemID1[3] = "veggies";
        neededItemID1[4] = "pills";
        neededItemID1[5] = "tentacle";
        neededItemID1[6] = "pikachu";
    }

    void SetNeededItemIDSlot2()
    {
        neededItemID2[0] = "pyramid";
        neededItemID2[1] = "cross";
        neededItemID2[2] = "pizza";
        neededItemID2[3] = "dildo";
        neededItemID2[4] = "charity";
        neededItemID2[5] = "alien";
        neededItemID2[6] = "pyramid";
    }


    string GetQuestText(int num)
    {
        return questTexts[num];
    }

    string GetSucceedText(int num)
    {
        return succeedTexts[num];
    }
}
