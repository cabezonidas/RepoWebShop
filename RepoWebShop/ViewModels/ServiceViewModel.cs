﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class ServiceViewModel
    {
        public readonly string Action;
        public readonly string Controller;
        public readonly string IconClass;
        public readonly string Title;
        public readonly string Description;

        public ServiceViewModel(string action, string controller, string iconClass, string title, string description)
        {
            Action = action;
            Controller = controller;
            IconClass = iconClass;
            Title = title;
            Description = description;
        }
    }
}
