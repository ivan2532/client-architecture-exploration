using Core.Infrastructure.ViewController;
using UnityEngine;

namespace Core.Infrastructure
{
    [DefaultExecutionOrder(-1)]
    public class ApplicationContext : MonoBehaviour
    {
        [SerializeField] private ServiceRegistry serviceRegistry;

        private ControllerService _controllerService;

        private void OnEnable()
        {
            serviceRegistry.Initialize();
            InitializeServices();
        }

        private void OnDisable()
        {
            DisposeServices();
        }

        private void InitializeServices()
        {
            _controllerService = new ControllerService(serviceRegistry);
            serviceRegistry.Register(_controllerService);
        }

        private void DisposeServices()
        {
            _controllerService.Dispose();
        }
    }
}