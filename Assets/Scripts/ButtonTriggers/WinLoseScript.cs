using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class WinLoseScript : MonoBehaviour
{
    public GameObject IO_Room_Text;
    public GameObject CPU_Room_Text;
    public GameObject Memory_Room_Text;
    public GameObject InstructionText;
    public GameObject Bananas;
    public GameObject Peppers;
    List<string> ignoreList = new List<string> {"Blank", "IO Room Text", "CPU Room Text", "CPU:200", "CPU:400", "CPU:600", "BANANA TIME!", "Ew brocoli" };
    // Current turn number in the game
    // 0: I/O must press a button for CPU
    // 1: CPU must press a button for Memory
    // 2: Memory must press a button for CPU
    // 3: CPU must press a button for I/O
    // 4: Final answer
    // 5: Game over
    public int turnNumber = 0;
    TextMesh IO_Text_Obj;
    TextMesh CPU_Text_Obj;
    TextMesh Mem_Text_Obj;
    TextMeshProUGUI Popup_Text_Obj;

    private void Start()
    {
        if (IO_Room_Text != null && CPU_Room_Text != null && Memory_Room_Text != null)
        {
            IO_Text_Obj = IO_Room_Text.GetComponent<TextMesh>();
            CPU_Text_Obj = CPU_Room_Text.GetComponent<TextMesh>();
            Mem_Text_Obj = Memory_Room_Text.GetComponent<TextMesh>();
            Popup_Text_Obj = InstructionText.GetComponent<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        switch (turnNumber) {
            case 0:
                Popup_Text_Obj.text = "I/O, press a button to send an input and tell the number to the CPU";
                break;
            case 1:
                Popup_Text_Obj.text = "CPU, press a button matching the number given by Input and tell the new number to Memory";
                break;
            case 2:
                Popup_Text_Obj.text = "Memory, go to your library and find the book matching the number given by CPU, then press the matching button and tell the equation to CPU";
                break;
            case 3:
                Popup_Text_Obj.text = "CPU, solve the equation given by Memory and send the answer to Output";
                break;
            case 4:
                turnNumber++;
                if ((IO_Text_Obj.text == "IO: 100" && CPU_Text_Obj.text == "CPU: 1000") ||
                    (IO_Text_Obj.text == "IO: 200" && CPU_Text_Obj.text == "CPU: 2000") ||
                    (IO_Text_Obj.text == "IO: 300" && CPU_Text_Obj.text == "CPU: 3000"))
                {
                    IO_Text_Obj.text = "BANANA TIME!";
                    CPU_Text_Obj.text = "BANANA TIME!";
                    Mem_Text_Obj.text = "BANANA TIME!";
                    Bananas.SetActive(true);
                    Popup_Text_Obj.text = "Game won! Test your knowledge: Who provides the CPU with execution instructions?";
                }
                else
                {
                    Debug.Log("this was IO text->" + IO_Text_Obj.text);
                    Debug.Log("this was CPU text->" + CPU_Text_Obj.text);
                    IO_Text_Obj.text = "Ew peppers";
                    CPU_Text_Obj.text = "Ew peppers";
                    Mem_Text_Obj.text = "Ew peppers";
                    Peppers.SetActive(true);
                    Popup_Text_Obj.text = "Game lost. Test your knowledge: Who provides the CPU with execution instructions?";
                }
                break;
        }
    }

}
