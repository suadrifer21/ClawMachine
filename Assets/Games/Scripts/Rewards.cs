using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{
    [SerializeField] private List<Sprite> rewardsSprite;
    [SerializeField] GameObject rewardPanel;
    [SerializeField] Transform rewardItemsParent;
    private List<GameObject> grabbedRewards;

    public void ShowRewardsPanel(List<GameObject> rewards)
    {
        rewardPanel.SetActive(true);
        Time.timeScale = 0;
        grabbedRewards = rewards;
        for(int i = 0; i<grabbedRewards.Count;i++)
        {
            Sprite sprite = null;
            foreach(Sprite s in rewardsSprite)
            {
                if (s.name.Equals(grabbedRewards[i].name))
                    sprite = s;
            }
            rewardItemsParent.GetChild(i).GetComponent<RewardItemUI>().SetRewardItem(grabbedRewards[i].name, sprite);
        }
    }

    public void HideRewardsPanel()
    {
        rewardPanel.SetActive(false);
        Time.timeScale = 1;
        for (int i = 0; i <rewardItemsParent.childCount; i++)
        {
            rewardItemsParent.GetChild(i).gameObject.SetActive(false);
        }
    }
}
