using UnityEngine;

namespace Core.Infrastructure
{
    public class ApplicationContext : MonoBehaviour
    {
        [SerializeField] private ServiceRegistry serviceRegistry;

        private ControllerService _controllerService;

        private void OnEnable()
        {
            serviceRegistry.Initialize();
            _controllerService = new ControllerService(serviceRegistry);
        }

        private void OnDisable()
        {
            _controllerService.Dispose();
        }
    }
}