using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIWaveManager : MonoBehaviour
{

    Dropdown uiNnumberOfWaves;
    Dropdown uiWaveToEdit;
    Dropdown uiNumberOfSubwaves;
    Dropdown uiSubwaveToEdit;

    Button wavePanelButton;
    Button turretPanelButton;

    Text SelectedWaveAndSubwave;

    Text[] subwaveDuration;
    Button saveSubwaveDuration;

    GameObject wavePanel;
    GameObject turretPanel;


    Turret turret;
    GameObject mainTurret;
    GameObject currentSelectedTurret;

    UIManager uiManager;
    // Dropdown uiPercentageToChangeAt;
    // Start is called before the first frame update
    void Start()
    {
        findInputs();
        setupDropdowns();
        setupButtons();
     
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        SelectedWaveAndSubwave = GameObject.Find("selected wave and sub-wave Text").GetComponent<Text>();

        numberOfWavesChanged();
        numberOfSubwavesChanged();
    }


    void findInputs()
    {
        uiNnumberOfWaves = GameObject.Find("number Of Waves Input").GetComponent<Dropdown>();
        uiWaveToEdit = GameObject.Find("wave to edit Input").GetComponent<Dropdown>();
        uiNumberOfSubwaves = GameObject.Find("number Of sub-waves Input").GetComponent<Dropdown>();
        uiSubwaveToEdit = GameObject.Find("sub-wave to edit Input").GetComponent<Dropdown>();
        //uiPercentageToChangeAt = GameObject.Find("number Of Waves Input").GetComponent<Dropdown>();

        wavePanel = GameObject.Find("Wave Panel");
        turretPanel = GameObject.Find("turret panel");

        wavePanelButton = GameObject.Find("to Wave Settings").GetComponent<Button>();
        turretPanelButton = GameObject.Find("to Turret select").GetComponent<Button>();
        
        subwaveDuration = GameObject.Find("Subwave Duration Input").GetComponentsInChildren<Text>();
        saveSubwaveDuration = GameObject.Find("Save subwave duration").GetComponent<Button>();
    }

    void setupDropdowns()
    {
        uiNnumberOfWaves.onValueChanged.AddListener(delegate
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

        saveSubwaveDuration.onClick.AddListener(delegate
        {
            uiManager.saveDurationSettings(float.Parse(subwaveDuration[1].text));
        });
    }



    void toWaveSettings()
    {
        wavePanel.SetActive(true);
        turretPanel.SetActive(false);
    }

    void toTurretSelect()
    {
        wavePanel.SetActive(false);
        turretPanel.SetActive(true);
    }

    void numberOfWavesChanged() 
    {
        List<string> uiWaveToEditOptions = new List<string> { };  
        uiWaveToEdit.ClearOptions();

        for (int i = 0; i < uiNnumberOfWaves.value +1; i++)
        {
            uiWaveToEditOptions.Add((i + 1).ToString());
        }
        //uiManager.clearInputFields();
        uiWaveToEdit.AddOptions(uiWaveToEditOptions);
        setAllTurrets();
    }

    void waveToEditChanged()
    {
        uiManager.waveNum = uiWaveToEdit.value;
        uiManager.clearInputFields();
        //Debug.Log("current wave: " + uiManager.waveNum);
        SelectedWaveAndSubwave.text = "Wave " + (uiWaveToEdit.value + 1) + " selected," + "\n" +"subwave "+(uiSubwaveToEdit.value +1) +" Selected";
        setAllTurrets();
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
        setAllTurrets();
    }

    void subwaveToEditChanged()
    {
        uiManager.subwaveNum = uiSubwaveToEdit.value;
        uiManager.clearInputFields();
        SelectedWaveAndSubwave.text = "Wave " + (uiWaveToEdit.value + 1) + " selected," + "\n" + "subwave " + (uiSubwaveToEdit.value + 1) + " Selected";
        setAllTurrets();
    }

    void setAllTurrets()
    {
         GameObject[] turrets = GameObject.FindGameObjectsWithTag("turret Main");
        for (int i = 0; i < turrets.Length; i++)
        {
            turrets[i].GetComponent<turretSubwaveStorage>().totalWaveCount = uiNnumberOfWaves.value + 1;
            turrets[i].GetComponent<turretSubwaveStorage>().SubwaveCount[uiWaveToEdit.value] = uiNumberOfSubwaves.value + 1;
        }
    }

}
