using System;
using _GameAssets.Scripts.Enums;
using _GameAssets.Scripts.Gameplay.Player;
using UnityEngine;

public class PlayerStateUI : MonoBehaviour

{
[Header("References")]
[SerializeField] private PlayerController playerController;
[SerializeField] private RectTransform playerWalkingTransform;
[SerializeField] private RectTransform playerSlidingTransform;

[Header("Sprites")]
[SerializeField] private Sprite playerWalkingActiveSprite;
[SerializeField] private Sprite playerWalkingPassiveSprite;

[SerializeField] private Sprite playerSlidingActiveSprite;
[SerializeField] private Sprite playerSlidingPassiveSprite;

private void Start()
{
    playerController.OnPlayerStateChanged += PlayerController_OnPlayerStateChanged;
}

private void PlayerController_OnPlayerStateChanged(PlayerState state)
{
    
}

}
