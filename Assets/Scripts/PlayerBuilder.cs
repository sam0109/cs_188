using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerBuilder : MonoBehaviour
{
    public const int hatMax = 42;
    public const int hairStyleMax = 8;
    public const int hairColorMax = 10;
    public const int beardsMax = 7;
    public const int bodyMax = 3;

    public int hat;
    public int hairStyle;
    public int hairColor;
    public int body;
    public int clothes;
    public int beard;
    public string leftHandWeapon;
    public string rightHandWeapon;

    public GameObject beards;
    public GameObject hair;
    public GameObject hats;
    public GameObject bodies;
    public GameObject leftHand;
    public GameObject rightHand;

    public Button leftHats;
    public Button rightHats;
    public Button leftHairStyle;
    public Button rightHairStyle;
    public Button leftHairColor;
    public Button rightHairColor;
    public Button leftBody;
    public Button rightBody;
    public Button leftBeards;
    public Button rightBeards;
    public Button leftClothes;
    public Button rightClothes;
    
    public List<Material> materials = new List<Material>();

    public bool isMenu = false;

    void Start ()
    {
        drawCharacter();


        if (leftHairStyle != null && rightHairStyle != null)
        {
            leftHairStyle.onClick.AddListener(delegate { onleftHairStyle(); });
            rightHairStyle.onClick.AddListener(delegate { onrightHairStyle(); });
        }

        if (leftHats != null && rightHats != null)
        {
            leftHats.onClick.AddListener(delegate { onLeftHats(); });
            rightHats.onClick.AddListener(delegate { onRightHats(); });
        }

        if (leftHairColor != null && rightHairColor != null)
        {
            leftHairColor.onClick.AddListener(delegate { onleftHairColor(); });
            rightHairColor.onClick.AddListener(delegate { onrightHairColor(); });
        }

        if (leftBeards != null && rightBeards != null)
        {
            leftBeards.onClick.AddListener(delegate { onLeftBeard(); });
            rightBeards.onClick.AddListener(delegate { onRightBeard(); });
        }

        if (leftBody != null && rightBody != null)
        {
            leftBody.onClick.AddListener(delegate { onLeftBody(); });
            rightBody.onClick.AddListener(delegate { onrightBody(); });
        }

        if (leftClothes != null && rightClothes != null)
        {
            leftClothes.onClick.AddListener(delegate { onLeftClothes(); });
            rightClothes.onClick.AddListener(delegate { onrightClothes(); });
        }
    }

    public void setGameControl()
    {
        GameControl.control.myCharacter.hat = hat;
        GameControl.control.myCharacter.hairStyle = hairStyle;
        GameControl.control.myCharacter.hairColor = hairColor;
        GameControl.control.myCharacter.body = body;
        GameControl.control.myCharacter.clothes = clothes;
        GameControl.control.myCharacter.beard = beard;
    }

    public void drawCharacter()
    {
        bodies.transform.GetChild(body).gameObject.SetActive(false);

        if (hat < hatMax)
        {
            hats.transform.GetChild(hat).gameObject.SetActive(false);
        }

        if (hairStyle < hairStyleMax)
        {
            hair.transform.GetChild(hairColor).GetChild(hairStyle).gameObject.SetActive(false);
        }

        if (beard < beardsMax)
        {
            beards.transform.GetChild(hairColor).GetChild(beard).gameObject.SetActive(false);
        }

        for(int i = 0; i < leftHand.transform.childCount; i++)
        {
            leftHand.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < rightHand.transform.childCount; i++)
        {
            rightHand.transform.GetChild(i).gameObject.SetActive(false);
        }

        if (isMenu)
        {
            hat = GameControl.control.myCharacter.hat;
            hairStyle = GameControl.control.myCharacter.hairStyle;
            hairColor = GameControl.control.myCharacter.hairColor;
            body = GameControl.control.myCharacter.body;
            clothes = GameControl.control.myCharacter.clothes;
            beard = GameControl.control.myCharacter.beard;
        }
        else
        {
            hat = GameControl.control.state.frame_markers[gameObject.GetComponentInParent<FrameMarkerController>().frame_marker_identifier].hat;
            hairStyle = GameControl.control.state.frame_markers[gameObject.GetComponentInParent<FrameMarkerController>().frame_marker_identifier].hairStyle;
            hairColor = GameControl.control.state.frame_markers[gameObject.GetComponentInParent<FrameMarkerController>().frame_marker_identifier].hairColor;
            body = GameControl.control.state.frame_markers[gameObject.GetComponentInParent<FrameMarkerController>().frame_marker_identifier].body;
            clothes = GameControl.control.state.frame_markers[gameObject.GetComponentInParent<FrameMarkerController>().frame_marker_identifier].clothes;
            beard = GameControl.control.state.frame_markers[gameObject.GetComponentInParent<FrameMarkerController>().frame_marker_identifier].beard;
            leftHandWeapon = GameControl.control.state.frame_markers[gameObject.GetComponentInParent<FrameMarkerController>().frame_marker_identifier].leftHandWeapon;
            rightHandWeapon = GameControl.control.state.frame_markers[gameObject.GetComponentInParent<FrameMarkerController>().frame_marker_identifier].rightHandWeapon;
        }
        
        if(leftHandWeapon != "")
        {
            if(leftHandWeapon == "Basic Axe")
            {
                leftHand.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (leftHandWeapon == "Wood Shield")
            {
                leftHand.transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (leftHandWeapon == "Regular Sword")
            {
                leftHand.transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        if (rightHandWeapon != "")
        {
            if (rightHandWeapon == "Basic Axe")
            {
                rightHand.transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (rightHandWeapon == "Wood Shield")
            {
                rightHand.transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (rightHandWeapon == "Regular Sword")
            {
                rightHand.transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        if (hat < hatMax)
        {
            hats.transform.GetChild(hat).gameObject.SetActive(true);
        }

        if (hairStyle < hairStyleMax)
        {
            hair.transform.GetChild(hairColor).GetChild(hairStyle).gameObject.SetActive(true);
        }

        if (beard < beardsMax)
        {
            beards.transform.GetChild(hairColor).GetChild(beard).gameObject.SetActive(true);
        }

        bodies.transform.GetChild(body).gameObject.SetActive(true);
        bodies.transform.GetChild(body).gameObject.GetComponent<Renderer>().material = materials[clothes];
    }

    #region Button Events

    #region Hats
    void onLeftHats()
    {
        if (hat < hatMax)
        {
            hats.transform.GetChild(hat).gameObject.SetActive(false);
        }

        hat--;

        if (hat < 0)
        {
            hat = hatMax;
        }
        else
        {
            hats.transform.GetChild(hat).gameObject.SetActive(true);
        }
    }

    void onRightHats()
    {
        if (hat < hatMax)
        {
            hats.transform.GetChild(hat).gameObject.SetActive(false);
        }

        hat++;

        if (hat > hatMax)
        {
            hat = 0;
        }

        if(hat == hatMax)
        {
            return;
        }

        hats.transform.GetChild(hat).gameObject.SetActive(true);
    }
    #endregion

    #region hairstyle
    void onleftHairStyle()
    {
        if (hairStyle < hairStyleMax)
        {
            hair.transform.GetChild(hairColor).GetChild(hairStyle).gameObject.SetActive(false);
        }

        hairStyle--;

        if (hairStyle < 0)
        {
            hairStyle = hairStyleMax;
        }
        else
        {
            hair.transform.GetChild(hairColor).GetChild(hairStyle).gameObject.SetActive(true);
        }
    }

    void onrightHairStyle()
    {
        if (hairStyle < hairStyleMax)
        {
            hair.transform.GetChild(hairColor).GetChild(hairStyle).gameObject.SetActive(false);
        }

        hairStyle++;

        if (hairStyle > hairStyleMax)
        {
            hairStyle = 0;
        }

        if (hairStyle == hairStyleMax)
        {
            return;
        }

        hair.transform.GetChild(hairColor).GetChild(hairStyle).gameObject.SetActive(true);
    }
    #endregion

    #region hair color
    void onleftHairColor()
    {
        if (hairStyle < hairStyleMax)
        {
            hair.transform.GetChild(hairColor).GetChild(hairStyle).gameObject.SetActive(false);
        }

        if(beard < beardsMax)
        {
            beards.transform.GetChild(hairColor).GetChild(beard).gameObject.SetActive(false);
        }

        hairColor--;

        if (hairColor < 0)
        {
            hairColor = hairColorMax - 1;
        }

        if (hairStyle < hairStyleMax)
        {
            hair.transform.GetChild(hairColor).GetChild(hairStyle).gameObject.SetActive(true);
        }

        if (beard < beardsMax)
        {
            beards.transform.GetChild(hairColor).GetChild(beard).gameObject.SetActive(true);
        }
    }

    void onrightHairColor()
    {
        if (hairStyle < hairStyleMax)
        {
            hair.transform.GetChild(hairColor).GetChild(hairStyle).gameObject.SetActive(false);
        }

        if (beard < beardsMax)
        {
            beards.transform.GetChild(hairColor).GetChild(beard).gameObject.SetActive(false);
        }

        hairColor++;

        if (hairColor >= hairColorMax)
        {
            hairColor = 0;
        }

        if (hairStyle < hairStyleMax)
        {
            hair.transform.GetChild(hairColor).GetChild(hairStyle).gameObject.SetActive(true);
        }

        if (beard < beardsMax)
        {
            beards.transform.GetChild(hairColor).GetChild(beard).gameObject.SetActive(true);
        }
    }
    #endregion

    #region beard
    void onLeftBeard()
    {
        if (beard < beardsMax)
        {
            beards.transform.GetChild(hairColor).GetChild(beard).gameObject.SetActive(false);
        }

        beard--;

        if (beard < 0)
        {
            beard = beardsMax;
        }
        else
        {
            beards.transform.GetChild(hairColor).GetChild(beard).gameObject.SetActive(true);
        }
    }

    void onRightBeard()
    {
        if (beard < beardsMax)
        {
            beards.transform.GetChild(hairColor).GetChild(beard).gameObject.SetActive(false);
        }

        beard++;

        if (beard > beardsMax)
        {
            beard = 0;
        }

        if (beard == beardsMax)
        {
            return;
        }

        beards.transform.GetChild(hairColor).GetChild(beard).gameObject.SetActive(true);
    }
    #endregion

    #region body
    void onLeftBody()
    {
        bodies.transform.GetChild(body).gameObject.SetActive(false);

        body--;

        if (body < 0)
        {
            body = bodyMax - 1;
        }

        bodies.transform.GetChild(body).gameObject.SetActive(true);
        bodies.transform.GetChild(body).gameObject.GetComponent<Renderer>().material = materials[clothes];
    }

    void onrightBody()
    {
        bodies.transform.GetChild(body).gameObject.SetActive(false);

        body++;

        if (body >= bodyMax)
        {
            body = 0;
        }

        bodies.transform.GetChild(body).gameObject.SetActive(true);
        bodies.transform.GetChild(body).gameObject.GetComponent<Renderer>().material = materials[clothes];
    }
    #endregion

    #region clothes
    void onLeftClothes()
    {
        clothes--;

        if (clothes < 0)
        {
            clothes = materials.Count - 1;
        }

        bodies.transform.GetChild(body).gameObject.GetComponent<Renderer>().material = materials[clothes];
    }

    void onrightClothes()
    {
        clothes++;

        if (clothes >= materials.Count)
        {
            clothes = 0;
        }

        bodies.transform.GetChild(body).gameObject.GetComponent<Renderer>().material = materials[clothes];
    }
    #endregion

    #endregion
}
