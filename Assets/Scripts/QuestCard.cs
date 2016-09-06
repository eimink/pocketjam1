using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestCard : MonoBehaviour {

    //public Text Title;
    public Text Description;
    public Text Succeed;
    private Image slot1;
    private Image slot2;

    private string neededItemID1;
    private string neededItemID2;


   public void SetNeededIDs( string ID1, string ID2 )
    {
        neededItemID1 = ID1;
        neededItemID2 = ID2;
    }

    public string GetNeededIDFrom( int slot )
    {
        string returnStr = "";
        if ( slot == 0)
        {
            returnStr = neededItemID1;
        }
        else
        {
            returnStr = neededItemID2;
        }
        return returnStr;
    }

    //public void SetTitle( string text )
    //{
    //    Title.text = text;
    //}

    public void SetDescription( string text )
    {
        Description.text = text;
    }

    public void SetSucceed(string text)
    {
        Succeed.text = text;
    }


    public void SetSlot1( Image img)
    {
        slot1 = img;
    }

    public void SetSlot2(Image img)
    {
        slot2 = img;
    }
}
