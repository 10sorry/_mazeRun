using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class PortalBehavior : MonoBehaviour
{
    [SerializeField] private GameObject portalExit;
    [SerializeField] private GameObject portalEnter;
    [SerializeField] private Button teleportButton;
    [SerializeField] private TextMeshProUGUI tmpro;
    [SerializeField] private float alpha = 1f;
    private bool _playerIsOnPortalEnter = false;
    private bool _playerIsOnPortalExit = false;
    private bool _isBlinking = false;
    private ButtonScript _buttonScriptComponent;

    private const float SqrCloseDistance = 0.01f;

    private void Start()
    {
        tmpro.enabled = false;
        teleportButton.gameObject.SetActive(false);
        _buttonScriptComponent = teleportButton.GetComponent<ButtonScript>();
    }

    private void Update()
    {
        CheckPortalEnter();
        CheckPortalExit();
    }

    private void CheckPortalEnter()
    {
        Vector3 playerPosition = transform.position;
        Vector3 portalEnterPosition = GetAdjustedPosition(portalEnter.transform.position, playerPosition.y);

        bool isPlayerCloseToPortalEnter = IsPlayerCloseToPortal(playerPosition, portalEnterPosition);
        if (isPlayerCloseToPortalEnter && !_playerIsOnPortalEnter)
        {
            SetPortalEnterState(true);
            StartCoroutine(WaitForSpace(portalExit.transform.position));
        }
        else if (!isPlayerCloseToPortalEnter && _playerIsOnPortalEnter)
        {
            SetPortalEnterState(false);
        }
    }

    private void CheckPortalExit()
    {
        Vector3 playerPosition = transform.position;
        Vector3 portalExitPosition = GetAdjustedPosition(portalExit.transform.position, playerPosition.y);

        bool isPlayerCloseToPortalExit = IsPlayerCloseToPortal(playerPosition, portalExitPosition);
        if (isPlayerCloseToPortalExit && !_playerIsOnPortalExit)
        {
            SetPortalExitState(true);
            StartCoroutine(WaitForSpace(portalEnter.transform.position));
        }
        else if (!isPlayerCloseToPortalExit && _playerIsOnPortalExit)
        {
            SetPortalExitState(false);
        }
    }

    private void SetPortalEnterState(bool isActive)
    {
        _playerIsOnPortalEnter = isActive;
        tmpro.enabled = isActive;
        teleportButton.gameObject.SetActive(isActive);
        if (!isActive)
        {
            StopCoroutine(BlinkText());
        }
        else if (!_isBlinking)
        {
            StartCoroutine(BlinkText());
        }
    }

    private void SetPortalExitState(bool isActive)
    {
        _playerIsOnPortalExit = isActive;
        tmpro.enabled = isActive;
        teleportButton.gameObject.SetActive(isActive);
        if (!isActive)
        {
            StopCoroutine(BlinkText());
        }
        else if (!_isBlinking)
        {
            StartCoroutine(BlinkText());
        }
    }

    private Vector3 GetAdjustedPosition(Vector3 position, float y)
    {
        return new Vector3(position.x, y, position.z);
    }

    private bool IsPlayerCloseToPortal(Vector3 playerPosition, Vector3 portalPosition)
    {
        return (playerPosition - portalPosition).sqrMagnitude < SqrCloseDistance;
    }

    private System.Collections.IEnumerator WaitForSpace(Vector3 targetPosition)
    {
        yield return new WaitUntil(() => _buttonScriptComponent._buttonIsActivated == true || Input.GetKeyDown(KeyCode.Space));

        TeleportPlayer(targetPosition);
    }

    private IEnumerator BlinkText()
    {
        if (_isBlinking)
        {
            yield break;
        }

        _isBlinking = true;
        float targetAlpha = 0f;
        while (tmpro.color.a > targetAlpha)
        {
            alpha -= Time.deltaTime * 2f;
            tmpro.color = new Color(tmpro.color.r, tmpro.color.g, tmpro.color.b, alpha);
            yield return null;
        }

        targetAlpha = 1f;
        while (tmpro.color.a < targetAlpha)
        {
            alpha += Time.deltaTime * 2f;
            tmpro.color = new Color(tmpro.color.r, tmpro.color.g, tmpro.color.b, alpha);
            yield return null;
        }

        _isBlinking = false;
    }

    private void TeleportPlayer(Vector3 targetPosition)
    {
        MusicControllerScript.Instance.PlaySound("teleport");
        transform.position = targetPosition;
        _buttonScriptComponent._buttonIsActivated = false;
    }
}