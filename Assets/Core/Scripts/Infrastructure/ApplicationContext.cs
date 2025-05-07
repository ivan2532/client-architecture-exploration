using UnityEngine;

namespace Core.Infrastructure
{
    public class ApplicationContext : MonoBehaviour
    {
        [SerializeField] private ServiceRegistry serviceRegistry;

        private ControllerService _controllerService;

        private void OnEnable()
        {
            _controllerService = new ControllerService();
        }

        private void OnDisable()
        {
            _controllerService.Dispose();
        }
    }
}