using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;
using TMPro;
using System.Runtime.CompilerServices;

public class UiSys : SystemBase
{

    Dictionary<int, Ui> m_Sid2Ui = new Dictionary<int, Ui>();
    List<Ui> m_ActiveUi = new List<Ui>();
    Vector2 m_MousePosition;
    private GameObject m_PriceBroad;
    private TextMeshPro m_priceText;

    public void StartCreateUi()
    {
        GameObject Canvas;
        Canvas = GameObject.Find("Canvas");
        if (Canvas == null)
        {
            Debug.LogError("找不到Canvas");
            return;
        }
        UnityEngine.UI.Button addButtion = Canvas.transform.Find("ChickenHead").GetComponent<UnityEngine.UI.Button>();
        if (addButtion == null)
        {
            Debug.LogError("找不到Add按钮");
            return;
        }
        Debug.Log("找到Add按钮");
        addButtion.onClick.AddListener(OnAddButtonClick);
        RectTransform addRect = addButtion.GetComponent<RectTransform>();
        Debug.Log($"Add runtime state: interactable={addButtion.interactable}, active={addButtion.gameObject.activeInHierarchy}, rect={addRect.rect}, corners={string.Join(" | ", GetWorldCorners(addRect))}");
        m_MousePosition = ScreenHelper.GetMouseWorldPos();
    }
    public void OnAddButtonClick()
    {
        Debug.Log("Add按钮被点击");
        ShoppingSys shoppingSys = luncher.GetSystem<ShoppingSys>();
        shoppingSys.BuyAnimal("chicken");
    }
    public void CreateUi(EntityBase entity)
    {
        string name = VarHelper.GetString(entity.GetProp(PropId.Name));
        string path = $"Animal/{name}";//这个地方局限在animal了
        ResourceSys resourceSys = luncher.GetSystem<ResourceSys>();
        GameObject prefab = resourceSys.GetRes(path);
        if (prefab == null)
        {
            Debug.LogError("获取预制体失败");
            return;
        }
        prefab = UnityEngine.Object.Instantiate(prefab);
        Ui ui = new Ui();
        ui.OnCreate(entity, prefab);
        m_Sid2Ui.Add(ui.InstanceId, ui);
        m_ActiveUi.Add(ui);
    }
    public Ui GetUi(int s_id)
    {
        if (!m_Sid2Ui.ContainsKey(s_id))
        {
            return null;
        }
        return m_Sid2Ui[s_id];
    }
    //todo:和animal逻辑绑定，不同的动物调用不同的预制体，对表现层进行操作
    public override void Update()
    {
        //if (Time.frameCount % 120 == 0)
        //{
        //    Debug.Log($"UI heartbeat: frame={Time.frameCount}, focused={Application.isFocused}, mouse={Input.mousePosition}");
        //}
        if (Input.GetMouseButtonDown(0))
        {
            LogPointerDiagnostics();
        }
        m_MousePosition = ScreenHelper.GetMouseWorldPos();
        Ui currentUi = null;
        for (int i = 0; i < m_ActiveUi.Count; i++)
        {//对于所有的ui进行更新
            m_ActiveUi[i].OnUpdate();
            Ui ui = m_ActiveUi[i];
            if (ui.IsPointInCollider(m_MousePosition))
            {
                //Debug.Log($"Mouse is over UI: sid={ui.InstanceId}, name={VarHelper.GetString(ui.m_Entity.GetProp(PropId.Name))}");
                currentUi = ui;
                break;
            }
        }
        if (currentUi != null)
        {
            //todo:鼠标悬停在ui上，显示动物价格
            string name = VarHelper.GetString(currentUi.m_Entity.GetProp(PropId.Name));
            if (name != null)
            {
                int price = GetAnimalSellPrice(name);
                ShowAnimalPrice(currentUi, (int)price);
            }

            //SellAnimals(currentUi);
        }
        else
        {
            HideAnimalPrice();
        }
    }
    private void LogPointerDiagnostics()
    {
        Debug.Log($"Mouse click: screen={Input.mousePosition}, screenSize={Screen.width}x{Screen.height}, eventSystem={EventSystem.current != null}");
        if (EventSystem.current == null)
        {
            return;
        }

        PointerEventData pointer = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, results);
        Debug.Log($"Mouse raycast count: {results.Count}");
        for (int resultIndex = 0; resultIndex < results.Count; resultIndex++)
        {
            Debug.Log($"Mouse raycast [{resultIndex}]: {results[resultIndex].gameObject.name}, module={results[resultIndex].module.GetType().Name}, depth={results[resultIndex].depth}");
        }
    }

    private static string[] GetWorldCorners(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        string[] values = new string[corners.Length];
        for (int cornerIndex = 0; cornerIndex < corners.Length; cornerIndex++)
        {
            values[cornerIndex] = corners[cornerIndex].ToString("F1");
        }
        return values;
    }
    //private void SellAnimals(Ui ui)
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        string name = VarHelper.GetString(ui.m_Entity.GetProp(PropId.Name));
    //        if (name != null)
    //        {
    //            int price = GetAnimalSellPrice(name);
    //            ShoppingSys shoppingSys = luncher.GetSystem<ShoppingSys>();
    //            shoppingSys.AddCoin(price);
    //            AnimalSys animalSys = luncher.GetSystem<AnimalSys>();
    //            animalSys.KillAnimalBySid(ui.InstanceId);
    //        }
    //    }
    //}

    public void DestroyUi(int s_id)
    {
        if (!m_Sid2Ui.ContainsKey(s_id))
        {
            Debug.LogError($"不存在sid为{s_id}的ui");
            return;
        }
        Ui ui = m_Sid2Ui[s_id];
        m_ActiveUi.Remove(ui);
        m_Sid2Ui.Remove(s_id);
        ui.OnDestroy();
    }

    public void ShowCoin(int price)
    {
        GameObject Canvas = GameObject.Find("Canvas");
        TextMeshProUGUI coinText = Canvas.transform.Find("Coin").GetComponent<TextMeshProUGUI>();
        if (coinText != null)
        {
            coinText.text = price.ToString();
        }
        else
        {
            Debug.LogError("找不到Coin文本");
        }
    }

    public void ShowAnimalPrice(Ui ui, int price)
    {
        if(m_PriceBroad == null)
        {
            GameObject pricePrefab = Resources.Load<GameObject>("Animal/PriceBroad");
            if (pricePrefab == null)
            {
                Debug.LogError("找不到 Animal/PriceBroad.prefab");
                return;
            }
            m_PriceBroad = Object.Instantiate(pricePrefab);
            m_priceText = pricePrefab.transform.Find("PriceText").GetComponent<TextMeshPro>();
        }
        if (m_priceText != null)
        {
            m_priceText.text = $"{price}$";
        }
        else
        {
            Debug.LogError("找不到Price文本");
        }
        m_PriceBroad.transform.position = ui.m_Renderer.transform.position + new Vector3(0, 0.5f, 0);
        m_PriceBroad.SetActive(true);
    }
    public void HideAnimalPrice()
    {
        if (m_PriceBroad != null)
        {
            m_PriceBroad.SetActive(false);
        }

        //m_PriceUi = null;
    }
    private int GetAnimalSellPrice(string name)
    {
        AnimalSys animalSys = luncher.GetSystem<AnimalSys>();
        return animalSys.GetAnimalSellPrice(name);
    }
}
