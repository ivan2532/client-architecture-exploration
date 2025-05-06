using Core.View;
using UnityEngine;

namespace Core.Controller
{
    public class ApplicationController : ControllerBase<ApplicationView>
    {
        public ApplicationController(ApplicationView view) : base(view)
        {
            Debug.Log("Application Controller constructor called.");
        }
    }
}