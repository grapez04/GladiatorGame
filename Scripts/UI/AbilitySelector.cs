
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilitySelector : MonoBehaviour
{
    Abillity[] abillities;

    private void Update()
    {
        Debug.Log($"playerSpeed: {GameManager.playerSpeed}\nplayerDamage: {GameManager.playerDamage}\nplayerHealth: {GameManager.playerHealth}");
    }

    private void Start()
    {
        Texture2D testAbillity1Texture = Texture2D.blackTexture;
        Texture2D testAbillity2Texture = Texture2D.grayTexture;
        Texture2D testAbillity3Texture = Texture2D.whiteTexture;

        testAbillity1Texture.Reinitialize(100, 100);
        testAbillity2Texture.Reinitialize(100, 100);
        testAbillity3Texture.Reinitialize(100, 100);

        RenderAbilities(new Abillity[]{
            new Abillity("TestAbility1", Sprite.Create(testAbillity1Texture, new Rect(0,0,testAbillity1Texture.width, testAbillity1Texture.height), Vector2.zero), new Abillity.Attribute[]{
                new Abillity.Attribute("Speed", +1, Abillity.Attribute.Type.Pro),
                new Abillity.Attribute("Damage", +1, Abillity.Attribute.Type.Pro),
                new Abillity.Attribute("Health", -1, Abillity.Attribute.Type.Con),
                new Abillity.Attribute("Speed", -1, Abillity.Attribute.Type.Con),
            }),
            new Abillity("TestAbility2", Sprite.Create(testAbillity2Texture, new Rect(0,0,testAbillity2Texture.width, testAbillity2Texture.height), Vector2.zero), new Abillity.Attribute[]{
                new Abillity.Attribute("Speed", +2, Abillity.Attribute.Type.Pro),
                new Abillity.Attribute("Damage", +2, Abillity.Attribute.Type.Pro),
                new Abillity.Attribute("Health", -2, Abillity.Attribute.Type.Con),
                new Abillity.Attribute("Speed", -2, Abillity.Attribute.Type.Con),
            }),
            new Abillity("TestAbility3", Sprite.Create(testAbillity3Texture, new Rect(0,0,testAbillity3Texture.width, testAbillity3Texture.height), Vector2.zero), new Abillity.Attribute[]{
                new Abillity.Attribute("Speed", +3, Abillity.Attribute.Type.Pro),
                new Abillity.Attribute("Damage", +3, Abillity.Attribute.Type.Pro),
                new Abillity.Attribute("Health", -3, Abillity.Attribute.Type.Con),
                new Abillity.Attribute("Speed", -3, Abillity.Attribute.Type.Con),
            }),
        });
    }


    private void OnAbilitySelected(ClickEvent evt)
    {
        int abilityIndex = int.Parse($"{evt.target.ToString()[14]}");

        Abillity abillity = abillities[abilityIndex];

        foreach (Abillity.Attribute attribute in abillity.attributes)
        {
            switch (attribute.name)
            {
                case "Speed":
                    GameManager.playerSpeed += attribute.modifier;
                    break;
                case "Damage":
                    GameManager.playerDamage += attribute.modifier;
                    break;
                case "Health":
                    GameManager.playerHealth += attribute.modifier;
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }
        foreach (VisualElement element in GetComponent<UIDocument>().rootVisualElement.Children().First().Children())
        {
            element.UnregisterCallback<ClickEvent>(OnAbilitySelected);
        }

        gameObject.SetActive(false);
    }

    public void RenderAbilities(Abillity[] renderAbillities = null)
    {
        if (renderAbillities != null)
        {
            abillities = renderAbillities;
        }

        for (int i = 0; i < GetComponent<UIDocument>().rootVisualElement.Children().First().childCount; i++)
        {
            VisualElement element = GetComponent<UIDocument>().rootVisualElement.Children().First().ElementAt(i);
            Abillity abillity = abillities[i];

            element.Q<Image>("Image").sprite = abillity.image;
            element.Q<Label>("title").text = abillity.name;

            GroupBox pros = element.Q<GroupBox>("pros");
            GroupBox cons = element.Q<GroupBox>("cons");

            int proIndex = 0;
            int conIndex = 0;

            foreach (Abillity.Attribute attribute in abillity.attributes)
            {
                switch (attribute.type)
                {
                    case Abillity.Attribute.Type.Pro:
                        ((Label)pros.ElementAt(proIndex)).text = attribute.name;
                        proIndex++;
                        break;
                    case Abillity.Attribute.Type.Con:
                        ((Label)cons.ElementAt(conIndex)).text = attribute.name;
                        conIndex++;
                        break;
                }
            }

            element.RegisterCallback<ClickEvent>(OnAbilitySelected);
        }
    }
}


public class Abillity
{
    public string name;
    public Sprite image;
    public Attribute[] attributes;

    public Abillity(string _name, Sprite _image, Attribute[] _attributes)
    {
        name = _name;
        image = _image;
        attributes = _attributes;
    }

    public class Attribute
    {
        public string name;
        public float modifier;
        public Type type;

        public enum Type
        {
            Pro,
            Con,
        }

        public Attribute(string _name, float _modifier, Type _type)
        {
            name = _name;
            modifier = _modifier;
            type = _type;
        }
    }
}

public class GameManager : MonoBehaviour
{
    public static float playerSpeed = 1f;
    public static float playerDamage = 1f;
    public static float playerHealth = 1f;
}