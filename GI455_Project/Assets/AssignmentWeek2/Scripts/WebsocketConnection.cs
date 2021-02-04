using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using UnityEngine.UI;
using TMPro;

namespace ProgramChat
{
    public class WebsocketConnection : MonoBehaviour
    {
        private WebSocket myWebsocket;

        public GameObject connectUI;
        public GameObject chatUI;

        public InputField ipField;
        public InputField portField;
        public InputField usernameField;

        public InputField messageField;
        public TextMeshProUGUI chatText;

        // Start is called before the first frame update
        void Start()
        {
            connectUI.SetActive(true);
            chatUI.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void ConnectWebsocket()
        {
            myWebsocket = new WebSocket("ws://" + ipField.text + ":" + portField.text + "/"); // connect to server

            myWebsocket.OnMessage += OnMessage;

            myWebsocket.Connect();

            if(myWebsocket.ReadyState == WebSocketState.Open) // activate chat ui on successful connection
            {
                connectUI.SetActive(false);
                chatUI.SetActive(true);
            }

        }

        public void SendMessage() //Send Message to server
        {
            if(myWebsocket.ReadyState == WebSocketState.Open)
            {
                myWebsocket.Send(usernameField.text + ": " + messageField.text);
                messageField.text = "";
            }
        }

        private void OnDestroy()
        {
            if(myWebsocket != null)
            {
                myWebsocket.Close();
            }
        }

        public void OnMessage(object sender, MessageEventArgs messageEventArgs) // Recieved Broadcast Message
        {
            string[] messages = messageEventArgs.Data.Split(':'); // 0 = username , 1 = message

            if (messages[0] == usernameField.text) //check if sent by user
            {
                Debug.Log("msg sent : " + messages[1]);
                chatText.text += "<align=right> " + messages[1] + " <br>";
            }
            else
            {
                Debug.Log("msg recieved : " + messages[1]);
                chatText.text += "<align=left> " + messageEventArgs.Data + " <br>";
            }
        }
    }

}

