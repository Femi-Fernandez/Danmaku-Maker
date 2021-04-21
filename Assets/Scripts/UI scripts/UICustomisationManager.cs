using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Android;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UICustomisationManager : MonoBehaviour
{

    Dropdown uiNumberOfWaves;
    Dropdown uiWaveToEdit;
    Dropdown uiNumberOfSubwaves;
    Dropdown uiSubwaveToEdit;

    Button wavePanelButton;
    Button turretPanelButton;
    Button bulletPanelButton;

    Text SelectedWaveAndSubwave;

    Text[] subwaveDuration;
    Button saveSubwaveDuration;

    GameObject wavePanel;
    GameObject turretPanel;
    GameObject bulletPanel;
    Button saveWaveInfo;

    Turret turret;
    GameObject mainTurret;
    GameObject currentSelectedTurret;


    Button bul_1_blue;
    Button bul_1_pink;
    Button bul_2_blue;
    Button bul_2_pink;
    Button bul_3_blue;
    Button bul_3_pink;
    Button bul_4_blue;
    Button bul_4_pink;

    UIManager uiManager;
    // Dropdown uiPercentageToChangeAt;

    public AnalyticsCommands AC;
    // Start is called before the first frame update
    void Awake()
    {
        findInputs();
        setupDropdowns();
        setupButtons();

        setupBulletChangeButtons();

        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        SelectedWaveAndSubwave = GameObject.Find("selected wave and sub-wave Text").GetComponent<Text>();

        numberOfWavesChanged();
        numberOfSubwavesChanged();


        wavePanel.SetActive(false);
        turretPanel.SetActive(true);
        bulletPanel.SetActive(false);
    }


    void findInputs()
    {
        uiNumberOfWaves = GameObject.Find("number Of Waves Input").GetComponent<Dropdown>();
        uiWaveToEdit = GameObject.Find("wave to edit Input").GetComponent<Dropdown>();
        uiNumberOfSubwaves = GameObject.Find("number Of sub-waves Input").GetComponent<Dropdown>();
        uiSubwaveToEdit = GameObject.Find("sub-wave to edit Input").GetComponent<Dropdown>();

        wavePanel = GameObject.Find("Wave Panel");
        turretPanel = GameObject.Find("turret panel");
        bulletPanel = GameObject.Find("bullet panel");


        wavePanelButton = GameObject.Find("to Wave Settings").GetComponent<Button>();
        turretPanelButton = GameObject.Find("to Turret select").GetComponent<Button>();
        bulletPanelButton = GameObject.Find("to Bullet select").GetComponent<Button>();

        subwaveDuration = GameObject.Find("Subwave Duration Input").GetComponentsInChildren<Text>();
        saveSubwaveDuration = GameObject.Find("Save subwave duration").GetComponent<Button>();
        saveWaveInfo = GameObject.Find("Save phase info").GetComponent<Button>();

        bul_1_blue = GameObject.Find("Bullet 1 blue button").GetComponent<Button>();
        bul_1_pink = GameObject.Find("Bullet 1 pink button").GetComponent<Button>();
        bul_2_blue = GameObject.Find("Bullet 2 blue button").GetComponent<Button>();
        bul_2_pink = GameObject.Find("Bullet 2 pink button").GetComponent<Button>();
        bul_3_blue = GameObject.Find("Bullet 3 blue button").GetComponent<Button>();
        bul_3_pink = GameObject.Find("Bullet 3 pink button").GetComponent<Button>();
        bul_4_blue = GameObject.Find("Bullet 4 blue button").GetComponent<Button>();
        bul_4_pink = GameObject.Find("Bullet 4 pink button").GetComponent<Button>();
    }

    void setupDropdowns()
    {
        uiNumberOfWaves.onValueChanged.AddListener(delegate
        {
            numberOfWavesChanged();
        }
        );

        uiWaveToEdit.onValueChanged.AddListener(delegate
        {
            waveToEditChanged();
        }
        );

        uiNumberOfSubwaves.onValueChanged.AddListener(delegate
        {
            numberOfSubwavesChanged();
        }
        );

        uiSubwaveToEdit.onValueChanged.AddListener(delegate
        {
            subwaveToEditChanged();
        }
        );

    }

    void setupButtons()
    {
        wavePanelButton.onClick.AddListener(delegate
        {
            toWaveSettings();
        });

        turretPanelButton.onClick.AddListener(delegate
        {
            toTurretSelect();
        });

        bulletPanelButton.onClick.AddListener(delegate
        {
            toBulletSelect();
        });

        saveSubwaveDuration.onClick.AddListener(delegate
        {
            AC.saveDurationPressed();
            if (subwaveDuration[1].text != "")
            {
                uiManager.saveDurationSettings(float.Parse(subwaveDuration[1].text));
            }
           
        });

        saveWaveInfo.onClick.AddListener(delegate
        {
            setAllTurrets();
        });
    }

    void setupBulletChangeButtons() 
    {
        bul_1_blue.onClick.AddListener(delegate
        {
            bulletTypeChanged(0);
        });

        bul_1_pink.onClick.AddListener(delegate
        {
            bulletTypeChanged(1);
        });

        bul_2_blue.onClick.AddListener(delegate
        {
            bulletTypeChanged(2);
        });

        bul_2_pink.onClick.AddListener(delegate
        {
            bulletTypeChanged(3);
        });

        bul_3_blue.onClick.AddListener(delegate
        {
            bulletTypeChanged(4);
        });

        bul_3_pink.onClick.AddListener(delegate
        {
            bulletTypeChanged(5);
        });

        bul_4_blue.onClick.AddListener(delegate
        {
            bulletTypeChanged(6);
        });

        bul_4_pink.onClick.AddListener(delegate
        {
            bulletTypeChanged(7);
        });
    }

    void toWaveSettings()
    {
        wavePanel.SetActive(true);
        turretPanel.SetActive(false);
        bulletPanel.SetActive(false);
    }

    void toTurretSelect()
    {
        wavePanel.SetActive(false);
        turretPanel.SetActive(true);
        bulletPanel.SetActive(false);
    }

    void toBulletSelect()
    {
        wavePanel.SetActive(false);
        turretPanel.SetActive(false);
        bulletPanel.SetActive(true);
    }

    void bulletTypeChanged(int bulletNum)
    {

        uiManager.turret.bulletType = bulletNum;
    }

    void numberOfWavesChanged() 
    {
        List<string> uiWaveToEditOptions = new List<string> { };  
        uiWaveToEdit.ClearOptions();

        for (int i = 0; i < uiNumberOfWaves.value +1; i++)
        {
            uiWaveToEditOptions.Add((i + 1).ToString());
        }
        //uiManager.clearInputFields();
        setAllTurrets();

        uiWaveToEdit.AddOptions(uiWaveToEditOptions);
        uiWaveToEdit.value = 0;
        uiManager.toggleWaveSelect(uiNumberOfWaves.value);
        //setAllTurrets();
    }

    void waveToEditChanged()
    {
        uiManager.waveNum = uiWaveToEdit.value;
        uiManager.clearInputFields();
        //Debug.Log("current wave: " + uiManager.waveNum);
        SelectedWaveAndSubwave.text = "Phase " + (uiWaveToEdit.value + 1) + " selected," + "\n" + "subPhase " + (uiSubwaveToEdit.value +1) +" Selected";
        //setAllTurrets();
    }

    void numberOfSubwavesChanged()
    {
        uiSubwaveToEdit.ClearOptions();
        List<string> uiSubwaveToEditOptions = new List<string> { };

        for (int i = 0; i < uiNumberOfSubwaves.value + 1; i++)
        {
            uiSubwaveToEditOptions.Add((i + 1).ToString());
        }
        //uiManager.clearInputFields();
        if (uiNumberOfSubwaves.value == 0)
        {
            uiManager.subwaveNum = 0;
        }
        uiSubwaveToEdit.AddOptions(uiSubwaveToEditOptions);
        uiSubwaveToEdit.value = 0;
        setAllTurrets();
        //setAllTurrets();
    }

    void subwaveToEditChanged()
    {
        uiManager.subwaveNum = uiSubwaveToEdit.value;  
        SelectedWaveAndSubwave.text = "Phase " + (uiWaveToEdit.value + 1) + " selected," + "\n" + "subPhase " + (uiSubwaveToEdit.value + 1) + " Selected";
        uiManager.clearInputFields();
        //setAllTurrets();
    }

    void setAllTurrets()
    {
         GameObject[] turrets = GameObject.FindGameObjectsWithTag("turret Main");
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].GetComponent<turretSubwaveStorage>().totalWaveCount = uiNumberOfWaves.value + 1;
            turrets[i].GetComponent<turretSubwaveStorage>().SubwaveCount[uiWaveToEdit.value] = uiNumberOfSubwaves.value + 1;
        }
    }

    public void setWaveAndSubwaveValues(int numOfWaves, int numOfSubwaves) 
    {
        uiNumberOfWaves.value = numOfWaves-1;
        uiNumberOfSubwaves.value = numOfSubwaves-1;
    }
}
