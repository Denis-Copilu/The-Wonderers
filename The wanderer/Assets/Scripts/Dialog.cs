using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Windows.Input;
public class Dialog : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    public bool playerInRange;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //////////////////////////////
       

        //////////////////////
        if (Input.GetKeyDown(KeyCode.I) && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.CompareTag("Player_1"))
        {
            Debug.Log("Player in range.");
        }*/
        playerInRange = true;
        Debug.Log("Player in range.");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        playerInRange = false;
        dialogBox.SetActive(false);
        Debug.Log("Player left range");
        /*if (other.CompareTag("Player_1"))
        {
            Debug.Log("Player left range");
        }*/
    }
    bool debounceButton(bool state)
    {
        bool stateNow = Input.GetKey(KeyCode.I);
        if (state != stateNow)
        {
            stateNow = Input.GetKey(KeyCode.I);
        }
        return stateNow;

    }
}
