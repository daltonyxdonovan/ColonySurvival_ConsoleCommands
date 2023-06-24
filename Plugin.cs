using BepInEx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ColonySurvival_ConsoleCommands
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        GameObject panel;
        TextMeshProUGUI pos_text;
        bool panel_setup = false;


        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

        }


        public void UpdatePos()
        {
            if (PlayerController.LastRealPosition == null)
            {
                Log("Player position is null.");
                return;
            }
            Vector3 pos = PlayerController.LastRealPosition;
            //make pos only ints
            pos.x = Mathf.Round(pos.x);
            pos.y = Mathf.Round(pos.y);
            pos.z = Mathf.Round(pos.z);
            panel.GetComponent<TextMeshProUGUI>().text = $"X: {pos.x}\nY: {pos.y}\nZ: {pos.z}";
        }

        public void Log(string message)
        {
            Logger.LogInfo(message);
        }

        public void Update()
        {
            if (panel == null)
            {
                panel = new GameObject("Panel_Daltonyx");
                panel.AddComponent<RectTransform>();
                panel.AddComponent<CanvasRenderer>();
                panel.AddComponent<Canvas>();
                panel.AddComponent<TextMeshProUGUI>();
                panel.GetComponent<TextMeshProUGUI>().text = "DALTONYX";
                panel.GetComponent<TextMeshProUGUI>().fontSize = 20;
                panel.GetComponent<TextMeshProUGUI>().color = Color.white;
                panel.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Right;
                panel.GetComponent<TextMeshProUGUI>().font = GameManager.Instance.AverageSansTMP;
                panel.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                Log("Panel created for console commands.");
            }

            

            if (!panel_setup)
            {
                // Get the size of the screen
                Vector2 screenSize = new Vector2(Screen.width, Screen.height);

                // Calculate the position of the text based on the screen size
                Vector2 panelSize = panel.GetComponent<RectTransform>().sizeDelta;
                Vector2 panelPosition = new Vector2(screenSize.x - panelSize.x, screenSize.y - panelSize.y);

                // Set the position of the text
                panel.GetComponent<RectTransform>().anchoredPosition = panelPosition;
                panel_setup = true;
            }

            UpdatePos();
            
        }
    }
}
