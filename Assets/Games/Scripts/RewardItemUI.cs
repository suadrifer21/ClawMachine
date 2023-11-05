using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RewardItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rewardName;
    [SerializeField] private Image rewardIcon;

    public void SetRewardItem(string name, Sprite sprite)
    {
        rewardName.text = name;
        rewardIcon.sprite = sprite;
        gameObject.SetActive(true);
    }
}
