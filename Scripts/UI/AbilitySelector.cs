using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class AbilitySelector : MonoBehaviour
{
    Abillity[] abillities;

    private void Start()
    {
        Level level = GameManager.levels.levels[GameManager.levels.currentLevel];
        System.Random rnd = new System.Random();
        Abillity[] _abillities = level.abilities.abillities.OrderBy(x => rnd.Next()).Take(3).ToArray();


        RenderAbilities(_abillities);
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
                case "Age":
                    Debug.LogWarning("Age notimplemented");
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

        GameManager.RestartGame();
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


            Image img = element.Q<Image>("Image");
            img.sprite = abillity.image;
            img.style.width = 200;
            img.style.height = 200;
            img.scaleMode = ScaleMode.ScaleToFit;

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
                        ((Label)pros.ElementAt(proIndex)).text = $"{attribute.name} {(attribute.modifier >= 0 ? '+' : '-')} {Mathf.Abs(attribute.modifier)}";
                        proIndex++;
                        break;
                    case Abillity.Attribute.Type.Con:
                        ((Label)cons.ElementAt(conIndex)).text = $"{attribute.name} {(attribute.modifier >= 0 ? '+' : '-')} {Mathf.Abs(attribute.modifier)}";
                        conIndex++;
                        break;
                }
            }

            element.RegisterCallback<ClickEvent>(OnAbilitySelected);
        }
    }
}

[System.Serializable]
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

    [System.Serializable]
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