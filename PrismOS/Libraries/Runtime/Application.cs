﻿using System.Collections.Generic;

namespace PrismOS.Libraries.Runtime
{
    public abstract class Application
    {
        // Application Manager Variable
        public static List<Application> Applications { get; set; } = new();

        public Application()
        {
            OnCreate();
            Applications.Add(this);
        }
        ~Application()
        {
            OnDestroy();
            Applications.Remove(this);
        }

        public abstract void OnUpdate();
        public abstract void OnDestroy();
        public abstract void OnCreate();
    }
}