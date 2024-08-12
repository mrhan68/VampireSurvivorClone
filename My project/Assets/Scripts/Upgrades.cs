using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    GameManager _gameManager;
    public GameObject upgradePanel;
    public GameObject rerollButton;
    public Image UpgradeButton1;
    public Image UpgradeButton2;
    public Image UpgradeButton3;
    public TextMeshProUGUI upgradeText1;
    public TextMeshProUGUI upgradeText2;
    public TextMeshProUGUI upgradeText3;
    public TextMeshProUGUI upgradeDes1;
    public TextMeshProUGUI upgradeDes2;
    public TextMeshProUGUI upgradeDes3;
    public List<Upgrade> UpgradesList;
    public class Upgrade{
        public string Name{get; set;}
        public string Description{get; set;}
        public string Rarity{get; set;}
        public float Value{get; set;}
    
    }
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        ButtonSet();
    }
    public List<Upgrade> commonUpgrades = new List<Upgrade>{
        new Upgrade{Name="Health", Description="Increase player health by X", Rarity="Common", Value=20},
        new Upgrade{Name="Damage", Description="Increase player attack damage by X", Rarity="Common", Value=10},
        new Upgrade{Name="Attack speed", Description="Increase player attack speed by X%", Rarity="Common", Value=10f},
        new Upgrade{Name="Move speed", Description="Increase player move speed by X%", Rarity="Common", Value=10f},
        new Upgrade{Name="Exp Multiplier", Description="Increase exp multiplier by X%", Rarity="Common", Value=20f},
        new Upgrade{Name="Pierce", Description="Increase number of enemy bullet can pierce by X", Rarity="Common", Value=1},
        new Upgrade{Name="Magnetic Distance", Description="Increase magnetic distance by X%", Rarity="Common", Value=20},
        new Upgrade{Name="Enemy Speed", Description="Reduce enemy speed by X%", Rarity="Common", Value=10f},
        new Upgrade{Name="Durability", Description="Increase damage income reduction by X", Rarity="Common", Value=4f},
        new Upgrade{Name="Crit Chance", Description="Increase crit chance by X%", Rarity="Common", Value=10f},
        new Upgrade{Name="Crit Damage", Description="Increase crit damage deal by X%", Rarity="Common", Value=20f},
        new Upgrade{Name="Dodge Chance", Description="Increase dodge chance by X%", Rarity="Common", Value=5f},
        new Upgrade{Name="Luck", Description="Increase luck by X", Rarity="Common", Value=2f},
    };
    public List<Upgrade> rareUpgrades = new List<Upgrade>{
         new Upgrade{Name="Health", Description="Increase player health by X", Rarity="Rare", Value=40},
        new Upgrade{Name="Damage", Description="Increase player attack damage by X", Rarity="Rare", Value=20},
        new Upgrade{Name="Attack speed", Description="Increase player attack speed by X%", Rarity="Rare", Value=20f},
        new Upgrade{Name="Move speed", Description="Increase player move speed by X%", Rarity="Rare", Value=20f},
        new Upgrade{Name="Exp Multiplier", Description="Increase exp multiplier by X%", Rarity="Rare", Value=40f},
        new Upgrade{Name="Number of bullet", Description="Increase number of bullet player fire by X but reduce your bullet damage by 5", Rarity="Rare", Value=1},
        new Upgrade{Name="Pierce", Description="Increase number of enemy bullet can pierce by X", Rarity="Rare", Value=2},
        new Upgrade{Name="Magnetic Distance", Description="Increase magnetic distance by X%", Rarity="Rare", Value=40},
        new Upgrade{Name="Enemy Speed", Description="Reduce enemy speed by X%", Rarity="Rare", Value=20f},
        new Upgrade{Name="Durability", Description="Increase damage income reduction by X", Rarity="Rare", Value=8f},
        new Upgrade{Name="Crit Chance", Description="Increase crit chance by X%", Rarity="Rare", Value=20f},
        new Upgrade{Name="Crit Damage", Description="Increase crit damage deal by X%", Rarity="Rare", Value=40f},
        new Upgrade{Name="Dodge Chance", Description="Increase dodge chance by X%", Rarity="Rare", Value=10f},
        new Upgrade{Name="Luck", Description="Increase luck by X", Rarity="Rare", Value=4f},
    };
    public List<Upgrade> epicUpgrades = new List<Upgrade>{
         new Upgrade{Name="Health", Description="Increase player health by X", Rarity="Epic", Value=60},
        new Upgrade{Name="Damage", Description="Increase player attack damage by X", Rarity="Epic", Value=30},
        new Upgrade{Name="Attack speed", Description="Increase player attack speed by X%", Rarity="Epic", Value=30f},
        new Upgrade{Name="Move speed", Description="Increase player move speed by X%", Rarity="Epic", Value=30f},
        new Upgrade{Name="Exp Multiplier", Description="Increase exp multiplier by X%", Rarity="Epic", Value=60f},
        new Upgrade{Name="Number of bullet", Description="Increase number of bullet player fire by X but reduce your bullet damage by 10", Rarity="Epic", Value=2},
        new Upgrade{Name="Pierce", Description="Increase number of enemy bullet can pierce by X", Rarity="Epic", Value=3},
        new Upgrade{Name="Magnetic Distance", Description="Increase magnetic distance by X%", Rarity="Epic", Value=60},
        new Upgrade{Name="Enemy Speed", Description="Reduce enemy speed by X%", Rarity="Epic", Value=30f},
        new Upgrade{Name="Durability", Description="Increase damage income reduction by X", Rarity="Epic", Value=12f},
        new Upgrade{Name="Crit Chance", Description="Increase crit chance by X%", Rarity="Epic", Value=30f},
        new Upgrade{Name="Crit Damage", Description="Increase crit damage deal by X%", Rarity="Epic", Value=60f},
        new Upgrade{Name="Dodge Chance", Description="Increase dodge chance by X%", Rarity="Epic", Value=15f},
        new Upgrade{Name="Luck", Description="Increase luck by X", Rarity="Epic", Value=6f},
    };
    public List<Upgrade> legendaryUpgrades = new List<Upgrade>{
         new Upgrade{Name="Health", Description="Increase player health by X", Rarity="Legendary", Value=80},
        new Upgrade{Name="Damage", Description="Increase player attack damage by X", Rarity="Legendary", Value=40f},
        new Upgrade{Name="Attack speed", Description="Increase player attack speed by X%", Rarity="Legendary", Value=40f},
        new Upgrade{Name="Move speed", Description="Increase player move speed by X%", Rarity="Legendary", Value=40f},
        new Upgrade{Name="Exp Multiplier", Description="Increase exp multiplier by X%", Rarity="Legendary", Value=80f},
        new Upgrade{Name="Number of bullet", Description="Increase number of bullet player fire by X but reduce your bullet damage by 15", Rarity="Legendary", Value=3},
        new Upgrade{Name="Pierce", Description="Increase number of enemy bullet can pierce by X", Rarity="Legendary", Value=4},
        new Upgrade{Name="Magnetic Distance", Description="Increase magnetic distance by X%", Rarity="Legendary", Value=80},
        new Upgrade{Name="Enemy Speed", Description="Reduce enemy speed by X%", Rarity="Legendary", Value=40f},
        new Upgrade{Name="Durability", Description="Increase damage income reduction by X", Rarity="Legendary", Value=16f},
        new Upgrade{Name="Crit Chance", Description="Increase crit chance by X%", Rarity="Legendary", Value=40f},
        new Upgrade{Name="Crit Damage", Description="Increase crit damage deal by X%", Rarity="Legendary", Value=80f},
        new Upgrade{Name="Dodge Chance", Description="Increase dodge chance by X%", Rarity="Legendary", Value=20f},
        new Upgrade{Name="Luck", Description="Increase luck by X", Rarity="Legendary", Value=8f},
    };
    public List <Upgrade> uniqueUpgrades = new List<Upgrade>{
        new Upgrade{Name="Explosive Enemies", Description="Enemy explode deal X damage on death", Rarity="Unique", Value=30},
        new Upgrade{Name="Bounce Bullet", Description="Bullet will bounce between enemies 1 time for each 2 pierce instead of piercing", Rarity="Unique", Value=1},
        new Upgrade{Name="Heal Bullet", Description="Bullet will heal X for player on hit", Rarity="Unique", Value=5},
        new Upgrade{Name="Giant Bullet", Description="Bullet deal X damage more for each durability", Rarity="Unique", Value=2},
        new Upgrade{Name="Frozen Bullet", Description="Bullet slowdown the enemies for X% on hit and make them take more damage", Rarity="Unique", Value=30},
        new Upgrade{Name="Poison Bullet", Description="Bullet deal X% enemy's health minimum 1 per second for 5 seconds", Rarity="Unique", Value=3},
        new Upgrade{Name="Brumble Vest", Description="Reduce 50% damage income, deal X damage to enemies on touch", Rarity="Unique", Value=20},
        new Upgrade{Name="Trap Bullet", Description="Bullet will stop enemies from moving for X second on hit", Rarity="Unique", Value=3},
        new Upgrade{Name="Lucky Bullet", Description="Bullet will deal more X damage for each luck", Rarity="Unique", Value=2},
        new Upgrade{Name="Sniping Bullet", Description="Bullet will always crit, crit chance will increase crit damage instead", Rarity="Unique", Value=0},
    };
    public void ButtonSet(){
        if(UpgradesList!=null){
            for(int i=0;i<UpgradesList.Count;i++){
                addUpgrade(UpgradesList[i].Name, UpgradesList[i].Description, UpgradesList[i].Rarity, UpgradesList[i].Value);
            }
        }
        UpgradesList = new List<Upgrade>();
        if(_gameManager._level%10==0 && _gameManager._level<=30){
            for(int x=0;x<3;x++){
                int t=UnityEngine.Random.Range(0, uniqueUpgrades.Count);
                UpgradesList.Add(uniqueUpgrades[t]);
                uniqueUpgrades.RemoveAt(t);
            }
        }
        else{
            for (int i = 0; i < 3; i++)
            {
                int weight = UnityEngine.Random.Range(0, 100-_gameManager._level*2-_gameManager.luck);
                if(weight > 60){
                    int t=UnityEngine.Random.Range(0, commonUpgrades.Count);
                    UpgradesList.Add(commonUpgrades[t]);
                    commonUpgrades.RemoveAt(t);
                } else if(weight > 30){
                    int t=UnityEngine.Random.Range(0, rareUpgrades.Count);
                    UpgradesList.Add(rareUpgrades[t]);
                    rareUpgrades.RemoveAt(t);
                } else if(weight > 10){
                    int t=UnityEngine.Random.Range(0, epicUpgrades.Count);
                    UpgradesList.Add(epicUpgrades[t]);
                    epicUpgrades.RemoveAt(t);
                } else{
                    int t=UnityEngine.Random.Range(0, legendaryUpgrades.Count);
                    UpgradesList.Add(legendaryUpgrades[t]);
                    legendaryUpgrades.RemoveAt(t);
                }
                Debug.Log(UpgradesList[i].Description);
            }
        }
         // Setting text
        upgradeText1.text = UpgradesList[0].Name;
        upgradeText2.text = UpgradesList[1].Name;
        upgradeText3.text = UpgradesList[2].Name;

        // Replacing the X with increase value
        upgradeDes1.text = UpgradesList[0].Description.Replace("X", UpgradesList[0].Value.ToString());
        upgradeDes2.text = UpgradesList[1].Description.Replace("X", UpgradesList[1].Value.ToString());
        upgradeDes3.text = UpgradesList[2].Description.Replace("X", UpgradesList[2].Value.ToString());
        // Setting color of the buttons
        Dictionary<string, Color> rarityColors = new Dictionary<string, Color>();
        rarityColors.Add("Common", new Color(1, 1, 1, 1));
        rarityColors.Add("Rare", new Color(0.5f, 1f, 0.5f, 1));
        rarityColors.Add("Epic", new Color(0.75f, 0.25f, 0.75f, 1));
        rarityColors.Add("Legendary", new Color(1, 0.92f, 0.016f, 1));
        rarityColors.Add("Unique", new Color(0.8f, 0, 0, 1));


        UpgradeButton1.color = rarityColors[UpgradesList[0].Rarity];
        UpgradeButton2.color = rarityColors[UpgradesList[1].Rarity];
        UpgradeButton3.color = rarityColors[UpgradesList[2].Rarity];

    }
    private void addUpgrade(string name, string description, string rarity, float value)
    {
        if(rarity=="Common"){
            commonUpgrades.Add(new Upgrade{Name=name, Description=description, Rarity=rarity, Value=value});
        } else if(rarity=="Rare"){
            rareUpgrades.Add(new Upgrade{Name=name, Description=description, Rarity=rarity, Value=value});
        } else if(rarity=="Epic"){
            epicUpgrades.Add(new Upgrade{Name=name, Description=description, Rarity=rarity, Value=value});
        } else if(rarity=="Legendary"){
            legendaryUpgrades.Add(new Upgrade{Name=name, Description=description, Rarity=rarity, Value=value});
        }
        else{
            uniqueUpgrades.Add(new Upgrade{Name=name, Description=description, Rarity=rarity, Value=value});
        }
    }
    public void RerollButtonClicked()
    {
        ButtonSet();
        _gameManager._rerollCount--;
        if(_gameManager._rerollCount==0){
            rerollButton.SetActive(false);
        }
    }
    public void UpgradeButton1Clicked()
    {
        if(UpgradesList[0].Rarity=="Unique"){
            addUniqueUpgrade(UpgradesList[0].Description.Replace("X", UpgradesList[0].Value.ToString()));
            UpgradeChosen(UpgradesList[0].Name, UpgradesList[0].Value);
            UpgradesList.RemoveAt(0);

        }
        
        else UpgradeChosen(UpgradesList[0].Name, UpgradesList[0].Value);
        Time.timeScale = 1;
        upgradePanel.SetActive(false);
    }
    public void UpgradeButton2Clicked()
    {
        if(UpgradesList[1].Rarity=="Unique"){
            addUniqueUpgrade(UpgradesList[1].Description.Replace("X", UpgradesList[1].Value.ToString()));
            UpgradeChosen(UpgradesList[1].Name, UpgradesList[1].Value);
            UpgradesList.RemoveAt(1);
        }
        else UpgradeChosen(UpgradesList[1].Name, UpgradesList[1].Value);
        Time.timeScale = 1;
        upgradePanel.SetActive(false);
    }
    public void UpgradeButton3Clicked()
    {
        if(UpgradesList[2].Rarity=="Unique"){
            addUniqueUpgrade(UpgradesList[2].Description.Replace("X", UpgradesList[2].Value.ToString()));
            UpgradeChosen(UpgradesList[2].Name, UpgradesList[2].Value);
            UpgradesList.RemoveAt(2);
        }
        else UpgradeChosen(UpgradesList[2].Name, UpgradesList[2].Value);
        Time.timeScale = 1;
        upgradePanel.SetActive(false);
    }
     public void UpgradeChosen(string Upgrade_chosen, float increase)
    {
        _gameManager._expBar -= _gameManager._level*2;
        _gameManager._level++;
        Time.timeScale = 1;
        if (Upgrade_chosen == "Health")
        {
            _gameManager._maxPlayerHealth += increase;
            _gameManager._playerHealth += increase;
        }
        else if (Upgrade_chosen == "Damage")
        {
            _gameManager.damage += increase;
        }
        else if (Upgrade_chosen == "Attack speed")
        {
            _gameManager._shootRate -= (_gameManager._shootRate * increase) / 100;
            if(_gameManager._shootRate < 0.1f) {
                _gameManager._shootRate = 0.1f;

            }
        }
        else if (Upgrade_chosen == "Move speed")
        {   
            _gameManager._playerSpeed += (_gameManager._playerSpeed * increase) / 100;
        }
        else if (Upgrade_chosen == "Exp Multiplier")
        {
            _gameManager._expMultiplier += (_gameManager._expMultiplier * increase) / 100;
        }
        else if (Upgrade_chosen == "Number of bullet")
        {
            _gameManager.numberShoots += increase;
            _gameManager.damage -= 5*increase;
        }
        else if (Upgrade_chosen == "Magnetic Distance")
        {
            _gameManager.magneticDistance += (_gameManager.magneticDistance * increase) / 100;
        }
        else if (Upgrade_chosen == "Pierce")
        {
            _gameManager.pierceNumber += increase;
        }
        else if (Upgrade_chosen == "Enemy Speed")
        {
            _gameManager._enemySpeed -= (_gameManager._enemySpeed * increase) / 100;
        }
        else if (Upgrade_chosen == "Durability")
        {
            _gameManager._durability += increase;
        }
        else if (Upgrade_chosen == "Crit Chance")
        {
            _gameManager.critChance += increase;
        }
        else if (Upgrade_chosen == "Crit Damage")
        {
            _gameManager.critDamage += increase;
        }
        else if (Upgrade_chosen == "Dodge Chance")
        {
            _gameManager.dodgeChance += increase;
        }
        else if (Upgrade_chosen == "Luck")
        {
            _gameManager.luck += Convert.ToInt32(increase);
        }
        else if (Upgrade_chosen == "Explosive Enemies")
        {
            _gameManager.explode = true;
        }
        else if (Upgrade_chosen == "Bounce Bullet")
        {
            _gameManager.isBounce = true;
        }
        else if (Upgrade_chosen == "Heal Bullet")
        {
            _gameManager.isHeal = true;
        }
        else if (Upgrade_chosen == "Giant Bullet")
        {
            _gameManager.isGiant = true;
        }
        else if (Upgrade_chosen == "Trap Bullet")
        {
            _gameManager.isTrap = true;
        }
        else if (Upgrade_chosen == "Frozen Bullet")
        {
            _gameManager.isCold = true;
        }
        else if (Upgrade_chosen == "Poison Bullet")
        {
            _gameManager.isPoison = true;
        }
        else if (Upgrade_chosen == "Brumble Vest")
        {
            _gameManager.brumbleVest = true;
        }
        else if (Upgrade_chosen == "Lucky Bullet")
        {
            _gameManager.luckyBullet = true;
        }
        else if (Upgrade_chosen == "Sniping Bullet")
        {
            _gameManager.snipingBullet = true;
            _gameManager.critChance += 95;
        }
        _gameManager.inUpgrade = false;
        _gameManager.isPaused = false;
        ButtonSet();
    }
    private void addUniqueUpgrade(string description)
    {
        if(_gameManager._level<10) _gameManager._uniqueUpgrade1 = description;
        else if(_gameManager._level<20) _gameManager._uniqueUpgrade2 = description;
        else if(_gameManager._level <30) _gameManager._uniqueUpgrade3 = description;
        else _gameManager._uniqueUpgrade4 = description;
    }
    
}
