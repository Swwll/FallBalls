using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class PointsCounter : MonoBehaviour
    {
        [SerializeField]
        private Player _player;

        private TextMeshProUGUI _textMesh;

        private void Start()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            _player.PointsCountChanged += OnPointsCountChanged;
        }

        private void OnDisable()
        {
            _player.PointsCountChanged -= OnPointsCountChanged;
        }

        private void OnPointsCountChanged(int points)
        {
            _textMesh.text = points.ToString();
        }
    }
}